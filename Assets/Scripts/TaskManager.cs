using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance = null;

    public int tasksPerMinuteWhenStarting;
    public Transform parentGUI;
    public GameObject task;
   // public GameObject player;

    private float waitingTime;
    private bool isCounting;
    private List<GameObject> taskList;

    public StrSpawner[] spawnPoints;
    public Vector2 refScreenSize;

    [Serializable]
    public struct StrSpawner
    {
        public Vector3 position;
        public bool occupied;
    };

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        waitingTime = 60 / tasksPerMinuteWhenStarting;
        isCounting = false;

        taskList = new List<GameObject>(); 
    }


    void Update()
    {
        if (!isCounting)
            StartCoroutine("WaitAndSpawn");
    }


    public Vector3 ChooseLocation(TaskIdentity task)
    {
        int i = 0;

        while(i < 20)
        {
            if (!spawnPoints[i].occupied)
            {
                spawnPoints[i].occupied = true;
                task.slot = i;
                Vector3 pos = new Vector3(spawnPoints[i].position.x * Screen.width / refScreenSize.x, Screen.height - (spawnPoints[i].position.y * Screen.height / refScreenSize.y), 0f);
                return (pos);
            }
            i++;
        }
        taskList.Remove(task.gameObject);
        task.AutoDestroy();
        return (new Vector3 (0,0,0));
    }


    public Quaternion ChooseRotation()
    {
        float z = 0f;

        z = UnityEngine.Random.Range(-15, 15);
        return (Quaternion.Euler(0f, 0f, z));
    }

    IEnumerator WaitAndSpawn()
    {
        isCounting = true;

        yield return new WaitForSeconds(waitingTime);

        GameObject taskToAdd = Instantiate(task, parentGUI, false);
        taskList.Add(taskToAdd);

        isCounting = false;
    }

    //function called by player
    public void TriggerCoworker(CoworkerScript coworker)
    {
        GameObject[] toDestroy = { null, null };
        int i = 0;
        bool isHappy = false;

        foreach (GameObject lookedAt in taskList)
        {
            if (i < 2 && lookedAt.GetComponent<TaskIdentity>().coworker == coworker.number)
                toDestroy[i] = lookedAt.GetComponent<TaskIdentity>().IsFulfilled();
            if (i < 2 && toDestroy[i] != null)
                i++;
        }

        while (i > 0)
        {
            coworker.GetComponent<CoworkerScript>().Happy();
            i--;
            taskList.Remove(toDestroy[i]);
            toDestroy[i].GetComponent<TaskIdentity>().AutoDestroy();
            isHappy = true;
        }
        if (!isHappy)
            coworker.GetComponent<CoworkerScript>().Mad();
    }
}

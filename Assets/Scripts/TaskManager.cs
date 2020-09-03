using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance = null;

    public int tasksPerMinuteWhenStarting;
    public GameObject task;
    public GameObject player;

    public Vector3[] tasksSlots; 

    private float waitingTime;
    private bool isCounting;
    private List<GameObject> taskList;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        waitingTime = tasksPerMinuteWhenStarting / 60;
        isCounting = false;

        taskList = new List<GameObject>(); 
    }

    void Update()
    {
        if (!isCounting)
            StartCoroutine("WaitAndSpawn");
    }

    private Vector3 ChooseLocation()
    {
        //Partie de Noé. Reservée à Noé. Ne pas toucher sauf Noé.

        return (new Vector3(0, 0, 0));
        //placeholder de fonction
        //tableau de structure qui retiennent chacune une pos & un bool "occupied"
    }

    private Quaternion ChooseRotation()
    {
        float z = 0f;
        float y = 0f;

        z = Random.Range(-13, 13);
        y = ((int)Random.Range(0, 1) == 1) ? 0 : 180;//optionnel, rotation miroir sur le postit
        return (Quaternion.Euler(z, 0f, y));
    }

    IEnumerator WaitAndSpawn()
    {
        isCounting = true;

        yield return new WaitForSeconds(waitingTime);

        GameObject taskToAdd = Instantiate(task, ChooseLocation(), ChooseRotation());
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
            //player.GetComponent<PlayerEntity>().EmptyHands(toDestroy[i].GetComponent<TaskIdentity>().type);////envoie le int du type de tâche pour savoir quoi jeter
            taskList.Remove(toDestroy[i]);
            toDestroy[i].GetComponent<TaskIdentity>().AutoDestroy();
            isHappy = true;
        }
        if (!isHappy)
            coworker.GetComponent<CoworkerScript>().Mad();
    }
}

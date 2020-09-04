using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    private int taskDone = 0;
    private float timerPlay = 0f;
    public int tasksPerMinuteWhenStarting;
    public Transform parentGUI;
    public GameObject task;
    public List<Task> tasks;
    // public GameObject player;
    public AudioClip soundPopPostIt;
    public GameObject gameOver;
    
    //public GameObject lightSys;
    //private LightTest lightTestScript;
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
        tasks = new List<Task>();
    }

    void Start()
    {
        //lightTestScript = lightSys.GetComponent<LightTest>();
        waitingTime = 60 / tasksPerMinuteWhenStarting;
        isCounting = false;

        taskList = new List<GameObject>(); 
    }


    void Update()
    {
        timerPlay += Time.deltaTime;
        if (!isCounting)
            StartCoroutine("WaitAndSpawn");
    }

    public void AddTaskDone()
    {
        taskDone++;

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
                AudioSource.PlayClipAtPoint(soundPopPostIt, transform.position);
                return (pos);
            }
            i++;
        }
        taskList.Remove(task.gameObject);
        task.AutoDestroy();

        gameOver.SetActive(true);
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
        //lightTestScript.addFog();
        isCounting = false;
    }

    public void ClearList()
    {
        foreach (Task toDestroy in tasks)
        {
            Destroy(toDestroy.postit);
        }
        tasks.Clear();
        foreach (GameObject toDestroyBis in taskList)
        {
            toDestroyBis.GetComponent<TaskIdentity>().AutoDestroy();
        }
        taskList.Clear();
    }

    //function called by player//plus appellée. A CLEAN
    public void TriggerCoworker(CoworkerScript coworker)
    {
        GameObject[] toDestroy = { null, null };
        int i = 0;
        bool isHappy = false;

        foreach (GameObject lookedAt in taskList)
        {
            if (i < 2 && lookedAt.GetComponent<TaskIdentity>().coworker == coworker.number)
                //toDestroy[i] = lookedAt.GetComponent<TaskIdentity>().IsFulfilled();
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


    public void IsFulfilled(Inventory inventory, string worker, PlayerEntity player)
    {
        foreach (Task task in tasks)
        {
            if (task.worker == worker)
            {
                if (task.type == "coffee" && (inventory.itemOne == "CoffeeFull" || inventory.itemTwo == "CoffeeFull"))
                {
                    spawnPoints[task.postit.GetComponent<TaskIdentity>().slot].occupied = false;
                    tasks.Remove(task);
                    Destroy(task.postit);
                    player.DeleteItem("CoffeeFull");
                    taskDone++;
                    player.targetItem.GetComponent<CoworkerScript>().Happy();
                    //lightTestScript.remFog();
                    return;
                }
                else if (task.type == "printer" && (inventory.itemOne == "Photocopy" || inventory.itemTwo == "Photocopy"))
                {
                    spawnPoints[task.postit.GetComponent<TaskIdentity>().slot].occupied = false;
                    tasks.Remove(task);
                    Destroy(task.postit);
                    player.DeleteItem("Photocopy");
                    taskDone++;
                    player.targetItem.GetComponent<CoworkerScript>().Happy();
                    //lightTestScript.remFog();
                    return;
                }
                else if (task.type == "RedFolder" || task.type == "PurpleFolder" || task.type == "GreenFolder" || task.type == "BlueFolder") 
                {
                    if (task.worker == "red" && (inventory.itemOne == "RedDocument" || inventory.itemTwo == "RedDocument"))
                    {
                        spawnPoints[task.postit.GetComponent<TaskIdentity>().slot].occupied = false;
                        tasks.Remove(task);
                        Destroy(task.postit);
                        player.DeleteItem("RedDocument");
                        taskDone++;
                        player.targetItem.GetComponent<CoworkerScript>().Happy();
                        //lightTestScript.remFog();
                        return;
                    }
                    else if (task.worker == "blue" && (inventory.itemOne == "BlueDocument" || inventory.itemTwo == "BlueDocument"))
                    {
                        spawnPoints[task.postit.GetComponent<TaskIdentity>().slot].occupied = false;
                        tasks.Remove(task);
                        Destroy(task.postit);
                        player.DeleteItem("BlueDocument");
                        taskDone++;
                        player.targetItem.GetComponent<CoworkerScript>().Happy();
                        //lightTestScript.remFog();
                        return;
                    }
                    else if (task.worker == "green" && (inventory.itemOne == "GreenDocument" || inventory.itemTwo == "GreenDocument"))
                    {
                        spawnPoints[task.postit.GetComponent<TaskIdentity>().slot].occupied = false;
                        tasks.Remove(task);
                        Destroy(task.postit);
                        player.DeleteItem("GreenDocument");
                        taskDone++;
                        player.targetItem.GetComponent<CoworkerScript>().Happy();
                        //lightTestScript.remFog();
                        return;
                    }
                    else if (task.worker == "purple" && (inventory.itemOne == "PurpleDocument" || inventory.itemTwo == "PurpleDocument"))
                    {
                        spawnPoints[task.postit.GetComponent<TaskIdentity>().slot].occupied = false;
                        tasks.Remove(task);
                        Destroy(task.postit);
                        player.DeleteItem("PurpleDocument");
                        taskDone++;
                        player.targetItem.GetComponent<CoworkerScript>().Happy();
                        //lightTestScript.remFog();
                        return;
                    }
                }
                player.targetItem.GetComponent<CoworkerScript>().Mad();
            }
        }
        player.targetItem.GetComponent<CoworkerScript>().Mad();
        //fonction return null si la tâche n'est PAS fulfilled et gameObject si elle est fulfilled
        //probe l'inventaire pour voir si l'item voulu est là.
        //si non -> return null
        //si oui, appelle fonction interne de Player pour destroy cet item de son inventaire, puis -> return gameObject
        //(chaque tâche a une var type : 0- coffee, 1- photocopie, 2- dossier1, 3- dossier2, 4- dossier3);
    }

    IEnumerator TimerAnim(GameObject target)
    {
        yield return new WaitForSeconds(3f);
        target.GetComponent<Animator>().SetFloat("happiness", 0);
    }
}

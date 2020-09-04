using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskIdentity : MonoBehaviour
{
    public Color[] coworkersList = new Color[4];
    public Color[] colorOfArchive = new Color[5];
    public string[] task = new string[3];
    public Sprite[] taskSprite = new Sprite[3];
    public int type, coworker;
    public int slot;
    public Image img;
    int randomTask , randomArchiveColor;
    public Image taskIcon;

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
        CreateTask();
        slot = 0;
    }


    private void CreateTask()
    {
        string typeTask = "";
        string work = "";
        this.transform.position = TaskManager.instance.ChooseLocation(this);
        this.transform.rotation = TaskManager.instance.ChooseRotation();

        coworker = UnityEngine.Random.Range(0, coworkersList.Length);
        if (coworker == 0)
        {
            work = "blue";
        }
        else if (coworker == 1)
        {
            work = "green";
        }
        else if (coworker == 2)
        {
            work = "purple";
        }
        else
        {
            work = "red";
        }
        img.color = coworkersList[coworker];
        randomTask = UnityEngine.Random.Range(0, task.Length);
        type = randomTask;
        if (type == 0)
            typeTask = "coffee";
        else if (type == 1)
            typeTask = "printer";
        else if (type == 2)
        {
            type += coworker;
            if (type == 2)
                typeTask = "BlueFolder";
            else if (type == 3)
                typeTask = "GreenFolder";
            else if (type == 4)
                typeTask = "PurpleFolder";
            else if (type == 5)
                typeTask = "RedFolder";
        }
        taskIcon.sprite = taskSprite[type];//change icon
        TaskManager.instance.tasks.Add(new Task { worker = work, type = typeTask, postit = this.gameObject });
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }

    
}

[Serializable]
public struct Task
{
    public string worker;
    public string type;
    public GameObject postit;
}

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

    private TaskManager tm;
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
        tm = FindObjectOfType<TaskManager>();
        this.transform.position = tm.ChooseLocation(this);
        this.transform.rotation = tm.ChooseRotation();

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
        if(type == 0)
        {
            typeTask = "coffee";
        }
        else if (type == 1)
        {
            typeTask = "printer";
        }
        else if(type == 2)
        {
            randomArchiveColor = UnityEngine.Random.Range(0, colorOfArchive.Length); // choosing color for archive
            if(colorOfArchive[randomArchiveColor]== colorOfArchive[0])
            {
                type = 2;
                // archive bleue
            }
            if (colorOfArchive[randomArchiveColor] == colorOfArchive[1])
            {
                type = 3;
                // archive rouge
            }
            if (colorOfArchive[randomArchiveColor] == colorOfArchive[2])
            {
                type = 4;
                //archive verte
            }

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

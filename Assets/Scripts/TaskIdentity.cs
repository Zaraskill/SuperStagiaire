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
    int type, coworker;
    public Image img;
    int randomTask , randomArchiveColor;
    public Image taskIcon;
    
    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) 
        {  
                CreateTask(); //just a test. 
        }
        
    }

    private void CreateTask()
    {
        coworker = Random.Range(0, coworkersList.Length);
        img.color = coworkersList[coworker];
        randomTask = Random.Range(0, task.Length);
        if(task[randomTask]== "cofee")
        {
            type = 0;
            
        }
        else if(task[randomTask] == "printer")
        {
            type = 1;
            
        }
        else
        {
            randomArchiveColor = Random.Range(0, colorOfArchive.Length); // choosing color for archive
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

            //print("New task: " + colorOfArchive[randomArchiveColor] + task[randomTask] + " for"+ coworkersList[randomCoworker]+".");
        }
        /*        else
                {
                    print("New task: " + *//*colorOfArchive[randomArchiveColor] +*//* task[randomTask] + " for" + coworkers[randomCoworker] + ".");
                }*/
        print("coworker: " + coworker + " type: " + type);
        taskIcon.sprite = taskSprite[type];
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }
}

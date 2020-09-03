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
        tm = FindObjectOfType<TaskManager>();
        this.transform.position = tm.ChooseLocation(this);
        this.transform.rotation = tm.ChooseRotation();

        coworker = Random.Range(0, coworkersList.Length);
        img.color = coworkersList[coworker];
        randomTask = Random.Range(0, task.Length);
        type = randomTask;
        
        if(type == 2)
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

        }
        taskIcon.sprite = taskSprite[type];//change icon
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }

    public GameObject IsFulfilled()
    {
        //fonction return null si la tâche n'est PAS fulfilled et gameObject si elle est fulfilled
        //probe l'inventaire pour voir si l'item voulu est là.
        //si non -> return null
        //si oui, appelle fonction interne de Player pour destroy cet item de son inventaire, puis -> return gameObject
        //(chaque tâche a une var type : 0- coffee, 1- photocopie, 2- dossier1, 3- dossier2, 4- dossier3);
        return null;
    }
}

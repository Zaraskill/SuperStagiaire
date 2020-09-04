using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFolderManager : MonoBehaviour
{
    public static SpawnFolderManager instance;

    [Header("Spawns")]
    public GameObject spawnOne;
    public GameObject spawnTwo;
    public GameObject spawnThree;
    public GameObject spawnFour;

    [Header("Folders")]
    public GameObject folderBlue;
    public GameObject folderRed;
    public GameObject folderGreen;
    public GameObject folderPurple;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void Start()
    {
        GameObject obj = Instantiate(folderBlue);
        obj.transform.position = spawnOne.transform.position;

        obj = Instantiate(folderRed);
        obj.transform.position = spawnTwo.transform.position;
        
        obj = Instantiate(folderGreen);
        obj.transform.position = spawnThree.transform.position;

        obj = Instantiate(folderPurple);
        obj.transform.position = spawnFour.transform.position;
/*
        List<GameObject> folders = new List<GameObject>();
        folders.Add(folderBlue);
        folders.Add(folderGreen);
        folders.Add(folderPurple);
        folders.Add(folderRed);

        GameObject obj = Instantiate(folders[Random.Range(0, folders.Capacity)]);
        obj.transform.position = spawnOne.transform.position;
        folders.Remove(obj);

        obj = Instantiate(folders[Random.Range(0, folders.Capacity)]);
        obj.transform.position = spawnTwo.transform.position;
        folders.Remove(obj);

        obj = Instantiate(folders[Random.Range(0, folders.Capacity)]);
        obj.transform.position = spawnThree.transform.position;
        folders.Remove(obj);

        obj = Instantiate(folders[Random.Range(0, folders.Capacity)]);
        obj.transform.position = spawnFour.transform.position;
        folders.Remove(obj);*/
    }
}

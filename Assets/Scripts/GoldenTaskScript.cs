using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenTaskScript : MonoBehaviour
{
    public int number;
    public bool fulfilled;
    public Sprite[] folderSprites;
    public Image taskIcon;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void LaunchTask()
    {
        if (number == 3)
        {
            int randFolder = Random.Range(0, folderSprites.Length);
            taskIcon.sprite = folderSprites[randFolder];
        }
        gameObject.SetActive(true);
    }

    //receives false if failed, true if succeeded
    public void KillTask(bool status)
    {
        gameObject.SetActive(false);
        fulfilled = status;
    }

    public bool isFulfilled()
    {
        if (fulfilled)
            return false;

        //check inventaire pour son type
        //if not, return false
        //if yes, fulfilled = true, drop item deactivate && return true && killtask(true);
    }


}

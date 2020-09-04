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

    private int folderColor; //0 = R ; 1 = B ; 2 = P ; 3 = G

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
            folderColor = Random.Range(0, folderSprites.Length);
            taskIcon.sprite = folderSprites[folderColor];
        }
        gameObject.SetActive(true);
    }

    //receives false if failed, true if succeeded
    public void KillTask(bool status)
    {
        gameObject.SetActive(false);
        fulfilled = status;
    }

    public bool isFulfilled(Inventory inventory, PlayerEntity player)
    {
        if (fulfilled)
            return false;
        if (number == 1)
        {
            if (inventory.itemOne == "CoffeeFull" || inventory.itemTwo == "CoffeeFull")
            {
                player.DeleteItem("CoffeeFull");
                TaskManager.instance.AddTaskDone();
                KillTask(true);
                return (fulfilled);
            }
        }
        else if (number == 2)
        {
            if (inventory.itemOne == "Photocopy" || inventory.itemTwo == "Photocopy")
            {
                player.DeleteItem("Photocopy");
                TaskManager.instance.AddTaskDone();
                KillTask(true);
                return (fulfilled);
            }
        }
        else if (number == 3)
        {
            if (folderColor == 0 && (inventory.itemOne == "RedDocument" || inventory.itemTwo == "RedDocument"))
            {
                player.DeleteItem("RedDocument");
                TaskManager.instance.AddTaskDone();
                KillTask(true);
                return (fulfilled);
            }
            else if (folderColor == 1 && (inventory.itemOne == "BlueDocument" || inventory.itemTwo == "BlueDocument"))
            {
                player.DeleteItem("BlueDocument");
                TaskManager.instance.AddTaskDone();
                KillTask(true);
                return (fulfilled);
            }
            else if (folderColor == 2 && (inventory.itemOne == "PurpleDocument" || inventory.itemTwo == "PurpleDocument"))
            {
                player.DeleteItem("PurpleDocument");
                TaskManager.instance.AddTaskDone();
                KillTask(true);
                return (fulfilled);
            }
            else if (folderColor == 3 && (inventory.itemOne == "GreenDocument" || inventory.itemTwo == "GreenDocument"))
            {
                player.DeleteItem("GreenDocument");
                TaskManager.instance.AddTaskDone();
                KillTask(true);
                return (fulfilled);
            }
        }
        return false;
    }


}

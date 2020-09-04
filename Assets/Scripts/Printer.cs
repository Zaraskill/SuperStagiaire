using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{

    public float timePrint;
    public GameObject photocopy;
    private float timerPrint;
    private bool hasStartPrint;
    private PlayerEntity player;

    // Start is called before the first frame update
    void Start()
    {
        timerPrint = timePrint;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStartPrint)
        {
            timerPrint -= Time.deltaTime;
            if(timerPrint <= 0)
            {
                hasStartPrint = false;
                timerPrint = timePrint;
                player.canMove = true;
                player.ObtainPhotocopy(photocopy);
            }
        }
    }

    public void StartPrinting(PlayerEntity player)
    {
        player.PlaySoundPrinter();
        this.player = player;
        player.canMove = false;
        hasStartPrint = true;
    }

    public void StopPrinting()
    {
        hasStartPrint = false;
        timerPrint = timePrint;
    }
}

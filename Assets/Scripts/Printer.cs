using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{

    public float timePrint;
    public int numberCopiesEnd;
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
                timerPrint = timePrint;
                player.ObtainPhotocopy(numberCopiesEnd);
            }
        }
    }

    public void StartPrinting(PlayerEntity player)
    {
        this.player = player;
        hasStartPrint = true;
    }

    public void StopPrinting()
    {
        hasStartPrint = false;
        timerPrint = timePrint;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{

    public float timePrint;
    public GameObject photocopy;
    public GameObject child;
    public GameObject objToScale;
    private float timerPrint;
    private bool hasStartPrint;
    private PlayerEntity player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStartPrint)
        {
            timerPrint += Time.deltaTime;

            objToScale.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one,timerPrint/timePrint);
            if(timerPrint >= timePrint)
            {
                StopPrinting();
                hasStartPrint = false;
                timerPrint = 0;
                player.canMove = true;
                player.ObtainPhotocopy(photocopy);
            }
        }
    }

    public void StartPrinting(PlayerEntity player)
    {
        child.SetActive(true);
        //player.PlaySoundPrinter();
        this.player = player;
        player.canMove = false;
        hasStartPrint = true;
    }

    public void StopPrinting()
    {
        player.canMove = true;
        objToScale.transform.localScale = Vector3.zero;
        child.SetActive(false);
        hasStartPrint = false;
        timerPrint = 0;
    }

    public bool HasStartPrint()
    {
        return hasStartPrint;
    }
}

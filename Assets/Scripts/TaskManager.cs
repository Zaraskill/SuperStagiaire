using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public int tasksPerMinuteWhenStarting;
    public GameObject task;

    private float waitingTime;
    private bool isCounting;
    
    //initialisation of tasks list
    void Start()
    {
        waitingTime = tasksPerMinuteWhenStarting / 60;
        isCounting = false;
        //créer liste des tasks
    }

    void Update()
    {
        if (!isCounting)
            StartCoroutine("WaitAndSpawn");
    }

    IEnumerator WaitAndSpawn()
    {
        isCounting = true;

        yield return new WaitForSeconds(waitingTime);

        //Instantiate(task, );
        isCounting = false;
    }

    // Update is called once per frame
    public void TriggerCoworker(CoworkerScript coworker)
    {
        bool isHappy = false;
        //2.boucle dans la liste en cherchant la valeur du collègue triggered
        //3.si ya une tâche à lui, on check si elle est fulfilled (appelle de fonction interne à la task pour checker si fulfilled)
        //4.si oui, isHappy = true; && destroy task && destroy chainon liste && destroy objet donné dans inventaire joueur
        //5.si non, rien;
        //6.oui ou non, on continue la boucle et on recommence les étapes 3 à 6 //////////////////////////////////////////////////////////a voir avec GDS (partie sur si plusieurs tasks validables, validées en un press)
        //7.qd sort, si !isHappy collègueTriggered.Mad() si isHappy collègueTriggered.Happy()

    }
}

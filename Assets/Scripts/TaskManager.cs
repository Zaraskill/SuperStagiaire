using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    //ref vers player
    
        private CoworkerScript triggeredCoworker;

    //initialisation of tasks list
    void Start()
    {
        //créer liste des tasks
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
            ;
            //instantiate 1 task d'un type random et pour un collègue random;
            //ajouter cette task au bout de la liste;

        //si tu press A
            //0. check si près d'un collègue. Non, leave. Oui, continue.
            //1.isHappy est false de base
            //2.boucle dans la liste en cherchant la valeur du collègue triggered
            //3.si ya une tâche à lui, on check si elle est fulfilled
            //4.si oui, isHappy = true; && destroy task && destroy chainon liste && destroy objet donné dans inventaire joueur
            //5.si non, rien;
            //6.oui ou non, on continue la boucle et on recommence les étapes 3 à 6 //////////////////////////////////////////////////////////a voir avec GDS (partie sur si plusieurs tasks validables, validées en un press)
            //7.qd sort, si !isHappy collègueTriggered.Mad() si isHappy collègueTriggered.Happy()
    }
}

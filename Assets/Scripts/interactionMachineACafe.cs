using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactionMachineACafe : MonoBehaviour
{
    public float tempsDePreparationCafe;
    public inventairePlayer inventaire;
    
    private bool isInRange;
    private bool attente;
    private bool cafePret = false;
  
        
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !attente && !cafePret && Input.GetKeyDown("a"))
            StartCoroutine(PreparationCafe());
        else if (isInRange && !attente && cafePret && Input.GetKeyDown("a"))
        {
            if (inventaire.GetObjet("cafe"))
                cafePret = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = false;
    }

    IEnumerator PreparationCafe()
    {
        print("le cafe coule");
        attente = true;
        yield return new WaitForSeconds(tempsDePreparationCafe);
        attente = false;
        print("le cafe ne coule plus");
        cafePret = true;
    }

}
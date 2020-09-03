using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionPoubelle : MonoBehaviour
{
    public inventairePlayer inventaire;

    private bool isInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown("a"))
            inventaire.DropObjet();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventairePlayer : MonoBehaviour
{
    public string[] inventaire;

    private int nObjet = 0 ;

    void Start()
    {
        inventaire = new string[2] ;
    }

    void Update()
    {
        
    }

    public bool GetObjet (string objet)
    {
        if (nObjet < 2)
        {
            inventaire[nObjet] = objet;
            nObjet++;
            return true;
        }
        else
            return false;
    }

    public void DropObjet()
    {
        inventaire[0] = "";
        inventaire[1] = "";
    }
}

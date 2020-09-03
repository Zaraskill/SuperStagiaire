using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerScript : MonoBehaviour
{
    public int number;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mad()
    {
        Debug.Log("Coworker " + number + " is mad");
    }

    public void Happy()
    {
        Debug.Log("Coworker " + number + " is happy");
    }
}

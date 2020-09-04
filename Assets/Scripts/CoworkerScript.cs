using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerScript : MonoBehaviour
{
    public int number;
    public AudioClip soundHappy;
    public AudioClip soundAngry;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mad()
    {
        AudioSource.PlayClipAtPoint(soundAngry, transform.position);
        Debug.Log("Coworker " + number + " is mad");
    }

    public void Happy()
    {
        AudioSource.PlayClipAtPoint(soundHappy, transform.position);
        Debug.Log("Coworker " + number + " is happy");
    }
}

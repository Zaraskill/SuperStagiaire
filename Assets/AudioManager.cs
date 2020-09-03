using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public AudioClip[] music;
    public AudioSource[] audioSource;

    // Start is called before the first frame update
    void Start()
    {
        for(int indexMusic = 0; indexMusic < music.Length; indexMusic++)
        {
            audioSource[indexMusic].clip = music[indexMusic];
            audioSource[indexMusic].Play();
        }


        
        /*audioSource[1].clip = music[1];
        audioSource[1].Play();*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

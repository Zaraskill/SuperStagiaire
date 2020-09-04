using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class LightTest : MonoBehaviour
{
    private Light l;
    bool start = true;
    void Start()
    {
        l = gameObject.GetComponent<Light>();
        l.spotAngle = 30;
    }

    // Update is called once per frame
    void Update()
    {
        //l.color = new Color(0.2f, 0.5f, 0.9f, 1); //color test
        
        if (start == true)
        {

        }
    }


}

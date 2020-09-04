using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class LightTest : MonoBehaviour
{
    private Light l;
    public float defaultLight;

    void Start()
    {
        l = gameObject.GetComponent<Light>();
        l.spotAngle = defaultLight;
    }

    // Update is called once per frame

    public void addFog()
    {
        l.spotAngle -= defaultLight/20;
    }

    public void remFog()
    {
        l.spotAngle += defaultLight / 20;
    }

    public void resetLight()
    {
        l.spotAngle = defaultLight;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTestScript : MonoBehaviour
{
    public int worker;
    public int type;

    // Start is called before the first frame update
    void Start()
    {
        worker = 1;
        type = 1;
    }

    public GameObject IsFulfilled()
    {
        int plop = Random.Range(0, 1);

        if (plop == 0)
            return (gameObject);
        return (null);
    }

    public void AutoDestroy()
    {
        Debug.Log("Boum !");
    }
}

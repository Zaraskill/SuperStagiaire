using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float goldenTaskSpawnTime;
    public float goldenTaskTimeToFulfill;

    public GameObject GT1;
    public GameObject GT2;
    public GameObject GT3;

    private bool success;          
    private bool isCounting;

    void Start()
    {
        isCounting = false;
        success = false;
    }

    void Update()
    {
        if (!isCounting)
            StartCoroutine("WaitAndSpawn");
        CheckForSuccess();
    }

    IEnumerator WaitAndSpawn()
    {
        isCounting = true;

        yield return new WaitForSeconds(goldenTaskSpawnTime);

        isCounting = false;

        GT1.GetComponent<GoldenTaskScript>().LaunchTask();
        GT2.GetComponent<GoldenTaskScript>().LaunchTask();
        GT3.GetComponent<GoldenTaskScript>().LaunchTask();
        StartCoroutine("TaskTimer");
    }

    IEnumerator TaskTimer()
    {
        yield return new WaitForSeconds(goldenTaskTimeToFulfill);
        if (!success)
            Failure();
        success = false;
    }

    private void Failure()
    {
        //boss mad
        //tasks disappear
        //ttes les taches, fulfilled = false
    }

    //called by player when press A near boss
    public void TriggerBoss()
    {
        bool status = false;

        status = GT1.GetComponent<GoldenTaskScript>().isFulfilled();
        status = status ? status : GT2.GetComponent<GoldenTaskScript>().isFulfilled();
        status = status ? status : GT3.GetComponent<GoldenTaskScript>().isFulfilled();

        if (status)
            Happy();
        else
            Mad();
    }

    private void CheckForSuccess()
    {
        //si trois GT 'fulfilled' == true, alors success = true
        //thumbs up
        //clean task tree
        //toutes les tâches fulfilled == false
    }

    public void Happy()
    {
        Debug.Log("yes !");//////////////// A CHANGER
    }

    public void Mad()
    {
        Debug.Log("GRRRRR");/////////////////// A CHANGER
    }
}

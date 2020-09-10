using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public static BossScript instance;

    public float goldenTaskSpawnTime;
    public float goldenTaskTimeToFulfill;

    public GameObject GT1;
    public GameObject GT2;
    public GameObject GT3;

    private bool success;          
    private bool isCounting;

    public AudioClip soundHappy;
    public AudioClip soundAngry;
    //public AudioClip soundBossTask;

    //public GameObject lightSys;
    //private LightTest lightTestScript;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        //lightTestScript = lightSys.GetComponent<LightTest>();
        isCounting = false;
        success = false;
    }

    void Update()
    {
        if (!isCounting)
        {
            StartCoroutine("WaitAndSpawn");
            //AudioSource.PlayClipAtPoint(soundBossTask, transform.position);
        }

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
        Debug.Log("le message de paix suivant : Coucou.");///////////////////////
        Mad();
        GT1.GetComponent<GoldenTaskScript>().KillTask(false);
        GT2.GetComponent<GoldenTaskScript>().KillTask(false);
        GT3.GetComponent<GoldenTaskScript>().KillTask(false);
    }

    //called by player
    public void TriggerBoss(Inventory inventory, PlayerEntity player)
    {
        bool status = false;

        status = GT1.GetComponent<GoldenTaskScript>().isFulfilled(inventory, player);
        status = status ? status : GT2.GetComponent<GoldenTaskScript>().isFulfilled(inventory, player);
        status = status ? status : GT3.GetComponent<GoldenTaskScript>().isFulfilled(inventory, player);

        if (status)
            Happy();
        else
            Mad();
    }

    private void CheckForSuccess()
    {
        if (GT1.GetComponent<GoldenTaskScript>().fulfilled && GT2.GetComponent<GoldenTaskScript>().fulfilled && GT2.GetComponent<GoldenTaskScript>().fulfilled)
        {
            success = true;
            GT1.GetComponent<GoldenTaskScript>().fulfilled = false;
            GT2.GetComponent<GoldenTaskScript>().fulfilled = false;
            GT3.GetComponent<GoldenTaskScript>().fulfilled = false;
            //faire un son special ?
            //fonction de clear de list pour que ce soit beau
            TaskManager.instance.ClearList();
            //lightTestScript.resetLight();
        }
    }

    public void Happy()
    {
        StopCoroutine("TimerAnim");
        GetComponent<Animator>().SetInteger("happyness", 1);
        StartCoroutine("TimerAnim");
        AudioSource.PlayClipAtPoint(soundHappy, transform.position);
    }

    public void Mad()
    {
        StopCoroutine("TimerAnim");
        GetComponent<Animator>().SetInteger("happyness", -1);
        StartCoroutine("TimerAnim");
        AudioSource.PlayClipAtPoint(soundAngry, transform.position);
    }

    IEnumerator TimerAnim()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<Animator>().SetInteger("happyness", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactionMachineACafe : MonoBehaviour
{
    public float tempsDePreparationCafe;
    
    private bool attente;
    private bool cafePret = false;

    public GameObject coffeeFull;
    private PlayerEntity player;
    private Animator _animator;
    private bool hasStartPrint;
    private float timerPrint;
    public GameObject objToScale;
    public GameObject child;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasStartPrint)
        {
            timerPrint += Time.deltaTime;

            objToScale.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timerPrint / tempsDePreparationCafe);
            if (timerPrint >= tempsDePreparationCafe)
            {
                hasStartPrint = false;
                timerPrint = 0;
                _animator.SetBool("working", false);
                cafePret = true;
                attente = false;
            }
        }
    }

    public void StartPrinting(PlayerEntity player)
    {
        child.SetActive(true);
        this.player = player;
        player.canMove = false;
        hasStartPrint = true;
    }

    public bool HasStartPrint()
    {
        return hasStartPrint;
    }


    public void Interact(PlayerEntity playerEntity)
    {
        player = playerEntity;
        if (cafePret && player.IsHoldingItems() < 2)
        {
            player.GetCoffeeFull(coffeeFull);
            _animator.SetTrigger("pickedUp");
            cafePret = false;
            child.SetActive(false);
        }
        else if (!attente && player.IsHoldingEmptyCoffee())
        {
            player.DestroyCoffeeEmpty();
            child.SetActive(true);
            _animator.SetBool("working", true);
            hasStartPrint = true;
            attente = true;
            player.PlaySoundCoffee();
        }
    }

    public bool IsCoffeeReady()
    {
        return cafePret;
    }
}
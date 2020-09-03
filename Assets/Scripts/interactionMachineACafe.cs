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

    }

    public void Interact(PlayerEntity playerEntity)
    {
        player = playerEntity;
        if (cafePret && player.IsHoldingItems() < 2)
        {
            player.GetCoffeeFull(coffeeFull);
            _animator.SetTrigger("pickedUp");
            cafePret = false;
        }
        else if (!attente && player.IsHoldingEmptyCoffee())
        {
            player.DestroyCoffeeEmpty();
            _animator.SetBool("working", true);
            StartCoroutine(PreparationCafe());
        }
    }

    public bool IsCoffeeReady()
    {
        return cafePret;
    }

    IEnumerator PreparationCafe()
    {
        Debug.Log("coffee start");
        attente = true;
        yield return new WaitForSeconds(tempsDePreparationCafe);
        _animator.SetBool("working", false);
        Debug.Log("coffee end");
        attente = false;
        cafePret = true;
    }

}
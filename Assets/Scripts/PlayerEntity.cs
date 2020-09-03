﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{

    // Move
    [Header("Move")]
    public float acceleration = 20f;
    public float moveSpeedMax = 10f;
    private Vector2 moveDir;
    private Vector2 speed = Vector2.zero;
    private Vector2 orientDir = Vector2.right;

    // Frictions
    [Header("Friction")]
    public float baseFriction;
    public float baseTurnFriction;
    private float friction;
    private float turnFriction;

    //Interation
    private string interactWith;

    //Objets
    [Header("Objets")]
    public Inventory holdingObjects;
    public bool canInteract;
    public GameObject itemHoldOne;
    public GameObject itemHoldTwo;
    private GameObject targetItem;
    private List<GameObject> holdingItems;
    private float placeCopies;

    //Rigidbody
    [Header("Rigidbody")]
    private Rigidbody2D _rigidbody;

    //Animator
    [Header("Animator")]
    private Animator _animator;

    [Header("A delete avec integration meca printer")]
    public GameObject photocopy;

    // Debug
    [Header("Debug")]
    public bool _debugMode = false;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _rigidbody.gravityScale = 0;
        turnFriction = baseTurnFriction;
        friction = baseFriction;
        holdingItems = new List<GameObject>();
        holdingObjects.itemOne = "";
        holdingObjects.itemTwo = "";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateMove();
        UpdatePosition();
        UpdateSprite();
    }

    private void OnGUI()
    {
        if (!_debugMode)
        {
            return;
        }

        GUILayout.BeginVertical();
        GUILayout.Label("interact = " + interactWith);
        GUILayout.EndVertical();
    }


    private void UpdatePosition()
    {
        _rigidbody.velocity = new Vector2(speed.x, speed.y);
    }

    #region Move

    public void Move(Vector2 dir)
    {
        moveDir = dir;
    }

    private void UpdateMove()
    {
        if (moveDir != Vector2.zero)
        {
            _animator.SetBool("stopRunning", false);
            float turnAngle = Vector2.SignedAngle(speed, moveDir);
            turnAngle = Mathf.Abs(turnAngle);
            float frictionRatio = turnAngle / 180f;
            float turnFrictionWithRatio = turnFriction * frictionRatio;

            speed += moveDir * acceleration * Time.fixedDeltaTime;
            if (speed.sqrMagnitude > moveSpeedMax * moveSpeedMax)
            {
                speed = speed.normalized * moveSpeedMax;
            }

            Vector2 frictionDir = speed.normalized;
            speed -= frictionDir * turnFrictionWithRatio * Time.fixedDeltaTime;

            orientDir = speed.normalized;
        }
        else if (speed != Vector2.zero)
        {
            Vector2 frictionDir = speed.normalized;
            float frictionToApply = friction * Time.fixedDeltaTime;
            if (speed.sqrMagnitude <= frictionToApply * frictionToApply)
            {
                speed = Vector2.zero;
            }
            else
            {
                speed -= frictionToApply * frictionDir;
            }
        }
    }

    #endregion

    private void UpdateSprite()
    {
        _animator.SetBool("runLeft", false);
        _animator.SetBool("runUp", false);
        _animator.SetBool("runDown", false);
        _animator.SetBool("runRight", false);

        if (moveDir.x == moveDir.y && moveDir.x == 0)
        {
            _animator.SetBool("stopRunning", true);
            return;
        }
        if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y))
        {
            if (moveDir.x < 0)
            {
                _animator.SetBool("runLeft", true);
            }
            else
            {
                _animator.SetBool("runRight", true);
            }
        }
        else
        {
            if (moveDir.y < 0)
            {
                _animator.SetBool("runDown", true);
            }
            else
            {
                _animator.SetBool("runUp", true);
            }
        }
    }

    #region Collider/Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Worker")
        {
            interactWith = "worker";
            canInteract = true;
        }
        else if(collision.gameObject.tag == "ArchivesDocument")
        {
            if(IsHoldingItems() < 2)
            {
                canInteract = true;
                interactWith = "archivesDocument";
                targetItem = collision.gameObject;
            }
        }
        else if (collision.gameObject.tag == "Printer")
        {
            if (IsHoldingItems() < 2)
            {
                interactWith = "printer";
                canInteract = true;
                targetItem = collision.gameObject;                
            }
        }
        else if (collision.gameObject.tag == "Coffee")
        {
            if (IsHoldingItems() < 2)
            {
                canInteract = true;
                interactWith = "coffee";
                targetItem = collision.gameObject;
            }
        }
        else if (collision.gameObject.tag == "CoffeeMachine")
        {

        }
        else if (collision.gameObject.tag == "Trash")
        {
            if (IsHoldingItems() > 0)
            {
                canInteract = true;
                interactWith = "trash";
            }            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        interactWith = "";
    }

    #endregion

    #region Interact

    public void Interact()
    {
        if (!canInteract)
        {
            return;
        }
        if (interactWith == "worker")
        {
            GiveToWorker();
        }
        else if (interactWith == "printer")
        {
            targetItem.GetComponent<Printer>().StartPrinting(this);
        }
        else if (interactWith == "trash")
        {
            DropItems();
        }
        else
        {
            PickItem();
        }
    }

    public bool IsInteractionWithPrinter()
    {
        return interactWith == "printer";
    }

    public void StopPrinter()
    {
        targetItem.GetComponent<Printer>().StopPrinting();
    }

    public void ObtainPhotocopy(int numberCopies)
    {
        
        GameObject obj = Instantiate(photocopy);
        targetItem = obj;
        targetItem.transform.SetParent(this.transform);
        if (holdingObjects.itemOne == "")
        {
            targetItem.transform.localPosition = itemHoldOne.transform.localPosition;
            holdingObjects.itemOne = targetItem.tag;
        }
        else
        {
            targetItem.transform.localPosition = itemHoldTwo.transform.localPosition;
            holdingObjects.itemTwo = targetItem.tag;
        }        
        holdingItems.Add(targetItem);        
        _animator.SetBool("isHoldingItem", true);
    }

    private void PickItem()
    {
        if (IsHoldingItems() == 2)
        {
            return;
        }
        if(targetItem.tag == "Coffee")
        {
            if (holdingObjects.itemOne == "" && holdingObjects.itemTwo != "")
            {
                holdingObjects.itemOne = holdingObjects.itemTwo;
                holdingObjects.firstItem = holdingObjects.secondItem;
                holdingObjects.firstItem.transform.localPosition = itemHoldOne.transform.localPosition;
                holdingObjects.secondItem = null;
            }
            else
            {
                targetItem.transform.SetParent(this.transform);
                targetItem.GetComponent<CircleCollider2D>().enabled = false;
                targetItem.GetComponent<BoxCollider2D>().enabled = false;
                holdingObjects.itemTwo = targetItem.tag;
                holdingObjects.secondItem = targetItem;
                holdingObjects.secondItem.transform.localPosition = itemHoldTwo.transform.localPosition;
                _animator.SetBool("isHoldingItem", true);
                targetItem = null;
            }
        }
        else if (holdingObjects.itemOne == "")
        {
            targetItem.transform.SetParent(this.transform);
            targetItem.transform.localPosition = itemHoldOne.transform.localPosition;
            holdingObjects.firstItem = targetItem;
            holdingObjects.itemOne = targetItem.tag;
            targetItem.GetComponent<CircleCollider2D>().enabled = false;
            targetItem.GetComponent<BoxCollider2D>().enabled = false;
            _animator.SetBool("isHoldingItem", true);
            targetItem = null;
        }
        else
        {
            holdingObjects.itemTwo = targetItem.tag;
            targetItem.transform.SetParent(this.transform);
            targetItem.transform.localPosition = itemHoldTwo.transform.localPosition;
            holdingObjects.secondItem = targetItem;
            targetItem.GetComponent<CircleCollider2D>().enabled = false;
            targetItem.GetComponent<BoxCollider2D>().enabled = false;          
            canInteract = false;
            _animator.SetBool("isHoldingItem", true);
            targetItem = null;
        }
    }


    //A changer avec les quetes
    private void GiveToWorker()
    {
        holdingObjects.itemOne = "";
        holdingObjects.itemTwo = "";
        foreach(GameObject obj in holdingItems)
        {
            Destroy(obj);
        }
        _animator.SetBool("isHoldingItem", false);
    }

    public void DropItems()
    {
        holdingObjects.itemOne = "";
        holdingObjects.itemTwo = "";
        Destroy(holdingObjects.firstItem);
        Destroy(holdingObjects.secondItem);
        _animator.SetBool("isHoldingItem", false);
    }



    #endregion

    #region Items

    private float IsHoldingItems()
    {
        if(holdingObjects.itemOne != "" || holdingObjects.itemTwo != "")
        {
            if (holdingObjects.itemTwo != "" && holdingObjects.itemOne != "")
            {
                return 2;
            }
            return 1;
        }
        return 0;
    }

    #endregion

}

public struct Inventory
{
    public string itemOne;
    public string itemTwo;
    public GameObject firstItem;
    public GameObject secondItem;
}

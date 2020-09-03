using System.Collections;
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
    private bool interactWithWorker;
    private bool interactWithPrinter;

    //Objets
    [Header("Objets")]
    public string[] holdingObjects;
    public bool canPickItem;
    public GameObject itemHoldOne;
    public GameObject itemHoldTwo;
    private GameObject targetItem;
    private List<GameObject> holdingItems;
    private bool isHoldingObject;
    private float numberOfCopies;
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
        holdingObjects = new string[2] { "", "" };
        turnFriction = baseTurnFriction;
        friction = baseFriction;
        holdingItems = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(holdingObjects.Length);
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
        GUILayout.Label("printer = " + interactWithPrinter);
        GUILayout.Label("item 1 = " + holdingObjects[0]);
        GUILayout.Label("item 2 = " + holdingObjects[1]);
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
            interactWithWorker = true;
            canPickItem = true;
        }
        else if(collision.gameObject.tag == "ArchivesDocument")
        {
            interactWithWorker = false;
            if(!IsHoldingItems())
            {
                canPickItem = true;
                targetItem = collision.gameObject;
            }
        }
        else if (collision.gameObject.tag == "Printer")
        {
            placeCopies = IsHoldingPhotocopy();
            if (placeCopies == 2)
            {
                interactWithPrinter = false;
                canPickItem = false;
            }
            else
            {
                interactWithPrinter = true;
                canPickItem = true;
                targetItem = collision.gameObject;
            }
        }
        else if (collision.gameObject.tag == "Coffee")
        {
            if (!IsHoldingItems())
            {
                canPickItem = true;
                targetItem = collision.gameObject;
            }
        }
        else if (collision.gameObject.tag == "CoffeeMachine")
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPickItem = false;
        interactWithPrinter = false;
        interactWithWorker = false;
    }

    #endregion

    #region Interact

    public void Interact()
    {
        if (interactWithWorker)
        {
            GiveToWorker();
        }
        else if (interactWithPrinter)
        {
            targetItem.GetComponent<Printer>().StartPrinting(this);
        }
        else
        {
            PickItem();
        }
    }

    public bool IsInteractionWithPrinter()
    {
        return interactWithPrinter;
    }

    public void StopPrinter()
    {
        targetItem.GetComponent<Printer>().StopPrinting();
    }

    public void ObtainPhotocopy(int numberCopies)
    {
        
        numberOfCopies += numberCopies;
        if (IsHoldingPhotocopy() != 2)
        {
            return;
        }
        GameObject obj = Instantiate(photocopy);
        targetItem = obj;
        targetItem.transform.SetParent(this.transform);
        if (holdingObjects[0] == "")
        {
            targetItem.transform.localPosition = itemHoldOne.transform.localPosition;
            holdingObjects[0] = targetItem.tag;
        }
        else
        {
            targetItem.transform.localPosition = itemHoldTwo.transform.localPosition;
            holdingObjects[1] = targetItem.tag;
        }        
        holdingItems.Add(targetItem);        
        isHoldingObject = true;
        _animator.SetBool("isHoldingItem", true);
    }

    private void PickItem()
    {
        if (!canPickItem)
        {
            return;
        }
        if(targetItem.tag == "Coffee")
        {
            if (holdingObjects[0] == "")
            {
                holdingObjects[0] = holdingObjects[1];
                holdingItems[0].transform.localPosition = itemHoldOne.transform.localPosition;
            }
        }
        if (holdingObjects[0] == "")
        {
            targetItem.transform.SetParent(this.transform);
            targetItem.transform.localPosition = itemHoldOne.transform.localPosition;
            holdingItems.Add(targetItem);
            holdingObjects[0] = targetItem.tag;
            targetItem.GetComponent<CircleCollider2D>().enabled = false;
            targetItem.GetComponent<BoxCollider2D>().enabled = false;
            isHoldingObject = true;
            _animator.SetBool("isHoldingItem", true);
            targetItem = null;
        }
        else
        {
            holdingObjects[1] = targetItem.tag;
            targetItem.transform.SetParent(this.transform);
            targetItem.transform.localPosition = itemHoldTwo.transform.localPosition;
            holdingItems.Add(targetItem);
            targetItem.GetComponent<CircleCollider2D>().enabled = false;
            targetItem.GetComponent<BoxCollider2D>().enabled = false;          
            isHoldingObject = true;
            canPickItem = false;
            _animator.SetBool("isHoldingItem", true);
            targetItem = null;
        }
    }


    //A changer avec les quetes
    private void GiveToWorker()
    {
        holdingObjects[0] = "";
        holdingObjects[1] = "";
        foreach(GameObject obj in holdingItems)
        {
            Destroy(obj);
        }
        isHoldingObject = false;
        _animator.SetBool("isHoldingItem", false);
    }

    #endregion

    #region Items

    private bool IsHoldingItems()
    {
        if(holdingObjects[0] != "" && holdingObjects[1] != "")
        {
            return true;
        }
        return false;
    }

    private float IsHoldingPhotocopy()
    {
        if (IsHoldingItems())
        {
            if (holdingObjects[0] == "Photocopy")
            {
                return 0;
            }
            else if (holdingObjects[1] == "Photocopy")
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        return 3;
    }

    #endregion

}

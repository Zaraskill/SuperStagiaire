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

    //Objets
    [Header("Objets")]
    public string[] holdingObjects;
    public bool canPickItem;
    private GameObject targetItem;
    private bool isHoldingObject;
    private float numberOfCopies;

    //Rigidbody
    [Header("Rigidbody")]
    public Rigidbody2D _rigidbody;

    //Animator
    [Header("Animator")]
    public Animator _animator;

    //Model
    [Header("Models")]
    public List<Sprite> ModelsSprites;
    private SpriteRenderer spritePlayer;

    // Debug
    [Header("Debug")]
    public bool _debugMode = false;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _rigidbody.gravityScale = 0;
        spritePlayer = GetComponent<SpriteRenderer>();
        holdingObjects = new string[2] { "", "" };
        turnFriction = baseTurnFriction;
        friction = baseFriction;
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
        GUILayout.Label("Speed = " + speed);
        GUILayout.Label("moveDir = " + moveDir);
        GUILayout.Label("orientDir = " + orientDir);
        GUILayout.Label("holdingItem = " + isHoldingObject);
        GUILayout.Label("pickItem = " + canPickItem);
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
        if(collision.gameObject.tag == "ArchivesDocument")
        {
            if(holdingObjects[1].Contains(""))
            {
                canPickItem = true;
                targetItem = collision.gameObject;
            }
        }
    }

    #endregion

    #region Items

    public void PickItem()
    {
        if(holdingObjects[0].Contains(""))
        {
            holdingObjects[0] = targetItem.tag;
            isHoldingObject = true;
            _animator.SetBool("isHoldingItem", true);
        }
        else
        {
            holdingObjects[1] = targetItem.tag;
            isHoldingObject = true;
            _animator.SetBool("isHoldingItem", true);
        }
    }

    #endregion
}

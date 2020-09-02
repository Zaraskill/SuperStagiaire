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

    //Objets
    [Header("Objets")]
    public string[] holdingObjects;
    private float numberOfCopies;

    //Rigidbody
    [Header("Rigidbody")]
    public Rigidbody2D _rigidbody;

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
        _rigidbody.gravityScale = 0;
        spritePlayer = GetComponent<SpriteRenderer>();
        holdingObjects = new string[2];
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
        if (moveDir.x == moveDir.y)
        {
            return;
        }
        if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y))
        {
            if (moveDir.x < 0)
            {
                spritePlayer.sprite = ModelsSprites[2];
            }
            else
            {
                spritePlayer.sprite = ModelsSprites[3];
            }
        }
        else
        {
            if (moveDir.y < 0)
            {
                spritePlayer.sprite = ModelsSprites[1];
            }
            else
            {
                spritePlayer.sprite = ModelsSprites[0];
            }
        }
    }


}

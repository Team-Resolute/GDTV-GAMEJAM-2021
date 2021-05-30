using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PlayerMovement : MonoBehaviour
{
    private enum Facing {None, Right, Left}
    private Facing facing = Facing.Right;

    private Vector3 inputVector = default;
    private Vector3 moveVector = default;
    private bool facingRight = true;

    private float jumpFuel = 0f;
    [SerializeField] private float maxJumpFuel = 0.5f;
    private bool jumping = false;
    private bool jumpStart = false;
    private bool jumpButtonPressed = false;
    private bool falling = false;
    private float restTimer = 0f;
    private const float restTimerReset = 0.3f;
    private float hoverTimer = 0f;
    private const float hoverTimerReset = 10f;
    private bool hovering = false;
    
    
    private float acceleration = 1f;
    private float maxSpeed = 12f;
    private float currentSpeed = 0f;
    private float groundCheckDistance = default;
    
    [SerializeField] private float verticalSpeed = 18f;

    private float inputThreshold = 0.1f;
    [SerializeField] private LayerMask groundLayers;
    private Rigidbody body = default;

    [SerializeField] private Transform model;
    [SerializeField] private Transform center;
    
        
    void Start()
    {
        body = GetComponent<Rigidbody>();
        groundCheckDistance = Vector3.Distance(center.position, transform.position) + 1f;
    }
    
    void Update()
    {
        //moveVector = new Vector3(0, moveVector.y, 0);
        inputVector = Vector3.zero;
        CollectInput();
        StandUpright();
        
        PlanJumpMovement();
        PlanHorizontalMovement();
        Move();
    }

    void StandUpright()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
        transform.position = new Vector3(transform.position.x,
            transform.position.y,
            0f);
    }

    private void FixedUpdate()
    {
        //PlanJumpMovement();
        //PlanHorizontalMovement();
        //Move();
    }

    void PlanHorizontalMovement()
    {
        if (inputVector.x > inputThreshold || inputVector.x < -inputThreshold)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration, 0f, maxSpeed);
            
            moveVector.x = transform.right.x * (inputVector.x * currentSpeed * Time.deltaTime);
            if (moveVector.x > 0) { facing = Facing.Right;}
            if (moveVector.x < 0) { facing = Facing.Left;}
            model.transform.right = Vector3.right * Mathf.Sign(inputVector.x);
        }
        else
        {
            currentSpeed = 0f;
        }
    }

    void Move()
    {
        //body.MovePosition(transform.position + (moveVector));
        transform.Translate(moveVector);
    }

    private void PlanJumpMovement()
    {
        if (jumpButtonPressed && GroundCheck() && !jumpStart && !jumping && !falling && restTimer <= 0f)
        {
            jumpStart = true;
            jumping = true;
            jumpFuel = maxJumpFuel;
            falling = false;
            restTimer = restTimerReset;
            hoverTimer = hoverTimerReset;
            body.useGravity = true;
            hovering = false;
        }
        
        if (jumpStart && !GroundCheck())
        {
            jumpStart = false;
        }

        if (jumping && jumpFuel < 0f && hoverTimer >= 0)
        {
            hoverTimer -= Time.deltaTime;
            body.useGravity = false;
            hovering = true;
            moveVector.y = 0;
        }
        
        
        if ((GroundCheck() && !jumpStart && jumping) ||     //if you land on ground
            (jumping && !jumpButtonPressed) ||              //if you stop pressing the jump button
            (jumpFuel < 0f && hoverTimer < 0))              //if you've already been jumping too long
        {
            body.useGravity = true;
            jumpStart = false;
            jumpFuel = 0f;
            moveVector.y = 0;
            jumping = false;
            falling = true;
            hovering = false;
        }

        if (falling && GroundCheck())
        {
            falling = false;
            jumping = false;
            jumpStart = false;
            hovering = false;
            jumpFuel = 0;
            moveVector.y = 0;
        }
        
        if (jumping && !hovering)
        {
            jumpFuel -= Time.deltaTime;
            moveVector.y = verticalSpeed * Time.deltaTime;
        }

        if (GroundCheck() && !jumping && !jumpStart && !falling && !hovering && restTimer > 0)
        {
            restTimer -= Time.deltaTime;
        }
        
    }

    private void CollectInput()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if (facing == Facing.Right && !GroundCheck() && !(jumping || falling))
        {
            inputVector.x = Mathf.Min(inputVector.x, 0f);
        }
        else if (facing == Facing.Left && !GroundCheck() && !(jumping || falling))
        {
            inputVector.x = Mathf.Max(0, inputVector.x);
        }

        if (!falling && (inputVector.y > 0.5f || Input.GetButton("Jump")))
        {
            jumpButtonPressed = true;
        }
        else
        {
            jumpButtonPressed = false;
        }

        if (inputVector.x < 0.1f && inputVector.x > -0.1f)
        {
            moveVector.x = 0f;
        }
        
        
    }

    private bool GroundCheck()
    {
        Vector3 dir = Vector3.down + (Vector3.right * Mathf.Sign(inputVector.x));
        bool hit = Physics.Raycast(center.position, dir, groundCheckDistance, groundLayers);
        if (!hit)
        {
            dir = Vector3.down + (Vector3.right * Mathf.Sign(-inputVector.x));
            hit = Physics.Raycast(center.position, dir, groundCheckDistance, groundLayers);
        }

        return hit;
    }

    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
    }
    
}

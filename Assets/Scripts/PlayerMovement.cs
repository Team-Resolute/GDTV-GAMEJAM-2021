using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PlayerMovement : MonoBehaviour
{
    private enum Facing {None, Right, Left}
    private Facing facing = Facing.Right;

    private Vector3 inputVector;
    private Vector3 moveVector;
    private bool facingRight = true;
    private bool jumping = false;
    private bool jumpCommand = false;
    
    private float acceleration = 1f;
    private float maxSpeed = 20f;
    private float currentSpeed = 0f;

    // Jumpimg
    [SerializeField] private float verticalSpeed = 2f;

    private float inputThreshold = 0.1f;
    [SerializeField] private LayerMask groundLayers;
    private Rigidbody body = default;

    [SerializeField] private Transform model;
    [SerializeField] private Transform center;
    void Start()
    {
        //groundLayers = ~groundLayers;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector3.zero;
        inputVector = Vector3.zero;
        CollectInput();
        StandUpright();
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
        MoveHorizontally();
        Jump();
    }

    void MoveHorizontally()
    {
        if (inputVector.x > inputThreshold || inputVector.x < -inputThreshold)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration, 0f, maxSpeed);
            
            moveVector.x = transform.right.x * (inputVector.x * currentSpeed * Time.deltaTime);
            body.MovePosition(transform.position + moveVector);
            if (moveVector.x > 0) { facing = Facing.Right;}
            if (moveVector.x < 0) { facing = Facing.Left;}
            model.transform.right = Vector3.right * Mathf.Sign(inputVector.x);
        }
        else
        {
            currentSpeed = 0f;
        }
    }

    private void Jump()
    {
        //TODO: Add jump functionality
        // Implementation is only partial: we should have a jumping state to 1 and we should detect landing to put it back to 0
        // Furthermore it does not allow horizontal movement at the same time
        if(jumpCommand == true && GroundCheck() == true)
        {
            body.velocity += Vector3.up * verticalSpeed;
            jumpCommand = false;
        }

    }

    private void CollectInput()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if (facing == Facing.Right && !GroundCheck())
        {
            inputVector.x = Mathf.Min(inputVector.x, 0f);
        }
        else if (facing == Facing.Left && !GroundCheck())
        {
            inputVector.x = Mathf.Max(0, inputVector.x);
        }

        if (!jumping && (inputVector.y > 0.5f || Input.GetButton("Jump")))
        {
            jumpCommand = true;
        }
    }

    private bool GroundCheck()
    {
        Vector3 dir = Vector3.down + (Vector3.right * Mathf.Sign(inputVector.x));
        bool hit = Physics.Raycast(center.position, dir, 3f, groundLayers);
        if (!hit)
        {
            dir = Vector3.down + (Vector3.right * Mathf.Sign(-inputVector.x));
            hit = Physics.Raycast(center.position, dir, 3f, groundLayers);
        }

        return hit;
    }

    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
    }
    
}

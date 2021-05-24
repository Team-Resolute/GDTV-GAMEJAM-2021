using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 inputVector;
    private Vector3 moveVector;
    private bool jumpCommand = false;
    
    private float acceleration = 1f;
    private float maxSpeed = 20f;
    private float currentSpeed = 0f;

    private float inputThreshold = 0.1f;
    [SerializeField] private LayerMask groundLayers;
    private Rigidbody rigidbody = default;

    [SerializeField] private Transform model;
    
    void Start()
    {
        //groundLayers = ~groundLayers;
        rigidbody = GetComponent<Rigidbody>();
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
    }

    private void FixedUpdate()
    {
        MoveHorizontally();
    }

    void MoveHorizontally()
    {
        if (inputVector.x > inputThreshold || inputVector.x < -inputThreshold)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration, 0f, maxSpeed);
            
            moveVector.x = transform.right.x * (inputVector.x * currentSpeed * Time.deltaTime);
            rigidbody.MovePosition(transform.position + moveVector);
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
    }

    private void CollectInput()
    {
        if (GroundCheck()==false) { return;}
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        if (inputVector.y > 0.5f || Input.GetButton("Jump"))
        {
            jumpCommand = true;
        }
    }

    private bool GroundCheck()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(transform.position, Vector3.down, out hitInfo, 3f, groundLayers);
        if (hit) {Debug.Log(hitInfo.collider.name);}

        
        //TODO: Fix ground checks
        return true;
    }

    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
    }
    
}

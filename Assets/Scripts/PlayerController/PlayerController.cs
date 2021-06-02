using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Timers;
using Sound;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    private Vector2 moveInput;

    public LayerMask groundLayer;
    public Transform groundPoint;
    public Transform headCheckPoint;
    public Transform midCheckPoint;
    private bool isGrounded;
    private bool PlayImpactSound;

    private float verticalVelocitytimer = 0f;
    private Animator animator;

    private float direction = 1;

    private void IncreaseVerticalVelocityTimer()
    {
        if (Math.Abs(body.velocity.y) > 1f)
        {
            verticalVelocitytimer += 0.1f;
        }
        else
        {
            verticalVelocitytimer = 0;
        }
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        //Debug.Log(body.velocity);
        
        //Update the timer of how long the player have been on the air
        IncreaseVerticalVelocityTimer();
        
        // Horizontal movement
        moveInput.x = Input.GetAxis("Horizontal");

        float dir = 1 * Mathf.Sign(moveInput.x);
        bool isBlocked = false;
        
        if (moveInput.x != 0)
        {
            if (Physics.Raycast(groundPoint.position, Vector3.right * dir, .6f, groundLayer)
            || Physics.Raycast(headCheckPoint.position, Vector3.right * dir, .6f, groundLayer)
            || Physics.Raycast(midCheckPoint.position, Vector3.right * dir, .6f, groundLayer))
            {
                isBlocked = true;
            }
        }

        float horizMovement = 0;
        if (!isBlocked)
        {
            horizMovement = moveInput.x * moveSpeed;
        }
        
        body.velocity = new Vector3(horizMovement, body.velocity.y, 0);
        

        // Jumping
        RaycastHit hit;
        if(Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, groundLayer))
        {
            if (!isGrounded && verticalVelocitytimer > 1f)
            {
                animator.SetFloat("Vertical", 0f);
                SoundManager.PlaySound(SoundManager.Sound.JumpImpact, transform.position);   
            }
            
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(Input.GetButtonDown("Jump") && isGrounded && Time.timeScale > 0f)
        {
            animator.SetFloat("Vertical", 1f);
            SoundManager.PlaySound(SoundManager.Sound.Jump, transform.position);
            body.velocity += new Vector3(0f, jumpForce, 0f);
        }

        // Character facing right or left

        
        AdjustRotation(body.velocity.x);
        StayOn2DPlane();
        
        //Idle or forwardAnimationCheck
        IdleOrForwardCheck();
    }

    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
    }

    private void IdleOrForwardCheck()
    {
        if (Mathf.Abs(body.velocity.x) > 0 && isGrounded)
        {
            animator.SetFloat("Horizontal", Mathf.Abs(Input.GetAxis("Horizontal")));
        }
        else
        {
            animator.SetFloat("Horizontal", -1f);
        }
    }
    
    void AdjustRotation(float movement)
    {
        if (movement > 0) { direction = 1;}
        if (movement < 0) { direction = -1;}

        transform.rotation = Quaternion.identity;
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);

        if (direction == -1) { rot.y = 180f; }
        if (direction == 1) { rot.y = 0f; }

        transform.rotation = quaternion.Euler(rot.x, rot.y, rot.z);
    }

    void StayOn2DPlane()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y,
            0f);
    }
    
    
}

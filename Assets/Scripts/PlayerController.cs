using System;
using System.Collections;
using System.Collections.Generic;
using Sound;
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
    private bool isGrounded;
    private bool PlayImpactSound;

    private float verticalVelocitytimer = 0f;
    

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
    
    void Update()
    {
        //Debug.Log(body.velocity);
        
        //Update the timer of how long the player have been on the air
        IncreaseVerticalVelocityTimer();
        
        // Horizontal movement
        moveInput.x = Input.GetAxis("Horizontal");
        body.velocity = new Vector3(moveInput.x * moveSpeed, body.velocity.y, 0);

        // Jumping
        RaycastHit hit;
        if(Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, groundLayer))
        {
            if (!isGrounded && verticalVelocitytimer > 1f)
            {
                SoundManager.PlaySound(SoundManager.Sound.JumpImpact, transform.position);   
            }
            
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            
            SoundManager.PlaySound(SoundManager.Sound.Jump, transform.position);
            body.velocity += new Vector3(0f, jumpForce, 0f);
        }

        // Character facing right or left

        
        if(transform.rotation == Quaternion.Euler(0,0,0) && body.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else if(transform.rotation == Quaternion.Euler(0,180,0) && body.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
    }

    private void PlayWalkLoopSound()
    {
        if (body.velocity.x > 0 && isGrounded)
        {
            
        }
    }

}

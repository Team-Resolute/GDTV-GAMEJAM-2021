using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    private Vector2 moveInput;

    public LayerMask groundLayer;
    public Transform groundPoint;
    private bool isGrounded;

    void Update()
    {
        // Horizontal movement
        moveInput.x = Input.GetAxis("Horizontal");
        body.velocity = new Vector3(moveInput.x * moveSpeed, body.velocity.y, 0);

        // Jumping
        RaycastHit hit;
        if(Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
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

}

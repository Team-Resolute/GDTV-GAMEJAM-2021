using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (Input.GetButton("Jump"))
        {
            animator.SetFloat("Vertical", 1f);
        } else
        {
            animator.SetFloat("Vertical", 0f);
        }
        
    }

}

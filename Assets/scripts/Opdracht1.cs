using System;
using UnityEngine;

public class Opdracht1 : MonoBehaviour
{
    public float acceleration;
    public float maxVelocity;
    private float timePassed;
    private Vector3 deraction;
    
    private Rigidbody rb;
    private Animator animator ;

    private bool start;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        deraction = new(1, 0, 0);
    }

    private void Update()
    {
        HandlePlayerInput();
        HandleAnimations();

        if (start)
        {
            timePassed += Time.deltaTime;
        }
    }

    private void HandlePlayerInput()
    {
        if (Input.GetKey(KeyCode.Space) && timePassed < 3f)
        {
            if(!start) start = true;
            
            if (rb.velocity.x < maxVelocity)
            {
                rb.velocity += deraction * acceleration;
                
                if (rb.velocity.x > maxVelocity)
                {
                    rb.velocity -= deraction * maxVelocity;
                }
            }

            timePassed = 0f;
        }
    }

    private void HandleAnimations()
    {
        if (rb.velocity.x <= 0f && timePassed < 30f)
        {
            animator.SetFloat("Run", 0f);
        }
        else if (rb.velocity.x > 0f && timePassed < 30f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if (timePassed >= 30f)
        {
            animator.SetTrigger("Death");
        }
    }
}
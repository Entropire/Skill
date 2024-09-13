using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Opdracht1 : MonoBehaviour
{
    public GameObject startUI;
    public GameObject wonUI;
    public TMP_Text timeText;

    public float acceleration;
    public float maxVelocity;
    
    private Rigidbody rb;
    private Animator animator ;
    
    private bool playing;
    private bool isDead;
    private bool won;
    
    private float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandlePlayerInput();
        HandleAnimations();
        
        if (!won && !isDead && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += Vector3.forward * acceleration;
            
            if(rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
        }
        
        if (playing && !isDead && !won)
        {
            startUI.SetActive(false);
            time += Time.deltaTime;
            timeText.text = time.ToString("0.00");
        }

        if (time > 8f)
        {
            isDead = true;
            time = 0f;
            wonUI.gameObject.SetActive(true);
        }

        if (rb.transform.position.z > 16)
        {
            won = true;
            time = 0f;
            wonUI.SetActive(true);
        }
    }
    
    private void HandlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!playing && !isDead)
            {
                playing = true;
                animator.ResetTrigger("Idle");
            }

            if (playing && won || isDead)
            {
                transform.position = new(0f,0f, -18f);
                playing = false;
                isDead = false;
                won = false;
                animator.SetTrigger("Idle");
                animator.ResetTrigger("Death");
                animator.ResetTrigger("Won");
                wonUI.SetActive(false);
                startUI.SetActive(true);
            }
        }
    }
    
    private void HandleAnimations()
    {
        if (!won && !isDead && rb.velocity.z <= 0f)
        {
            animator.SetFloat("Run", 0f);
        }
        else if (!won && !isDead && rb.velocity.z > 0f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if (!won && isDead)
        {
            animator.SetTrigger("Death");
        }
        else if(won)
        {
            animator.SetTrigger("Won");
        }
    }
}


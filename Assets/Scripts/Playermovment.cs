using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Playermovment : MonoBehaviour
{
    public float jumpHight;
    public float speed;

    private float horizontal;
    private bool facingRight = true;
    private float walk;


    //private bool canDash;
    //private bool isDashing;
    //private float dashPow = 10f;
    //private float dashTime = 0.2f; 


    bool fall = false;
    bool onGround = false;

   
    public Animator animator;
    Rigidbody2D RB;

    public static Playermovment instance;
    public bool isAttacking = false;

    private void Awake()
    {
        instance = this;
    }


    //[SerializeField]
    //private Rigidbody2D RB;
    // <summary>
    //[SerializeField]
    // </summary>
    //private Transform groundCheck;
    //[SerializeField]
    //private LayerMask groundlayer;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Falling();
        Attack();
        Flip();



        if (Input.GetButtonDown("Jump") && onGround)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpHight);
            onGround = false;
            animator.SetBool("IsJumping", !onGround);
        }
        if (Input.GetButtonUp("Jump") && RB.velocity.y > 0f)
        {
            RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * 0.5f);
        }
      
    }

    private void FixedUpdate()
    {
        
        RB.velocity = new Vector2(horizontal * walk, RB.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(RB.velocity.x));
        animator.SetFloat("yVelocity", (RB.velocity.y));
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        fall = false;
        animator.SetBool("Fall", fall);
        animator.SetBool("IsJumping", !onGround);
        
        
    }

    private void Flip() 
    {
        if(facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

    private void Falling()
    {
        if ((RB.velocity.y < 0))
        {
            fall = true;
            animator.SetBool("Fall", fall);
        }
        else
        {
            fall = false;
            animator.SetBool("Fall", fall);
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isAttacking)
        {
            isAttacking = true;

        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_2") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3"))
        {
            walk = speed / 10f;
        }
        else
        {
            walk = speed;
        }
    }

}

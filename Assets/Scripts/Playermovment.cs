using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Playermovment : MonoBehaviour
{
    private float horizontal;
    public float speed;
    public float jumpHight;
    private bool facingRight = true;

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
        //animator.SetFloat("Speed", Mathf.Abs(horizontal));

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
            fall = true;
            animator.SetBool("Fall",  !fall);

        }

        //if (!Input.GetButtonUp("Jump") && RB.velocity.y > 0f || !Input.GetButtonUp("Jump") && !onGround)
        //{
        //    fall = true;
        //    animator.SetBool("Fall",  fall);
        //}




    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(horizontal * speed, RB.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(RB.velocity.x));
        animator.SetFloat("yVelocity", (RB.velocity.y));
    }

    //bool IsGrounded()
    //{
    //animator.SetBool("IsJumping", false);
    //  return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    //}

    //public void Jump (bool jumping)
    //{
    //   animator.SetBool("IsJumping", jumping);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        fall = false;
        animator.SetBool("Fall", !fall);
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
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isAttacking)
        {
            isAttacking = true;
        }
    }


}

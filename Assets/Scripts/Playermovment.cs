using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Playermovment : MonoBehaviour
{
    public float jumpHight;
    public float speed;

    private float horizontal;
    private bool facingRight = true;
    public float walk;



    //private bool canDash;
    //private bool isDashing;
    //private float dashPow = 10f;
    //private float dashTime = 0.2f; 


    bool fall = false;
    bool onGround = false;

   
    public Animator animator;
    Rigidbody2D RB;

    //public static Playermovment instance;
    [HideInInspector]
    public bool isAttacking = false;
    [HideInInspector]
    public bool KfromRight;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    


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
        //Attack();
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

    private void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            RB.velocity = new Vector2(horizontal * walk, RB.velocity.y);
            animator.SetFloat("xVelocity", Math.Abs(RB.velocity.x));
            animator.SetFloat("yVelocity", (RB.velocity.y));
        }
        else
        {
            if (KfromRight == true)
            {
                RB.velocity = new Vector2(-KBForce, KBForce);
            }
            if (KfromRight == false)
            {
                RB.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }
            
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
    

}

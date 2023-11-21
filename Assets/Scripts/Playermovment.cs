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

    public Vector2 defaultColliederOffset;
    public Vector2 defaultColliederSize;

    public Vector2 dashColliederOffset;
    public Vector2 dashColliederSize;



    

    //private bool canDash;
    //private bool isDashing;
    //private float dashPow = 10f;
    //private float dashTime = 0.2f; 


    bool fall = false;
    bool onGround = false;

   
    public Animator animator;
    Rigidbody2D RB;
    public BoxCollider2D c;
    public Transform headCheck;
    public float headCheckLenght;
    public LayerMask groundMask;

    //public static Playermovment instance;
    [HideInInspector]
    public bool isAttacking = false;

    

    [Header("Dashing")]
    private bool isDashing=false;
    private bool canDash=true;
    private float dashPower= 24f;
    private float dashDuration = 0.25f;
    private float dashCooldown = 0.75f;

   



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
        c = GetComponent<BoxCollider2D>();

        c.size = defaultColliederSize;
        c.offset = defaultColliederOffset;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        bool isHeadHitting = collAbov();

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
        if((Input.GetKeyDown(KeyCode.C)|| isHeadHitting) && canDash)
        {
            StartCoroutine(Dash());
        }
        

        


    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
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

    private IEnumerator Dash()
    {
        bool isHeadHitting = collAbov();
        float dashD;
        //Debug.Log("DASHING");
        canDash = false;
        isDashing = true;
        c.size = dashColliederSize;
        c.offset = dashColliederOffset;
        float defaultGravity = RB.gravityScale;
        RB.gravityScale = 0f;
        RB.velocity = new Vector2(transform.localScale.x * dashPower * (speed / 2), 0f);
        //trail maybe
        animator.SetBool("isDashing", isDashing);

        
        if (isHeadHitting)
        {
            Debug.Log("SOMTHING IS ABOVE");
            dashD = dashDuration * 1.5f;
            yield return new WaitForSeconds(dashD);
            RB.gravityScale = defaultGravity;
            isDashing = false;
            animator.SetBool("isDashing", isDashing);
            c.size = defaultColliederSize;
            c.offset = defaultColliederOffset;

            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
        else
        {
            dashD = dashDuration;
            yield return new WaitForSeconds(dashD);
            RB.gravityScale = defaultGravity;
            isDashing = false;
            animator.SetBool("isDashing", isDashing);
            c.size = defaultColliederSize;
            c.offset = defaultColliederOffset;

            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
        
    }
    bool collAbov()
    {
        
        bool hit = Physics2D.Raycast(headCheck.position, Vector2.up, headCheckLenght, groundMask);

        return hit;
    }
    private void OnDrawGizmos()
    {
        if (headCheck == null) return;

        Vector2 from = headCheck.position;
        Vector2 to = new Vector2(headCheck.position.x, headCheck.position.y + headCheckLenght);

        Gizmos.DrawLine(from, to);
    }


}


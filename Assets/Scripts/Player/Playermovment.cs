using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class Playermovment : MonoBehaviour
{
    [Header("Basic Stat Settings")]
    public float jumpHight;
    public float speed;
    private float walk;

    private float horizontal;
    private bool facingRight = true;
    public Color defaultColor;
    public Color gameOver;

    [Header("Colliders settings| Default")]
    public Vector2 defaultColliederOffset;
    public Vector2 defaultColliederSize;

    [Header("Colliders settings| Dash")]
    public Vector2 dashColliederOffset;
    public Vector2 dashColliederSize;

    bool fall = false;
    bool onGround = false;

    [HideInInspector]
    public bool isFinished;

    [Header("Referenced Attributes")]
    public Animator animator;
    Rigidbody2D RB;
    public BoxCollider2D c;
    public Transform headCheck;
    public float headCheckLenght;
    public LayerMask groundMask;
    private SpriteRenderer sprite;

    [Header("Knockback")]
    public Transform center;
    public float KnockbackForce;
    public bool kbd= false ;
    public float kbDuration;
    public Color kb_color;



    [Header("Dashing")]
    private bool isDashing=false;
    private bool canDash=true;
    public float dashPower= 24f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 0.75f;
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        c = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

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
        Flip();

        if (Input.GetButtonDown("Jump") && onGround || Input.GetKeyDown(KeyCode.UpArrow) && onGround)
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
           animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Transition_end"))
        {
            walk = speed / 20f;
        }
        else
        {
            walk = speed;
        }
        if (Input.GetKeyDown(KeyCode.C) && canDash)
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
        if (!kbd)
        {
            RB.velocity = new Vector2(horizontal * walk, RB.velocity.y);
            animator.SetFloat("xVelocity", Math.Abs(RB.velocity.x));
            animator.SetFloat("yVelocity", RB.velocity.y);
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
        if (RB.velocity.y < -1)
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
        //Debug.Log("DASHING");
        canDash = false;
        isDashing = true;
        c.size = dashColliederSize;
        c.offset = dashColliederOffset;
        float defaultGravity = RB.gravityScale;
        RB.gravityScale = 0f;
        RB.velocity = new Vector2(transform.localScale.x * dashPower * (speed / 2), 0f);
        animator.SetBool("isDashing", isDashing);

        yield return new WaitForSeconds(dashDuration);
        RB.gravityScale = defaultGravity;
        isDashing = false;
        animator.SetBool("isDashing", isDashing);
        c.size = defaultColliederSize;
        c.offset = defaultColliederOffset;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
    public void Knockback(Transform t)
    {
        var dir = center.position - t.position;
        //Debug.Log(dir);
        kbd = true;
        RB.velocity = dir.normalized * KnockbackForce;
        sprite.color = kb_color;
        //animator.SetBool("Hit", true);
        StartCoroutine(Unknockback());
    }
    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(kbDuration);
        kbd = false;
        deadOrAlive();
        //animator.SetBool("Hit", false);
    }

    private void deadOrAlive()
    {
        if (isFinished)
        {
            sprite.color = gameOver;
            GetComponent<Playermovment>().enabled = false;
        }
        else
        {
            sprite.color = defaultColor;
        }
    }



}


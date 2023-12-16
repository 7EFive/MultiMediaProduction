using System;
using System.Collections;
//using Unity.VisualScripting;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField] ParticleSystem walkParticles;
    [SerializeField] ParticleSystem chargeParticles;
    [Header("Basic Stat Settings")]
    public float jumpHight;
    public float speed;
    private float walk;

    private float horizontal;
    private bool facingRight = true;
    public Color defaultColor;
    public Color gameOver;
    public bool stayOnGround;

    [Header("Colliders settings| Default")]
    public Vector2 defaultColliederOffset;
    public Vector2 defaultColliederSize;

    [Header("Colliders settings| Old Form")]
    public Vector2 oldFormColliederOffset;
    public Vector2 oldFormColliederSize;

    [Header("Colliders settings| Dash")]
    public Vector2 dashColliederOffset;
    public Vector2 dashColliederSize;

    public bool fall = false;
    public bool onGround = true;
    public bool charging=false;
    public bool ult_press = false;

    [HideInInspector]
    public bool isFinished=false;
    public bool older=false;

    [Header("Referenced Attributes")]
    public Animator animator;
    Rigidbody2D RB;
    public BoxCollider2D c;
    public Transform headCheck;
    public float headCheckLenght;
    public LayerMask groundMask;
    private SpriteRenderer sprite;
    public DealDamage swing;
    public PlayerHealth health;

    [Header("Knockback")]
    public Transform center;
    public float KnockbackForceX;
    public float KnockbackForceY;
    public bool pkbd = false;
    public bool kbd= false;
    public float kbDuration;
    public Color kb_color;
    private float KBF_x;
    private float KBF_y;

    [Header("Dashing")]
    public bool isDashing=false;
    public bool canDash=true;
    public float dashPower;
    public float dashDuration;
    public float dashCooldown;
    
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        c = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        swing = GetComponent<DealDamage>();
        c.size = defaultColliederSize;
        c.offset = defaultColliederOffset;
    }
  


    // Update is called once per frame
    void Update()
    {
        //Return value of is Dashing
        if (isDashing)
        {
            return;
        }
        Falling();
        AttackCheak();
        horizontal = Input.GetAxisRaw("Horizontal");
        //Flip sprite 

        if(charging || isFinished ){ 
            if (facingRight) {
                facingRight = true;
                
            } 
            else {
                facingRight = false;
            }
                
        }
        else
        {
            Flip();
        }

        //Jumping
        Jumping();


        Old();
        
        if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.DownArrow)) && canDash && !older)
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

        //regular playermovment
        if (!kbd && !pkbd && !charging)
        {
            RB.velocity = new Vector2(horizontal * walk, RB.velocity.y);
            if(RB.velocity[1] != 0 && onGround) {
                createWalkParticles();
            }
            animator.SetFloat("xVelocity", Math.Abs(RB.velocity.x));
            animator.SetFloat("yVelocity", RB.velocity.y);
        }

        //old playermovment
        if(!kbd && !pkbd && older){
            RB.velocity = new Vector2(horizontal * walk/2, RB.velocity.y);
            animator.SetFloat("xVelocity", Math.Abs(RB.velocity.x));
            animator.SetFloat("yVelocity", RB.velocity.y);
        }

        //No movement stats
        if((charging && (!pkbd || !kbd)) || (health.isParrying && (!kbd || !pkbd)) || (health.coolDown_ult_first_anim && (!kbd || !pkbd)))
        { 
            RB.velocity = new Vector2(0, 0);
        }

        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        fall = false;
        animator.SetBool("Fall", fall);
        animator.SetBool("IsJumping", !onGround);
        animator.SetBool("Hurt", false);

        if (onGround && isFinished)
        {
            animator.SetBool("GameOver", true);
            charging = false;
            c.enabled = false;
            GetComponent<PlayerHealth>().enabled = false;
            Debug.Log("The player Collieder should be off");
            RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            int notPlayable = LayerMask.NameToLayer("GameOver");
            gameObject.layer = notPlayable;

        }
    }
    void Jumping()
    {
        if ((Input.GetButtonDown("Jump") && onGround || Input.GetKeyDown(KeyCode.UpArrow) && onGround) && !older && !stayOnGround)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpHight);
            onGround = false;
            //animator.SetBool("Grounded", onGround);
            animator.SetBool("IsJumping", !onGround);
        }
        if (Input.GetButtonUp("Jump") && RB.velocity.y > 0f)
        {
            RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * 0.5f);
        }
    }
    void AttackCheak()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_2") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Transition_end") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Transition_to_2") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Transition_to_3"))|| 
           swing.isAttacking && onGround)
        {
            walk = speed / 50f;
            stayOnGround = true;
        }
        else
        {
            walk = speed;
            stayOnGround = false;
        }
    }
    

    void Flip() 
    {
        if(facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    
    void Falling()
    {
        if (RB.velocity.y < -1)
        {
            //Debug.Log("Falling");
            fall = true;
            //onGround = false;
            animator.SetBool("Fall", fall);
        }
        else
        {
            //onGround = true;
            fall = false;
            animator.SetBool("Fall", fall);
        }
    }
   

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        //Debug.Log("DASHING");
        c.size = dashColliederSize;
        c.offset = dashColliederOffset;
        float defaultGravity = RB.gravityScale;
        RB.gravityScale = 0f;
        RB.velocity = new Vector2(transform.localScale.x * dashPower * (speed / 2), 0f);
        //Debug.Log("Is Dash");
        animator.SetBool("isDashing", isDashing);

        yield return new WaitForSeconds(dashDuration);
        RB.gravityScale = defaultGravity;
        isDashing = false;
        animator.SetBool("isDashing", isDashing);
        c.size = defaultColliederSize;
        c.offset = defaultColliederOffset;
        //Debug.Log("Stop Dashing");

        yield return new WaitForSeconds(dashCooldown);
        //Debug.Log("can Dash");
        canDash = true;
    }
    
    public void Knockback(Transform t)
    {
        var dir = center.position - t.position;
        //Debug.Log(dir);
        kbd = true;
        pkbd = false;
        KBF_x = KnockbackForceX;
        KBF_y = KnockbackForceX;
        sprite.color = kb_color;

        if (dir.x > 0)
        {
            RB.velocity = new Vector2(KBF_x, KBF_y);
        }
        else
        {
            RB.velocity = new Vector2(-KBF_x, KBF_y);
        }
        StartCoroutine(Unknockback(kbDuration));
    }
    public void KnockbackP(Transform t)
    {
        var dir = center.position - t.position;
        //Debug.Log(dir);

        kbd = false;
        pkbd = true;
        KBF_y = 0;
        sprite.color = defaultColor;
        Debug.Log("gets parry KB");
        if (dir.x > 0)
        {
            RB.velocity = new Vector2(KBF_x, KBF_y);
        }
        else
        {
            RB.velocity = new Vector2(-KBF_x, KBF_y);
        }
        StartCoroutine(Unknockback(kbDuration));

 
    }
    private IEnumerator Unknockback(float kbDur)
    {
        yield return new WaitForSeconds(kbDur);
        kbd = false;
        health.isParrying = false;
        health.canParry = true;
        pkbd = false;
        sprite.color = defaultColor;
    }


    public void Old()
    {
        if (onGround)
        {
            Charging();
        }
        if (older)
        {
            c.size= oldFormColliederSize;
            c.offset= oldFormColliederOffset;
            GetComponent<DealDamage>().enabled = false;
            animator.SetBool("Old", older);
        }
        else
        {
            animator.SetBool("Old", older);
            GetComponent<DealDamage>().enabled = true;

        }
    }
    
    //Charging Health, Time Stop or slow down
    public void Charging()
    {
        if (Input.GetKeyDown(KeyCode.V) && older && !health.punished)
        {
            charging = true;
            createChargeParticles();
            Debug.Log("createChargeParticles();");
            animator.SetBool("Charge", true);

        }
        if (Input.GetKeyUp(KeyCode.V) && older)
        {
            charging = false;
            animator.SetBool("Charge", false);
        }
        if (Input.GetKeyDown(KeyCode.V) && !older && !isDashing)
        {
            Debug.Log("Should try to charge");
            ult_press = true;
        }
        
    }

    private void createWalkParticles() {
        walkParticles.Play();
    }

    private void createChargeParticles() {
        chargeParticles.Play();
    }
    
}
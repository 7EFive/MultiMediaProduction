using System;
using System.Collections;
using System.Runtime.InteropServices;
//using Unity.VisualScripting;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [HideInInspector] public static bool isGamePaused = false;
    // particles
    [SerializeField] ParticleSystem walkParticles;
    [SerializeField] ParticleSystem chargeParticles;
    [Header("Basic Stat Settings")]
    public float jumpHight;
    public float speed;
    private float walk;
    //public bool interact;

    //horizontal movment values
    private float horizontal;
    public bool facingRight = true;
    public Color defaultColor;
    public Color gameOver;
    public bool stayOnGround;
    public bool fall = false;
    public bool onGround = true;
    // bool for chargin and ultimate press button
    public bool charging = false;
    public bool ult_press = false;

    // collieders for diffrent states
    [Header("Colliders settings| Default")]
    public Vector2 defaultColliederOffset;
    public Vector2 defaultColliederSize;

    [Header("Colliders settings| Old Form")]
    public Vector2 oldFormColliederOffset;
    public Vector2 oldFormColliederSize;

    [Header("Colliders settings| Dash")]
    public Vector2 dashColliederOffset;
    public Vector2 dashColliederSize;

    [HideInInspector]
    // bool for dead state
    public bool isFinished=false;
    // bool for weeker state
    public bool older;

    [Header("Referenced Attributes")]
    public Animator animator;
    Rigidbody2D RB;
    public BoxCollider2D c;
    [SerializeField] CapsuleCollider2D cc;
    public LayerMask groundMask;
    private SpriteRenderer sprite;
    public DealDamage swing;
    public PlayerHealth health;
    public static PlayerMain instance;

    [Header("Knockback")]
    public Transform center;
    public float KnockbackForceX;
    public float KnockbackForceY;
    //bool for regular knockback;
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
    public AudioSource moveSound;
    public bool interactionStun ;
    private void Awake()
    {
        instance = this;
    }
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
        if (isGamePaused) {
            return;
        }
        // Return value of is Dashing
        if (isDashing)
        {
            return;
        }
        if (!interactionStun)
        {
            if (!isDashing)
            {
                Falling();
                Jumping();
            }
            AttackCheak();
            horizontal = Input.GetAxisRaw("Horizontal");


            // Sprite doesn't flip while charging or dead on movment
            if (charging || isFinished || (health.coolDown_ult_first_anim || health.coolDown_ult_last_anim) || kbd)
            {
                //createChargeParticles();
                if (facingRight)
                {
                    facingRight = true;

                }
                else
                {
                    facingRight = false;
                }

            }
            else
            {
                // Flip sprite 
                Flip();
            }
            if (kbd)
            {
                health.coolDown_ult_first_anim = false;
                health.coolDown_ult_last_anim = false;
                health.watch.SetActive(false);
                ult_press = false;
                health.coolDown_Ult = false;
                health.timeFrezze = false;
            }

            // Jumping
            

            // cheking method if mainPlayer is old
            Old();

            // states cheks for dashing
            if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.DownArrow)) && canDash && !older &&
               !((animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") ||
               animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_2") ||
               animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_3") ||
               animator.GetCurrentAnimatorStateInfo(0).IsName("Transition_end") ||
               animator.GetCurrentAnimatorStateInfo(0).IsName("Transition_to_2") ||
               animator.GetCurrentAnimatorStateInfo(0).IsName("Transition_to_3")) ||
               swing.isAttacking))
            {
                StartCoroutine(Dash());
            }
        }
       
        //Physics2D.IgnoreLayerCollision(7,8);
    }

    private void FixedUpdate()
    {
        if (isGamePaused) {
            return;
        }
        // cheack for dash
        if (isDashing)
        {
            return;
        }
        if (!kbd )
        {
            if (!charging)
            {

                RB.velocity = new Vector2(horizontal * walk, RB.velocity.y);
                if (RB.velocity[1] != 0 && onGround)
                {
                    createWalkParticles();
                    //Debug.Log("Grounded?"+onGround);
                    //Debug.Log("RB.velocity[1] != 0 ?" + RB.velocity[1]);
                    if (!moveSound.isPlaying)
                    {
                        moveSound.Play();
                    }


                }
                else
                {
                    moveSound.Stop();
                }


                animator.SetFloat("xVelocity", Math.Abs(RB.velocity.x));
                animator.SetFloat("yVelocity", RB.velocity.y);
            }

            // old playermovment
            if (older)
            {
                RB.velocity = new Vector2(horizontal * walk / 2, RB.velocity.y);
                animator.SetFloat("xVelocity", Math.Abs(RB.velocity.x));
                animator.SetFloat("yVelocity", RB.velocity.y);


            }


            // No movement on specific states
            if ((charging || health.isParrying) || health.coolDown_ult_first_anim|| interactionStun)
            {
                animator.SetFloat("xVelocity", 0);
                //createChargeParticles();
                RB.velocity = new Vector2(0, 0);
            }
        }
        // regular playermovment
       

        
    }

    // collieder onGround check 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGamePaused) {
            return;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            fall = false;
            animator.SetBool("Fall", fall);
            animator.SetBool("IsJumping", !onGround);
            animator.SetBool("Hurt", false);
            //Debug.Log("Player is colliding with ground");
        }
        //onGround = true;
        //fall = false;
        //animator.SetBool("Fall", fall);
        //animator.SetBool("IsJumping", !onGround); 
        //animator.SetBool("Hurt", false);
        //animator.SetBool("Hurt", false);


        // dead state if health is under 0 and mainPlayer is on gorund
        if (onGround && isFinished)
        {
            animator.SetBool("GameOver", true);
            charging = false;
            c.enabled = false;
            GetComponent<PlayerHealth>().enabled = false;
            //Debug.Log("The mainPlayer Collieder should be off");
            RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            int notPlayable = LayerMask.NameToLayer("GameOver");
            gameObject.layer = notPlayable;

        }
    }
    // Jumping method
    void Jumping()
    {
        if (isGamePaused) {
            return;
        }
        if (Input.GetButtonDown("Jump") && onGround || Input.GetKeyDown(KeyCode.UpArrow) && onGround)
        {
            if(!older && !stayOnGround)
            {
                DealDamage.instance.jumpSound();
                RB.velocity = new Vector2(RB.velocity.x, jumpHight);
                onGround = false;
                // animator.SetBool("Grounded", onGround);
                animator.SetBool("IsJumping", !onGround);
            }

        }
        if (Input.GetButtonUp("Jump") && RB.velocity.y > 0f)
        {
            RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * 0.5f);
        }
        

    }
    // slowdown mainPlayer on attacking state
    void AttackCheak()
    {
        if (isGamePaused) {
            return;
        }
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

    // Flip method
    void Flip() 
    {
        if (isGamePaused) {
            return;
        }

        if(facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Falling method
    void Falling()
    {
        if (isGamePaused) {
            return;
        }

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
   
    // Dash method
    public IEnumerator Dash()
    {
        DealDamage.instance.dashSound();
        canDash = false;
        isDashing = true;
        // Debug.Log("DASHING");
        c.size = dashColliederSize;
        c.offset = dashColliederOffset;
        float defaultGravity = RB.gravityScale;
        RB.gravityScale = 0f;
        RB.velocity = new Vector2(transform.localScale.x * dashPower * (speed / 2), 0f);
        // Debug.Log("Is Dash");
        animator.SetBool("isDashing", isDashing);

        yield return new WaitForSeconds(dashDuration);
        RB.gravityScale = defaultGravity;
        isDashing = false;
        animator.SetBool("isDashing", isDashing);
        c.size = defaultColliederSize;
        c.offset = defaultColliederOffset;
        // Debug.Log("Stop Dashing");

        yield return new WaitForSeconds(dashCooldown);
        // Debug.Log("can Dash");
        canDash = true;
    }
    // Knockback method
    public void Knockback(Transform t)
    {
        if (isGamePaused) {
            return;
        }
        var dir = center.position - t.position;
        //Debug.Log(dir);
        kbd = true;
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
 
    // stop Knockback method
    private IEnumerator Unknockback(float kbDur)
    {
        yield return new WaitForSeconds(kbDur);
        kbd = false;
        health.isParrying = false;
        health.canParry = true;
        animator.SetBool("Hurt", kbd);
        sprite.color = defaultColor;
    }

    // Old cheack method
    public void Old()
    {
        if (isGamePaused) {
            return;
        }
        if (onGround && !interactionStun)
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
        if (isGamePaused) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.V) && older && !health.punished)
        {
            charging = true;
            //createChargeParticles();
            DealDamage.instance.chargeSound();
            animator.SetBool("Charge", true);
            
        }
        if (Input.GetKeyUp(KeyCode.V) && older)
        {
            DealDamage.instance.SoundStop();
            charging = false;
            animator.SetBool("Charge", false);
        }
        if (Input.GetKeyDown(KeyCode.V) && !older && !isDashing)
        {
            Debug.Log("Should try to healthPlayer");
            ult_press = true;
        }
        
    }
    //Particle methods
    private void createWalkParticles() {
        walkParticles.Play();
    }

    public void createChargeParticles() {
        chargeParticles.Play();
    }
    

}
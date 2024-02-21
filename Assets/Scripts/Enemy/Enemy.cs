using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [Header("Basic Stats")]
    public bool isChasing = false;
    public float chaseDistance;
    public float speed;
    private float walk = 8f;
    public float maxDistance;
    public float canFlip;
    public bool attack_start=false;
    public bool attack= false;
    //[SerializeField] GameObject AttackPoint;
    //[SerializeField] EnemyAttack AttPoi_damage;

    [Header("Knockback")]
    //bool for knockback
    public bool kbd = false;
    public float kbForceX;
    public float kbForceY;
    public float kbDuration;

    [Header("Dash Attack")]
    public bool canDash;
    bool isDashing = false;
    public float dashPower;
    public float dashDuration;
    public float dashCooldown;

    [Header("Living Status")]
    Rigidbody2D rb;
    public bool dead=false;

    [Header("Referecne Objects")]
    public GameObject player;
    //public PlayerMain Player;
    PlayerHealth stop;
    public Animator animator;

    public AudioClip[] AudioClip;
    public AudioSource audioSource;
    public AudioSource aliveSound;
    private int counterD = 0;
    public static Enemy instance;
    float defaultAnimatorSpeed;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stop = player.GetComponent<PlayerHealth>();
        //AttPoi_damage = AttackPoint.GetComponent<EnemyAttack>();
        //AttPoi_damage.attackDamage = 0;
        attack_start = false;
        attack = false;
        defaultAnimatorSpeed = animator.speed;
    }
    private void Awake()
    {
        instance = this;
    }

   
    void Update()
    {
        //Debug.Log(animator.speed);
        //cheack if time has stoped by ultimate
        if (!stop.timeFrezze)
        {
            animator.speed=defaultAnimatorSpeed;
            try
            {
                //sound
                if (!dead && !aliveSound.isPlaying)
                {
                    aliveSound.Play();
                }
                if (dead && counterD == 0)
                {
                    aliveSound.Stop();
                    deadSound();
                    counterD++;
                }
                //Movment
                Movment();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



        }
        else
        {
            animator.speed=0;
        }
        
        
    }
    //movment method
    void Movment()
    {
        //cheack if mainPlayer is in chasing distance
        Vector3 scale = transform.localScale;
        if (isChasing && !kbd)
        {
            //Player on right side
            if (player.transform.position.x > transform.position.x)
            {
                //Debug.Log("Player ditected on right side");
                scale.x = Mathf.Abs(scale.x) * -1;
                if (player.transform.position.x - maxDistance >= transform.position.x)
                {
                    //Debug.Log("Moving to the right side");
                    rb.velocity = new Vector2(walk * speed, 0);
                    //transform.Translate(walk * Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    if (!isDashing && !kbd && canDash)
                    {
                        StartCoroutine(Dash());

                    }

                    //rb.velocity = new Vector2(0, 0);
                    //transform.Translate(0, 0, 0);
                }

            }

            //Player on left side
            else if (player.transform.position.x < transform.position.x)
            {
                //Debug.Log("Player ditected on left side");
                scale.x = Mathf.Abs(scale.x) ;
                if (player.transform.position.x + maxDistance <= transform.position.x)
                {
                    //Debug.Log("Moving to the left side");
                    rb.velocity = new Vector2(walk * -speed, 0);
                    //transform.Translate(walk * Time.deltaTime * -1 * speed, 0, 0);
                }
                else
                {
                    if (!isDashing && !kbd && canDash)
                    {
                        
                        StartCoroutine(Dash());
                    }
                    //rb.velocity = new Vector2(0, 0);
                    //transform.Translate(0, 0, 0);
                }
            }
            transform.localScale = scale;
        }
        else
        {
            if (Vector2.Distance(transform.position, player.transform.position) < chaseDistance)
            {
                isChasing = true;
                //animator.SetBool("seePlayer", isChasing);
            }
            else
            {
                isChasing = false;
                //animator.SetBool("seePlayer", !isChasing);
            }

        }
    }
    //knockback method
    public void Knockback(Transform t)
    {
        if (!stop.timeFrezze)
        {
            if (dead)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
            kbd = true;

            if (player.transform.position.x >= transform.position.x)
            {
                rb.velocity = new Vector2(-kbForceX, kbForceY);
            }
            else
            {
                rb.velocity = new Vector2(kbForceX, kbForceY);
            }
            StartCoroutine(Unknockback());
        }
    }
    //end of knockback
    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(kbDuration);
        kbd = false;
        //animator.SetBool("Hit", false);
    }
    //Dash to damage mainPlayer
    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        //Debug.Log("DASHING");
        //float defaultGravity = rb.gravityScale;
        //rb.gravityScale = 0f;
        attack_start = true;
        Debug.Log("attack_start is true");
        //AttPoi_damage.attackDamage = 25;
        rb.velocity = new Vector2(transform.localScale.x * -dashPower, 0f);
        //animator.SetBool("isDashing", isDashing);
        yield return new WaitForSeconds(dashDuration);
        //rb.gravityScale = defaultGravity;
        //animator.SetBool("isDashing", isDashing);
        //attack = false;
        //animator.SetBool("Attack", attack);
        //AttPoi_damage.attackDamage = 0;
        yield return new WaitForSeconds(dashCooldown);
        Debug.Log("canDash is true");
        isDashing = false;
        canDash = true;
        
    }
    public void deadSound()
    {
        audioSource.clip = AudioClip[0];
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void SoundStop()
    {
        audioSource.Stop();

    }




}

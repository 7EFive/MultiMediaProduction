using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Basic Stats")]
    public bool isChasing;
    public float speed;
    //private float walk = 8f;
    public float dashingRange;
    public bool attack_start=false;
    public bool attack= false;
    bool isCharging = false;
    public bool looksToLeft = true;
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
    PlayerHealth stop;
    public Animator animator;
    [SerializeField] public GameObject attackPoint;

    public AudioClip[] AudioClip;
    public AudioSource audioSource;
    public AudioSource aliveSound;
    private int counterD = 0;
    public static Enemy instance;
    float defaultAnimatorSpeed;
    private void Start()
    {
        if(attackPoint!= null)
        {
            attackPoint.SetActive(false);
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stop = player.GetComponent<PlayerHealth>();
        //AttPoi_damage = AttackPoint.GetComponent<EnemyAttack>();
        //AttPoi_damage.attackDamage = 0;
        attack_start = false;
        attack = false;
        isChasing = false;
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
            StopAllCoroutines();
            animator.speed=0;
        }
    }
    //movment method
    void Movment()
    {
        //cheack if mainPlayer is in chasing distance
        if (isChasing && !kbd && canDash)
        {
            //Player on right side
            if (player.transform.position.x > transform.position.x)
            {
                //Debug.Log("Player ditected on right side");
                if (!isDashing)
                {
                    if (looksToLeft)
                    {
                        Flip();
                    }

                    if (player.transform.position.x - dashingRange > transform.position.x)
                    {
                        //Debug.Log("Moving to the right side");
                        rb.velocity = new Vector2(speed, 0);
                        //transform.Translate(walk * Time.deltaTime * speed, 0, 0);
                    }
                    else
                    {
                        if (canDash)
                        {
                            StartCoroutine(Dash());

                        }

                        //rb.velocity = new Vector2(0, 0);
                        //transform.Translate(0, 0, 0);
                    }
                }
               

            }

            //Player on left side
            else if (player.transform.position.x < transform.position.x)
            {
                //Debug.Log("Player ditected on left side");
                if (!isDashing )
                {
                    if (!looksToLeft)
                    {
                        Flip();
                    }
                    if (player.transform.position.x + dashingRange < transform.position.x
                        )
                    {

                        //Debug.Log("Moving to the left side");
                        rb.velocity = new Vector2( -speed, 0);
                        //transform.Translate(walk * Time.deltaTime * -1 * speed, 0, 0);
                    }
                    else
                    {
                        if (canDash)
                        {

                            StartCoroutine(Dash());
                        }
                    }
                }
            }
        }
    }
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        looksToLeft = !looksToLeft;
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
    }
    //Dash to damage mainPlayer
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        isCharging = true;
        animator.SetBool("dashCharge", isCharging);
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(dashDuration/2f);

        attackPoint.SetActive(true);
        attack = true;
        animator.SetBool("attack", attack);
        isCharging = false;
        animator.SetBool("dashCharge", isCharging);
        rb.velocity = new Vector2(transform.localScale.x * -dashPower, 0f);
        yield return new WaitForSeconds(dashDuration);

        attackPoint.SetActive(false);
        rb.velocity = new Vector2(0f, 0f);
        attack = false;
        animator.SetBool("attack", attack);
        yield return new WaitForSeconds(dashCooldown/2);

        //Debug.Log("canDash is true");
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown/2);

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
        animator.SetBool("AttackStart", attack);
    }




}

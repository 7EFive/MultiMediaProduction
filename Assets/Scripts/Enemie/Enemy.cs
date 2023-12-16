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

    [Header("Knockback")]
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
    public PlayerMain Player;
    PlayerHealth stop;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stop = player.GetComponent<PlayerHealth>();

    }


    void Update()
    {
        if (!stop.timeFrezze)
        {
            
            //Movment
            Movment();
            
        }
        
        
    }
    void Movment()
    {
        Vector3 scale = transform.localScale;
        if (isChasing && !kbd)
        {
            //Player on right side
            if (player.transform.position.x > transform.position.x)
            {
                //Debug.Log("Player ditected on right side");
                scale.x = Mathf.Abs(scale.x);
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
                scale.x = Mathf.Abs(scale.x) * -1;
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
    public void Knockback(Transform t)
    {
        if (!stop.timeFrezze)
        {
            if (dead)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
            kbd = true;

            if (player.transform.position.x - maxDistance >= transform.position.x)
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
    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(kbDuration);
        kbd = false;
        //animator.SetBool("Hit", false);
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        //Debug.Log("DASHING");
        //float defaultGravity = rb.gravityScale;
        //rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        //animator.SetBool("isDashing", isDashing);

        yield return new WaitForSeconds(dashDuration);
        //rb.gravityScale = defaultGravity;
        //animator.SetBool("isDashing", isDashing);

        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }




}

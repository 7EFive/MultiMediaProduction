using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.U2D;

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
    public float kbForce;
    public float kbDuration;

    [Header("Living Status")]
    Rigidbody2D rb;
    public bool dead=false;

    [Header("Referecne Objects")]
    public GameObject player;
    public Playermovment Player;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Vector3 scale = transform.localScale;

        if (isChasing && !kbd)
        {
            //Player on right side
            if (player.transform.position.x > transform.position.x )
            {
                //Debug.Log("Player ditected on right side");
                scale.x = Mathf.Abs(scale.x);
                if (player.transform.position.x - maxDistance >= transform.position.x)
                {
                    //Debug.Log("Moving to the right side");
                    transform.Translate(walk * Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    transform.Translate(0, 0, 0);
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
                    transform.Translate(walk * Time.deltaTime * -1 * speed, 0, 0);
                }
                else
                {
                    transform.Translate(0, 0, 0);
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
        if (dead)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }

        kbd = true;
        //this.transform.Translate(walk * Time.deltaTSime * -1 * speed, 0, 0);

        if (player.transform.position.x - maxDistance >= transform.position.x)
        {
            rb.velocity = new Vector2(-kbForce, 2);
        }
        else
        {
            rb.velocity = new Vector2(kbForce, 2);
        }
        
        StartCoroutine(Unknockback());
    }
    private IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(kbDuration);
        kbd = false;
        //animator.SetBool("Hit", false);
    }



}

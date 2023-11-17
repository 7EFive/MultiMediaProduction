using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public bool isChasing;
    public float chaseDistance;
    public float speed;
    private float walk = 8f;
    public Animator animator;
    //private bool lookRight = true;
    public Rigidbody2D rb2d;


    private void Start()
    {

        //RidgedBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //animator.SetFloat("Speed", speed);
        //animator.SetFloat("Speed", speed);
    }


    void Update()
    {
        Vector3 scale = transform.localScale;
 
        if (isChasing)
        {
            if(player.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
                transform.Translate(walk * Time.deltaTime*speed, 0, 0);
                //RidgedBody2D.velocity = new Vector2(transform.position.x * -speed,0f);
                //transform.position += Vector3.left * walk * speed* Time.deltaTime;
            }
            else if (player.transform.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
                transform.Translate(walk * Time.deltaTime * -1*speed, 0, 0);

                //RidgedBody2D.velocity = new Vector2(transform.position.x * speed, 0f);
                //transform.position += Vector3.right * walk* speed * Time.deltaTime;

            }
            else
            {
                transform.Translate(0, 0, 0);
            }
            transform.localScale=scale;
            
        }
        else
        {
            if(Vector2.Distance(transform.position, player.transform.position) < chaseDistance)
            {
                isChasing = true;
               //animator.SetBool("seePlayer", isChasing);
            }else
            {
                isChasing = false;
                //animator.SetBool("seePlayer", !isChasing);
            }
        }

    }
  

}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public Playermovment Player;
    public bool isChasing = false;
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

        if (isChasing==true)
        {
            //Player on right side
            if (player.transform.position.x > transform.position.x )
            {
                //Debug.Log("Player ditected on right side");
                scale.x = Mathf.Abs(scale.x);
                if (player.transform.position.x - 7 >= transform.position.x)
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
                if (player.transform.position.x + 7 <= transform.position.x)
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
   


}

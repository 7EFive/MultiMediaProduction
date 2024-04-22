using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBeh : MonoBehaviour
{
    [SerializeField] Enemy enemyBeh;
    // [SerializeField] Enemy health;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyBeh.isChasing && enemyBeh.canDash && !enemyBeh.dead) 
        {
            //Debug.Log("Patrol is on");
            Patrolling();
        }
    }

    void Patrolling()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointA.transform)
        {
            if (!enemyBeh.looksToLeft)
            {
                enemyBeh.Flip();
            }
            rb.velocity = new Vector2(-enemyBeh.speed, 0);
        }
        else
        {
            if (enemyBeh.looksToLeft)
            {
                enemyBeh.Flip();
            }
            rb.velocity = new Vector2(enemyBeh.speed, 0);
        }
        if(Vector2.Distance(transform.position, currentPoint.position)< 0.5f && currentPoint == pointB.transform)
        {
            enemyBeh.Flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            enemyBeh.Flip();
            currentPoint = pointB.transform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public Animator animator;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public float attackRange = 2f;
    public static DealDamage instance;
    public bool isAttacking = false;

    public int attackDamage;
    public float attackRate=20f;
    float nextAttackTime = 0f;
    


    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time>= nextAttackTime)
        {
            //Debug.Log(nextAttackTime);
            if ((Input.GetKeyDown(KeyCode.X) && isAttacking == false))
            {
                doDamage();
                //Debug.Log("TOOK A SWING");
                isAttacking = true;
                nextAttackTime = Time.time + 0.5f / attackRate;
                //Debug.Log(nextAttackTime + Time.time);
            }
        }
        
    }
    void doDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemieBeh>().TakeDamage(attackDamage);
            Debug.Log(enemy.name + " was damaged by you.");
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public Playermovment p;

    public float attackRange = 2f;
    public int attackDamage = 25;

    public float attackRate= 0.5f;
    float nextAttackTime = 0f;

  
    // Update is called once per frame
    void Update()
    {
        if (p.isAttacking)
        {
            doDamage();
            //nextAttackTime = Time.time + 1 / attackRate;
        }
    }
    void doDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
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

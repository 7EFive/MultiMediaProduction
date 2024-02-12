using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] int attackDamage;
    Enemy isAttacking;
    void Start()
    {
        isAttacking = enemy.GetComponent<Enemy>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (isAttacking.attack)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                Debug.Log("Damaged by dash attack");
            }
            else
            {
                player.GetComponent<PlayerHealth>().TakeDamage(0);
                Debug.Log("there is no dash attack");
            }
        }
    }

}

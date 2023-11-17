using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBeh : MonoBehaviour
{
    public Animator animator;

    public Enemy en;


    public int maxHealth=100;
    public int currentHealth=100;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt"); 

        if (currentHealth <= 0)
        {
            Die();

            
        }
    }


    void Die()
    {
        Debug.Log("Abomination commited die!");

        animator.SetBool("isDead", true);

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Enemy>().speed = 0;
        GetComponent<Enemy>().isChasing = false;

        this.enabled = false;

    }
}

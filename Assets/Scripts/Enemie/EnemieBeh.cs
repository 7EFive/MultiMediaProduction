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
        Debug.Log("STRIKED ENEMY");

        animator.SetTrigger("Hurt"); 

        if (currentHealth <= 0)
        {
            Die();
            
            
        }
    }


    void Die()
    {
        //Debug.Log("Abomination commited die!");

        animator.SetBool("isDead", true);
        this.en.dead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        this.en.chaseDistance = 0;
        this.en.isChasing = false;

        Debug.Log("ENEMY DIED");
        this.enabled = false;

    }
}

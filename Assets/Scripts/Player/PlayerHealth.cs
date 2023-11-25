using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sprite;

    public int maxHealth = 100;
    public int currentHealth = 100;

    public Transform knockBack;

    Enemy en;
    Rigidbody2D rb;

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        animator.SetBool("GameOver", true);

        GetComponent<SpriteRenderer>();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<DealDamage>().enabled = false;
        GetComponent<Playermovment>().isFinished = true;

        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        
        Debug.Log("Game Over");
    }
}
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;


    public int maxHealth = 100;
    public int currentHealth = 100;
    

    public Transform knockBack;
    

    Rigidbody2D rb;
    public Playermovment player;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        player = GetComponent<Playermovment>();
    }

    // Update is called once per frame

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

       
        if (currentHealth <= 0)
        {
            Die();
        }
        Age();
       

    }
    public void Age()
    {
        if (currentHealth <= (maxHealth / 2))
        {
            player.older = true;
        }
        else
        {
            player.older = false;
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
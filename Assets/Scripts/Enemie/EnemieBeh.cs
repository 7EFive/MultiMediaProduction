using UnityEngine;

public class EnemieHealth : MonoBehaviour
{
    public Animator animator;

    public Enemy en;

    public GameObject player;
    PlayerHealth time;
    bool dead = false;


    public int maxHealth=100;
    int currentHealth;




    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        time = player.GetComponent<PlayerHealth>();

    }
    void Update()
    {
        //enemy dies after time unfrezzes if  current Health is or lower then 0
        if (currentHealth <= 0 && !dead && !time.timeFrezze)
        {
            dead = true;
            Die();
        }
    }
    // Update is called once per frame

    // method for takingDamage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("STRIKED ENEMY");

        animator.SetTrigger("Hurt"); 
    }

    // Dead status of enemy
    void Die()
    {
        //Debug.Log("Abomination commited die!");
        
        animator.SetBool("isDead", true);
        this.en.dead = true;
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        this.en.chaseDistance = 0;
        this.en.isChasing = false;

        Debug.Log("ENEMY DIED");

        //this.enabled = false;

    }
}

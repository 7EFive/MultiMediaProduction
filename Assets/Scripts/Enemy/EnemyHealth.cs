using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public Enemy en;

    [SerializeField] GameObject player;
    PlayerHealth time;
    bool dead = false;
    public bool isEnemy;
    public int maxHealth;
    public int currentHealth;
    [SerializeField] GameObject healthBarObject;
    HealthBar healthBar;
    CanvasGroup showBar;
    //[SerializeField] GameObject attackCollider;
    [SerializeField] GameObject detectionCollider;


    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.GetComponent<RegenObject>();
        currentHealth = maxHealth;
        if (healthBarObject!=null)
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            showBar = healthBarObject.GetComponent<CanvasGroup>();
            showBar.alpha = 0f;
        }
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
    void Awake()
    {
        if (healthBar != null)
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
    }

    // Update is called once per frame

    // method for takingDamage
    public void TakeDamage(int damage)
    {
        if (healthBarObject != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            //Debug.Log("ShowHealthBar");
            if (showBar.alpha==0f)
                showBar.alpha = 1f;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        if (gameObject.GetComponent<RegenObject>()!=null)
        {
            if (time.timeFrezze)
            {
                if (healthBarObject != null)
                {
                    healthBar.UpdateHealthBar(currentHealth, maxHealth);
                }
                currentHealth -= damage;
            }
        }
        else
        {
            if (healthBarObject != null)
            {
                healthBar.UpdateHealthBar(currentHealth, maxHealth);
            }
            currentHealth -= damage;
        }
        //Debug.Log("STRIKED ENEMY");

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
        if (healthBarObject != null)
        {
            showBar.alpha = 0f;
        }
        this.en.isChasing = false;
        if (isEnemy)
        {
            en.attackPoint.SetActive(false);
            detectionCollider.SetActive(false);
            /**
             GetComponent<EnemyAttack>().attackDamage = 0;
            Debug.Log("turend off Attack Script");
            GetComponent<EnemyAttack>().enabled = false;
            **/
        }
        
        
        //Debug.Log("ENEMY DIED");
        //Enemy.instance.deadSound();
        //this.enabled = false;

    }
}

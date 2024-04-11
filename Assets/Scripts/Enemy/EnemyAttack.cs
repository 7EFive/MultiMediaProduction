using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //public Animator animator;
    //public LayerMask playerLayers;
    public Transform attackPoint;
    public static EnemyAttack instance;
    [SerializeField] GameObject player;
    PlayerHealth health;
    //BoxCollider2D hitBox;
    //public Enemy e;

    //public float attackRange;
    //public float bodyAttackRange;
    //public bool isAttacking = false;

    public int attackDamage;
    public int attackDamageBody;
    private float attackSpeed;
    public float attackCooldown;
    public bool attackOn = true;


    void Start()
    {
        health = player.GetComponent<PlayerHealth>();
        //hitBox= attackPoint.GetComponent<BoxCollider2D>();
        attackOn = true;
        GetComponent<Enemy>();
    }
    private void Awake()
    {
        instance = this;
    }

   


    //}
    //void doDamage()
    //{

    //   Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

    //  foreach (Collider2D mainPlayer in hitEnemies)
    //  {

    //     mainPlayer.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    //      Debug.Log("Enemy landed a hit");
    //  }
    //}
    //void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}

    private void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag("Player"))
        {
            attack();
        }
    }
    
    void attack()
    {
        if(Time.time > attackSpeed && !health.timeFrezze)
        {
            health.TakeDamage(attackDamage, transform);
            Debug.Log("Enemy is collieding with mainPlayer");

            attackSpeed = Time.time + attackCooldown;
        }
                    
        //if (playerKB != null)
        //{
        //    playerKB.Knockback(transform);
        //}


    }
  
    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(bodyPoint.position, bodyAttackRange, playerLayers);
    //    //var playerKB = other.collider.GetComponent<PlayerMain>();
    //
    //    if (Time.time > attackSpeed)
    //    {
    //        foreach (Collider2D mainPlayer in hitEnemies)
    //        {
    //
    //            if (PlayerHealth.timeFrezzeStatic == false)
    //            {
    //                mainPlayer.GetComponent<PlayerHealth>().TakeDamage(attackDamageBody);
                    
     //               attackSpeed = Time.time + attackCooldown;
     //           }

    //        }
     //   }
    


    //}
}

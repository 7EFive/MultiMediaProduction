using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //public Animator animator;
    public LayerMask playerLayers;
    public Transform attackPoint;
    public Transform bodyPoint;
    //public Enemy e;

    public float attackRange;
    public float bodyAttackRange;
    //public bool isAttacking = false;

    public int attackDamage;
    public int attackDamageBody;
    private float attackSpeed;
    public float attackCooldown;
    public bool attackOn = true;


    void Start()
    {
        attackOn = true;
        GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Enemy>().attack && attackOn == true)
        {
            attackFront();
            //Debug.Log("attack in front is happaning");
        }
    }


    //}
    //void doDamage()
    //{

    //   Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

    //  foreach (Collider2D player in hitEnemies)
    //  {

    //     player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    //      Debug.Log("Enemy landed a hit");
    //  }
    //}
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(bodyPoint.position, bodyAttackRange);
    }

    void attackFront()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        //var playerKB = other.collider.GetComponent<PlayerMain>();

        if (Time.time > attackSpeed)
        {
            foreach (Collider2D player in hitEnemies)
            {

                if (PlayerHealth.timeFrezzeStatic == false)
                {
                    player.GetComponent<PlayerHealth>().TakeDamage(attackDamage, transform);
                    Debug.Log("Enemy is collieding with player");
                    //if (player != null)
                    //{
                    //    player.GetComponent<PlayerMain>().Knockback(transform);
                    //}
                    //playerKB.Knockback(transform);
                    //Debug.Log("damage front activated");
                    attackSpeed = Time.time + attackCooldown;
                }

            }
            //attackSpeed = Time.time + attackCooldown;
        }
        //if (playerKB != null)
        //{
        //    playerKB.Knockback(transform);
        //}


    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(bodyPoint.position, bodyAttackRange, playerLayers);
        //var playerKB = other.collider.GetComponent<PlayerMain>();

        if (Time.time > attackSpeed)
        {
            foreach (Collider2D player in hitEnemies)
            {

                if(PlayerHealth.timeFrezzeStatic == false)   {
                    player.GetComponent<PlayerHealth>().TakeDamage(attackDamageBody, transform);
                    //Debug.Log("Enemy is collieding with player");
                    //if (playerKB != null)
                    //{
                    //    playerKB.Knockback(transform);
                    //}
                    //playerKB.Knockback(transform);
                    attackSpeed = Time.time + attackCooldown;
                }
                
            }
            //attackSpeed = Time.time + attackCooldown;
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
    //        foreach (Collider2D player in hitEnemies)
    //        {
    //
    //            if (PlayerHealth.timeFrezzeStatic == false)
    //            {
    //                player.GetComponent<PlayerHealth>().TakeDamage(attackDamageBody);
                    
     //               attackSpeed = Time.time + attackCooldown;
     //           }

    //        }
     //   }
    


    //}
}

using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //public Animator animator;
    public LayerMask playerLayers;
    public Transform attackPoint;

    public float attackRange;
    //public bool isAttacking = false;

    public int attackDamage;
    private float attackSpeed;
    public float attackCooldown;



    
    // Update is called once per frame
    //void Update()
    //{
        //if (Time.time > attackSpeed)
        //{
        //    doDamage();
        //   attackSpeed = Time.time + attackCooldown;
            
        //}
        

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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        //var playerKB = other.collider.GetComponent<PlayerMain>();

        if (Time.time > attackSpeed)
        {
            foreach (Collider2D player in hitEnemies)
            {

                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                Debug.Log("Enemy landed a hit");
                //if (playerKB != null)
                //{
                //    playerKB.Knockback(transform);
                //}
                //playerKB.Knockback(transform);
                attackSpeed = Time.time + attackCooldown;
            }
            //attackSpeed = Time.time + attackCooldown;
        }
        //if (playerKB != null)
        //{
        //    playerKB.Knockback(transform);
        //}
        

    }
}

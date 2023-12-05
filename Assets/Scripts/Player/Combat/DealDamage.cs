using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public Animator animator;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public float attackRange;
    public static DealDamage instance;
    public bool isAttacking = false;
    public bool isAttackingInAir = false;

    public int attackDamage;
    public float attackRate;
    public float nextAttackTime;

    public PlayerMain kb;

    private void Start()
    {
        kb=GetComponent<PlayerMain>();
    }

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
       
        if (Time.time>= nextAttackTime && !kb.kbd)
        {
            //Debug.Log(nextAttackTime);
            if (Input.GetKeyDown(KeyCode.X) && !isAttacking && kb.onGround &&!kb.isDashing)
            {
                doDamage();
                Debug.Log("TOOK A SWING");
                isAttacking = true;
                nextAttackTime = Time.time + attackRate;
            }
            else if(Input.GetKeyDown(KeyCode.X) && !isAttackingInAir && !kb.onGround)
            {
                isAttackingInAir = true;
                animator.SetBool("MidAirSlash", isAttackingInAir);
                doDamageB();
                nextAttackTime = Time.time + attackRate*1.2f;
                //doDamage();
                //doDamage();
                //aR = attackRange * 1.6f;

            }
            
        }

        if (kb.onGround)
        {
            isAttackingInAir = false;
            animator.SetBool("MidAirSlash", isAttackingInAir);
        }
        

        //if (Input.GetKeyDown(KeyCode.X)){
            //Debug.Log(nextAttackTime + Time.time);
        //}
    }
   
    void doDamage()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemieHealth>().TakeDamage(attackDamage);
            enemy.GetComponent<Enemy>().Knockback(transform);
            //Debug.Log(enemy.name + " was damaged by you.");
        }
        
    }
    void doDamageB()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange*1.6f, enemyLayers);


        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemieHealth>().TakeDamage(attackDamage);
            enemy.GetComponent<Enemy>().Knockback(transform);
            //Debug.Log(enemy.name + " was damaged by you.");
        }

    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

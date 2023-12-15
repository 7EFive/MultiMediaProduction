using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public Animator animator;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public float attackRange;
    public static DealDamage instance;
    public bool isAttacking = false;
    public bool canAttackingInAir = true;
    public bool isAttackingInAir = false;
    public bool enterMidAirAttack = false;

    public int attackDamage;
    public float attackRate;
    public float nextAttackTime;

    public PlayerMain kb;
    public PlayerHealth charge;
    

    private void Start()
    {
        kb=GetComponent<PlayerMain>();
        charge = GetComponent<PlayerHealth>();
    }

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        //Attack
        Attack();

        //animationReset
        animationReset();

        
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime && !kb.kbd && !isAttacking)
        {
            //Debug.Log(nextAttackTime);
            if (Input.GetKeyDown(KeyCode.X) && kb.onGround)
            {
                doDamage();
                //Debug.Log("TOOK A SWING");
                isAttacking = true;
                nextAttackTime = Time.time + attackRate * 1.05f;

            }
            else if (Input.GetKeyDown(KeyCode.X) && !enterMidAirAttack && !kb.onGround && canAttackingInAir && !isAttackingInAir)
            {
                enterMidAirAttack = true;
                animator.SetBool("MidAirSlash_enter", enterMidAirAttack);
                canAttackingInAir = false;
            }
            if (!canAttackingInAir && !isAttackingInAir && enterMidAirAttack)
            {
                isAttackingInAir = true;
                animator.SetBool("MidAirSlash", isAttackingInAir);
                enterMidAirAttack = false;
            }
            if (!enterMidAirAttack && isAttackingInAir)
            {

                isAttackingInAir = false;
                Debug.Log("TOOK A SWING MID AIR");
                doDamageB();
            }

        }
    }
   
    void doDamage()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemieHealth>().TakeDamage(attackDamage);
            enemy.GetComponent<Enemy>().Knockback(transform);
            if (!charge.timeFrezze)
            {
                charge.GetComponent<PlayerHealth>().ChargeReg(attackDamage / 2);
            }
            //Debug.Log(enemy.name + " was damaged by you.");
        }
        
    }
    void doDamageB()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange*1.6f, enemyLayers);


        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemieHealth>().TakeDamage(attackDamage/2);
            enemy.GetComponent<Enemy>().Knockback(transform);
            if (!charge.timeFrezze)
            {
                charge.GetComponent<PlayerHealth>().ChargeReg(attackDamage / 4);
            }
            //Debug.Log(enemy.name + " was damaged by you.");
        }

    }
    void animationReset()
    {
        // if (!kb.onGround)
        //{
        //    isAttacking = false;
        //}
        if (kb.onGround)
        {
            enterMidAirAttack = false;
            isAttackingInAir = false;
            canAttackingInAir = true;
            animator.SetBool("MidAirSlash_enter", enterMidAirAttack);
            animator.SetBool("MidAirSlash", isAttackingInAir);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

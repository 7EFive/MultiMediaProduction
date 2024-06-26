using UnityEngine;

public class DealDamage : MonoBehaviour
{
    //Reference values
    public Animator animator;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    // spherical range radius of attack
    public float attackRange;
    // initializing DealDamage as instance for animator scripts
    public static DealDamage instance;
    //bool for attacking
    public bool canAttack=true;
    public bool isAttacking = false;
    //bool for attacking in Air and related to it
    public bool canAttackingInAir = true;
    public bool isAttackingInAir = false;
    public bool enterMidAirAttack = false;
    
    //initger for damage dealt by mainPlayer
    public int attackDamage;
    //cooldown after attacking
    public float attackRate;
    //value that time.Delt or time.Time is supposed to pass for ending the cooldown
    public float nextAttackTime;

    //References values for knockback and healthPlayer
    public PlayerMain mainPlayer;
    public PlayerHealth healthPlayer;
    [SerializeField] GameObject groundCheckObject;
    GroundCheck groundCheck;


    //Tryin to do some musik
    public AudioClip[] AudioClip;
    public AudioSource audioSource;


    private void Start()
    {
        groundCheck = groundCheckObject.GetComponent<GroundCheck>();
        mainPlayer =GetComponent<PlayerMain>();
        healthPlayer = GetComponent<PlayerHealth>();
    }
    // for animator script
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        //Attack
        if (!mainPlayer.interactionStun)
        {
            Attack();
        }

        if (groundCheck.onGround)
        {
            canAttackingInAir = false;
        }
        else
        {
            canAttackingInAir = true;
        }
        //animationReset
        animationReset();

        
    }
    // Main Combat method
    void Attack()
    {
        //cheack if cooldown is still, if the players is knockbacked and if the palyer is Not attacking already
        if (Time.time >= nextAttackTime && !mainPlayer.kbd && !isAttacking)
        {
            canAttack = true;
            //OnGround Combat
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (groundCheck.onGround && canAttack)
                {
                    doDamage();
                    canAttack = false;
                    isAttacking = true;
                    //initialize cooldown of attacking
                    nextAttackTime = Time.time + attackRate;
                }
                else if (!enterMidAirAttack && canAttackingInAir)
                {
                    jumpAttackSound();
                    enterMidAirAttack = true;
                    isAttackingInAir = true;
                    animator.SetBool("MidAirSlash_enter", enterMidAirAttack);
                    animator.SetBool("MidAirSlash", isAttackingInAir);
                    canAttackingInAir = false;
                }
                if (isAttackingInAir && enterMidAirAttack)
                {
                    jumpAttackSound();
                    isAttackingInAir = false;
                    doDamageB();
                    enterMidAirAttack = false;
                }
            }
        }
    }
   
    //OnGround damage area
    public void doDamage()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                hitSound();
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
                if (!healthPlayer.timeFrezze)
                {
                   
                    healthPlayer.GetComponent<PlayerHealth>().ChargeReg(attackDamage / 2);
                }
            }
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().Knockback(transform);
            }
            //Debug.Log(enemy.name + " was damaged by you.");
        }
    }

    //MidAir damage area
    public void doDamageB()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange*1.6f, enemyLayers);


        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                hitSound();
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage / 2);
                if (!healthPlayer.timeFrezze)
                {
                    
                    healthPlayer.GetComponent<PlayerHealth>().ChargeReg(attackDamage / 4);
                }
            }
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().Knockback(transform);
            }
            //Debug.Log(enemy.name + " was damaged by you.");
        }
    }
    //animation reset of MidAir Combat after landing
    void animationReset()
    {
        if (groundCheck.onGround)
        {
            enterMidAirAttack = false;
            isAttackingInAir = false;
            canAttackingInAir = false;
            animator.SetBool("MidAirSlash_enter", enterMidAirAttack);
            animator.SetBool("MidAirSlash", isAttackingInAir);
        }
    }

    // drawing sphere at attack position in size of value attackRange
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    //Sounds Collection Array
    public void firstPunch()
    {
        audioSource.clip = AudioClip[0];
        audioSource.PlayOneShot(audioSource.clip);  
    }
    public void secondPunch()
    {
        audioSource.clip = AudioClip[1];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void thirdPunch()
    {
        audioSource.clip = AudioClip[2];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void chargeSound()
    {
        audioSource.clip = AudioClip[3];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void dashSound()
    {
        audioSource.clip = AudioClip[4];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void jumpSound()
    {
        audioSource.clip = AudioClip[5];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void jumpAttackSound()
    {
        audioSource.clip = AudioClip[6];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public bool runSound()
    {
        audioSource.clip = AudioClip[7];
        audioSource.Play();
        //audioSource.PlayOneShot(audioSource.clip);
        if (audioSource.isPlaying)
        {
            return true;
        }
        return false;
    }
    public void hitSound()
    {
        audioSource.clip = AudioClip[8];
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void parrySound()
    {
        audioSource.clip = AudioClip[9];
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void SoundStop()
    {
        audioSource.Stop();
        
    }
}

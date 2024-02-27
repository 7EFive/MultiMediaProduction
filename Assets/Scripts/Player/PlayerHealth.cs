using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // bool for tutorial
    public bool tutorial;
    // Reference values
    [SerializeField] ParticleSystem chargeParticles;
    [SerializeField] ParticleSystem currentChargeParticles;
    [SerializeField] Camera camera;
    PostProcess ps;
    public Animator animator;
    public LayerMask enemy;
    // health
    public int maxHealth;
    public int currentHealth;
    public int halfHealth;
    // cooldown double
    double regDamage;
    public double takeDamageCooldown;
    // parry values
    public float parry;
    public bool canParry=true;
    public bool isParrying=false;
    public float parryDuration;
    public float parryCoolDown;
    public bool ulti_start = false;
    // no Particle bool
    bool noParticle = false;

    //chargeing values
    //[HideInInspector]
    // start charging value 0
    public float charge = 0f;
    // charging speed
    public float speedCharging= 10f;
    // max healthPlayer capacity
    public float maxCharg;
    // max healthPlayer overload limit
    public float overloadCharge;
    [HideInInspector]
    // constant charging amount meter
    public float chargeLimit;
    // max charging amount meter
    public float chargeLimitMax;
    // overload cap boolean
    bool chargeCapMax=false;
    // damage on going reaching max charging amount meter
    public int punishment;
    // cooldown on healthPlayer usage
    public float punishmentTime;
    [HideInInspector]
    //punishmend state check
    public bool punished = false;
    // bool for ult animation
    public bool coolDown_Ult = false;
    public bool coolDown_ult_first_anim = false;
    public bool coolDown_ult_last_anim=false;
    // bool on time Stop
    public bool timeFrezze = false;
    int chargeRemoverCount = 0;
    [HideInInspector] public static bool timeFrezzeStatic = false;

    //Rigidbody2D rb;
    // reference values
    public DealDamage swing;
    public PlayerMain mainPlayer;
    public static PlayerHealth instance;
    public GameObject watch;
    //---
    public float parryRange;
    public Transform parryPoint;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update

    void Start()
    {
        ps = camera.GetComponent<PostProcess>();
        currentHealth = maxHealth;
        mainPlayer = GetComponent<PlayerMain>();
        swing = GetComponent<DealDamage>();
        watch.SetActive(false);
        ps.GetComponent<PostProcess>().enabled = false;

    }

    // Update is called once per frame
    private void Update()
    {

        if (PlayerMain.isGamePaused) {
            return;
        }
        // value on half health cheack for tutorial
        halfHealth = maxHealth / 2;
        // mainPlayer tutorial mode
        if (tutorial)
        {
            //Physics2D.IgnoreLayerCollision(7,8);
            currentHealth = halfHealth;
        }
        // particle color change
        var main = chargeParticles.main;
        float removeOther = chargeLimit * chargeLimit * 0.00035f;
        main.startColor = new Color(1.0f, 1.0f - removeOther, 1.0f - removeOther, 1f);

        //currentChargeParticles
        
        var currentCharge = currentChargeParticles.main;
        removeOther = charge * charge * 0.00007f;
        currentCharge.startColor = new Color(0f, 1.0f - removeOther, 0.9f, 1f);
        if (charge == 0)
        {
            currentCharge.startColor = new Color(0f, 0f, 0f, 0f);

        }

        if(!mainPlayer.kbd)
        {
            // Parrying state
            if (!mainPlayer.interactionStun)
            {
                if (mainPlayer.older && Input.GetKeyDown(KeyCode.X) && canParry && !mainPlayer.charging)
                {
                    canParry = false;
                    noParticle = true;
                    //IEnumarator to Parry
                    StartCoroutine(Parry());
                    //Debug.Log("tryed to parry...");
                }
                //cheking players state

                //Chargeing funstion young and old
                chargingFunctionOlder();
                chargingFunctionYounger();
            }
           
            if (charge < 0)
            {
                charge = 0;
            }
            Age();
        }
        
        
    }
    //taking Damage method
    public void TakeDamage(int damage)
    {
        if (PlayerMain.isGamePaused)
        {
            return;
        }
        if (Time.time > regDamage)
        {
            // no damage on successful parry
            if (isParrying)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(parryPoint.position, parryRange, enemy);

                foreach (Collider2D enemy in hitEnemies)
                {

                    if (enemy.GetComponent<Enemy>() != null)
                    {
                        enemy.GetComponent<Enemy>().Knockback(transform);
                    }

                }
                charge += parry;
                DealDamage.instance.parrySound();
                Debug.Log("Succsesfull Parry");
                //mainPlayer.KnockbackP(transform);
                regDamage = Time.time + takeDamageCooldown;
            }
            // damage registered only above 0.5
            else if (damage > 0.5f && !punished)
            {

                currentHealth -= damage;
                animator.SetBool("Hurt", true);
                if (mainPlayer != null)
                {
                    mainPlayer.Knockback(transform);
                    regDamage = Time.time + takeDamageCooldown;
                }
            }

        }



        if (currentHealth <= 0)
        {
            Die();
            currentHealth = 0;
        }
    }

    //taking damage overload method
    public void TakeDamage(int damage, Transform t)
    {
        if (PlayerMain.isGamePaused) {
            return;
        }
        if (Time.time > regDamage)
        {
            // no damage on successful parry
            if (isParrying)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(parryPoint.position, parryRange, enemy);
                
                foreach (Collider2D enemy in hitEnemies){

                    if(enemy.GetComponent<Enemy>() != null) {
                        enemy.GetComponent<Enemy>().Knockback(transform);
                    }

                }
                charge += parry;
                DealDamage.instance.parrySound();
                Debug.Log("Succsesfull Parry");
                //mainPlayer.KnockbackP(transform);
                regDamage = Time.time + 0.25;
            }
            // damage registered only above 0.5
            else if (damage > 0.5f)
            {
                
                currentHealth -= damage;
                animator.SetBool("Hurt", true);
                if (mainPlayer != null)
                {
                    mainPlayer.Knockback(t);
                }
                regDamage = Time.time + takeDamageCooldown;
            }
            
        }

        

        if (currentHealth <= 0)
        {
            Die();
            currentHealth = 0;
        }
    }
   
    // Parry method
    private IEnumerator Parry()
    {
        canParry = false;
        isParrying = true;
        animator.SetBool("Parry", isParrying);
        
        yield return new WaitForSeconds(parryDuration);
        isParrying = false;
        animator.SetBool("Parry", isParrying);
        
        yield return new WaitForSeconds(parryCoolDown);
        canParry = true;
        noParticle = false;
    }
    // Players form change method
    public void Age()
    {
        if (halfHealth >= currentHealth )
        {
            if(chargeRemoverCount == 0)
            {
                chargeRemoverCount++;
                charge = 0;
            }
            
            mainPlayer.older = true;
            mainPlayer.Old();
            //Debug.Log("should turn old");
        }
        else
        {
            
            mainPlayer.older = false;
            mainPlayer.Old();
        }
    }

    public void ChargeReg(float x) 
    {
        if (Time.time > regDamage)
        {
            charge += x;
            regDamage = Time.time + 0.2;
        }


    }
    // Charging in Old form
    public void chargingFunctionOlder()
    {
        //Main charging function, healthPlayer and chargeLimit rises
        if (mainPlayer.charging && (charge < maxCharg) && (chargeLimit < chargeLimitMax) && !punished && mainPlayer.older)
        {
            
            charge += Time.deltaTime * speedCharging * 3;
            chargeLimit += Time.deltaTime * speedCharging * 2;
            if (!noParticle)
            {
                //createChargeParticles();
                //Debug.Log("createChargeParticles();");
            }
            
        }

        //if Player stops charging, charging limit drops
        if (!mainPlayer.charging && chargeLimit > 0)
        {
            chargeLimit -= Time.deltaTime * speedCharging / 3;
        }
        //healthPlayer limit at max punishes the mainPlayer
        if (chargeLimit > chargeLimitMax)
        {
            mainPlayer.charging = false;
            animator.SetBool("Charge", mainPlayer.charging);
            TakeDamage(punishment);
            punished = true;
        }
        //active on bool punished true
        if (punished)
        {
            if (chargeLimit < 1 && mainPlayer.older)
            {
                punished = false;
                //Debug.Log("UNPUNISHED");
            }
        }
        //transform back to young
        if (charge >= maxCharg && mainPlayer.older)
        {
            //Debug.Log("isFully charged");
            charge = 0;
            tutorial = false;
            currentHealth = maxHealth;
            mainPlayer.charging = false;
            mainPlayer.older = false;
            isParrying = false;
            animator.SetBool("Old", mainPlayer.older);
            animator.SetBool("Parry", isParrying);
            animator.SetBool("Charge", mainPlayer.charging);
            Age();
            chargeRemoverCount = 0;
            swing.nextAttackTime = Time.time + swing.attackRate;
        }
    }
    // fill charging meter in young form
    void chargingFunctionYounger()
    {
        if (mainPlayer.kbd)
        {
            coolDown_ult_first_anim = false;
            coolDown_ult_last_anim = false;
            coolDown_Ult = false;
            watch.SetActive(false);
        }
        //ultimate button press on 100 or more healthPlayer
        if (mainPlayer.ult_press && mainPlayer.onGround)
        {
            if ((charge >= maxCharg) && !mainPlayer.older && !coolDown_Ult)
            {
                watch.transform.position = new Vector2(transform.position.x, transform.position.y);
                coolDown_Ult = true;
                coolDown_ult_first_anim = true;
                mainPlayer.charging = false;
                watch.SetActive(true);
            }
            else
            {
                mainPlayer.ult_press = false;
            }

        }
        //healthPlayer overloading with max 125 healthPlayer value droping to 100
        if (charge > maxCharg && !mainPlayer.older)
        {
            chargeCapMax = true;
            charge -= Time.deltaTime * speedCharging / 3;

            if (chargeCapMax && charge >= overloadCharge)
            {
                charge = overloadCharge;
            }
            if(chargeCapMax && charge < maxCharg)
            {
                charge = maxCharg;
            }
            
        }

        // timeFrezze value for timeFrezze old
        if (coolDown_Ult && !coolDown_ult_first_anim && !coolDown_ult_last_anim)
        {
            ps.GetComponent<PostProcess>().enabled=true;
            timeFrezze = true;
            timeFrezzeStatic = true;
        }
        //healthPlayer droping after cloack spawn animation is done
        if (timeFrezze && charge != 0 && !mainPlayer.older)
        {
            timeFrezze = true;
            timeFrezzeStatic = true;
            chargeCapMax = false;
            charge -= Time.deltaTime * speedCharging;
        }
        //end of ultimate ability at healthPlayer 0
        else if (timeFrezze && charge == 0 && !mainPlayer.older)
        {
            ps.GetComponent<PostProcess>().enabled = false;
            watch.SetActive(false);
            mainPlayer.ult_press = false;
            coolDown_Ult = false;
            charge = 0;
            chargeLimit = 0;
            timeFrezze = false;
            timeFrezzeStatic = false;
        }
    }


    // Dead state of Player method
    public void Die()
    {
        GetComponent<DealDamage>().enabled = false;
        GetComponent<PlayerMain>().isFinished = true;
        animator.SetBool("Old", false);
        animator.SetBool("MidAirSlash", false);
        Debug.Log("Game Over");
    }

    //private void createChargeParticles() {
    //    chargeParticles.Play();
    //}
}
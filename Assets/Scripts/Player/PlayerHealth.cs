using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // bool for tutorial
    public bool tutorial;
    // Reference values
    [SerializeField] ParticleSystem chargeParticles;
    public Animator animator;
    public LayerMask enemy;
    // health
    public int maxHealth;
    public int currentHealth;
    public int halfHealth;
    // cooldown double
    double regDamage;
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
    [HideInInspector]
    // start charging value 0
    public float charge = 0f;
    // charging speed
    float speedCharging= 10f;
    // max charge capacity
    public float maxCharg;
    [HideInInspector]
    // constant charging amount meter
    public float chargeLimit;
    // max charging amount meter
    public float chargeLimitMax;
    // damage on going reaching max charging amount meter
    public int punishment;
    // cooldown on charge usage
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

    //Rigidbody2D rb;
    // reference values
    public DealDamage swing;
    public PlayerMain player;
    public static PlayerHealth instance;
    public GameObject watch;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update

    void Start()
    {
        currentHealth = maxHealth;
        player = GetComponent<PlayerMain>();
        swing = GetComponent<DealDamage>();
        watch.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        // value on half health cheack for tutorial
        halfHealth = maxHealth / 2;
        // player tutorial mode
        if (tutorial)
        {
            //Physics2D.IgnoreLayerCollision(7,8);
            currentHealth = halfHealth;
        }
        // particle color change
        var main = chargeParticles.main;
        float removeOther = chargeLimit * chargeLimit * 0.00035f;
        main.startColor = new Color(1.0f, 1.0f - removeOther, 1.0f - removeOther, 1f);
        

        // Parrying state
        if (player.older && Input.GetKeyDown(KeyCode.X) && canParry && !player.charging && !player.kbd)
        {
            canParry = false;
            noParticle = true;
            //IEnumarator to Parry
            StartCoroutine(Parry());
            Debug.Log("tryed to parry...");
        }
        //cheking players state
        Age();
        //Chargeing funstion young and old
        chargingFunctionOlder();
        chargingFunctionYounger();
        if (charge < 0)
        {
            charge = 0;
        }
    }

    
    // taking damage method
    public void TakeDamage(int damage)
    {
        if (Time.time > regDamage)
        {
            // no damage on successful parry
            if (isParrying)
            {
                charge += parry;
                Debug.Log("Succsesfull Parry");
                player.KnockbackP(transform);
                regDamage = Time.time + 0.5;
            }
            // damage registered only above 0.5
            else if (damage > 0.5f)
            {
                
                currentHealth -= damage;
                animator.SetBool("Hurt", true);
                if (player != null)
                {
                    player.Knockback(transform);
                }
                regDamage = Time.time + 0.5;
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
        if (halfHealth >= currentHealth)
        {
            player.older = true;
            player.Old();
            //Debug.Log("should turn old");
        }
        else
        {
            
            player.older = false;
            player.Old();
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
        //Main charging function, charge and chargeLimit rises
        if (player.charging && (charge < maxCharg) && (chargeLimit < chargeLimitMax) && !punished && player.older)
        {
            
            charge += Time.deltaTime * speedCharging * 3;
            chargeLimit += Time.deltaTime * speedCharging * 2;
            if (!noParticle)
            {
                //createChargeParticles();
                Debug.Log("createChargeParticles();");
            }
            
        }

        //if Player stops charging, charging limit drops
        if (!player.charging && chargeLimit > 0)
        {
            chargeLimit -= Time.deltaTime * speedCharging / 3;
        }
        //charge limit at max punishes the player
        if (chargeLimit > chargeLimitMax)
        {
            player.charging = false;
            animator.SetBool("Charge", player.charging);
            TakeDamage(punishment);
            punished = true;
        }
        //active on bool punished true
        if (punished)
        {
            if (chargeLimit < 1 && player.older)
            {
                punished = false;
                //Debug.Log("UNPUNISHED");
            }
        }
        //transform back to young
        if (charge >= maxCharg && player.older)
        {
            Debug.Log("isFully charged");
            charge = 0;
            tutorial = false;
            currentHealth = maxHealth;
            player.charging = false;
            player.older = false;
            isParrying = false;
            animator.SetBool("Old", player.older);
            animator.SetBool("Parry", isParrying);
            animator.SetBool("Charge", player.charging);
            Age();
            swing.nextAttackTime = Time.time + swing.attackRate;
        }
        
        
       
    }
    // fill charging meter in young form
    void chargingFunctionYounger()
    {
        //ultimate button press on 100 or more charge
        if (player.ult_press && player.onGround)
        {
            if ((charge >= maxCharg) && !player.older && !coolDown_Ult)
            {
                watch.transform.position = new Vector2(transform.position.x, transform.position.y);
                coolDown_Ult = true;
                coolDown_ult_first_anim = true;
                player.charging = false;
                watch.SetActive(true);
            }
            else
            {
                player.ult_press = false;
            }

        }
        //charge overloading with max 125 charge value droping to 100
        if (charge > maxCharg && !player.older)
        {
            charge -= Time.deltaTime * speedCharging / 2;

            if (charge > (maxCharg * 1.25f))
            {
                charge = maxCharg * 1.25f;
            }
        }

        // timeFrezze value for timeFrezze old
        if (coolDown_Ult && !coolDown_ult_first_anim && !coolDown_ult_last_anim)
        {
            timeFrezze = true;
        }
        //charge droping after cloack spawn animation is done
        if (timeFrezze && charge != 0 && !player.older)
        {
            timeFrezze = true;
            charge -= Time.deltaTime * speedCharging;
        }
        //end of ultimate ability at charge 0
        else if (timeFrezze && charge == 0 && !player.older)
        {
            watch.SetActive(false);
            player.ult_press = false;
            coolDown_Ult = false;
            charge = 0;
            chargeLimit = 0;
            timeFrezze = false;
        }
    }

    // Dead state of Player method
    public void Die()
    {
        GetComponent<DealDamage>().enabled = false;
        GetComponent<PlayerMain>().isFinished = true;
        Debug.Log("Game Over");
    }

    //private void createChargeParticles() {
    //    chargeParticles.Play();
    //}
}
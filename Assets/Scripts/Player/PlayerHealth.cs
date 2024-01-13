using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerHealth : MonoBehaviour
{
    public bool tutorial;
    [SerializeField] ParticleSystem chargeParticles;
    public Animator animator;
    public LayerMask enemy;
    public int maxHealth;
    public int currentHealth;
    public int halfHealth;
    double regDamage;
    public float parry;
    public bool canParry=true;
    public bool isParrying=false;
    public float parryDuration;
    public float parryCoolDown;
    public bool ulti_start = false;
    bool noParticle = false;

    [HideInInspector]
    public float charge = 0f;
    float speedCharging= 10f;
    public float maxCharg;
    [HideInInspector]
    public float chargeLimit;
    public float chargeLimitMax;
    public int punishment;
    public float punishmentTime;
    [HideInInspector]
    public bool punished = false;
    public bool coolDown_Ult = false;
    public bool coolDown_ult_first_anim = false;
    public bool coolDown_ult_last_anim=false;
    public bool timeFrezze = false;

    //Rigidbody2D rb;
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
        halfHealth = maxHealth / 2;
        if (tutorial)
        {
            //Physics2D.IgnoreLayerCollision(7,8);
            currentHealth = halfHealth;
        }

        var main = chargeParticles.main;
        float removeOther = chargeLimit * chargeLimit * 0.00035f;
        main.startColor = new Color(1.0f, 1.0f - removeOther, 1.0f - removeOther, 1f);
        

        //Parrying
        if (player.older && Input.GetKeyDown(KeyCode.X) && canParry && !player.charging && !player.kbd)
        {
            canParry = false;
            noParticle = true;
            //IEnumarator to Parry
            StartCoroutine(Parry());
            Debug.Log("tryed to parry...");
        }

        Age();
        //Chargeing funstion young and old
        chargingFunctionOlder();
        chargingFunctionYounger();
        if (charge < 0)
        {
            charge = 0;
        }
    }

    

    public void TakeDamage(int damage)
    {
        if (Time.time > regDamage)
        {
            if (isParrying)
            {
                charge += parry;
                Debug.Log("Succsesfull Parry");
                player.KnockbackP(transform);
                regDamage = Time.time + 0.5;
            }
            else
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
    //Charging in Old form
    public void chargingFunctionOlder()
    {
        //Main charging function, charge and chargeLimit rises
        if (player.charging && (charge < maxCharg) && (chargeLimit < chargeLimitMax) && !punished && player.older)
        {
            
            charge += Time.deltaTime * speedCharging * 3;
            chargeLimit += Time.deltaTime * speedCharging * 2;
            if (!noParticle)
            {
                createChargeParticles();
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


    void Die()
    {
        GetComponent<DealDamage>().enabled = false;
        GetComponent<PlayerMain>().isFinished = true;
        Debug.Log("Game Over");
    }

    private void createChargeParticles() {
        chargeParticles.Play();
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;

    public LayerMask enemy;
    public int maxHealth;
    public int currentHealth;
    double regDamage;
    public float parry;
    public bool canParry=true;
    public bool isParrying=false;
    public float parryDur;
    public float parryCD;
    public bool ulti_start = false;
    public bool main_ulti = false;

    [HideInInspector]
    public float charge = 0f;
    float speedC= 10f;
    public float maxCharg;
    [HideInInspector]
    public float chargeLimit;
    public float chargeLimitMax;
    public int punishment;
    public float punishmentTime;
    [HideInInspector]
    public bool punished = false;
    public bool cDJ = false;
    public bool cDJA = false;
    public bool cDJA_end;

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
        if (player.older && Input.GetKeyDown(KeyCode.X) && canParry && !player.charging && !player.kbd)
        {
            canParry = false;
            StartCoroutine(Parry());
            Debug.Log("tryed to parry...");
        }
        Age();
        if(Time.time > regDamage)
        {
            if (player.charging && (charge < maxCharg) && (chargeLimit < chargeLimitMax) && !punished && player.older)
            {
                charge += Time.deltaTime * speedC;
                chargeLimit += Time.deltaTime * speedC * 2;
            }
            
        }
        if (player.ult_press && player.onGround)
        {
            if ((charge >= maxCharg) && !player.older && !cDJ)
            {
                watch.transform.position = new Vector2(transform.position.x, transform.position.y);
                cDJ = true;
                cDJA = true;
                player.charging = false;
                watch.SetActive(true);
            }
            else
            {
                player.ult_press = false;
            }

        }
        
        if (charge > maxCharg && !player.older)
        {
            charge -= Time.deltaTime * speedC / 2;
            
            if(charge > (maxCharg * 1.25f))
            {
                charge = maxCharg * 1.25f;
            }
        }


        if (charge >= maxCharg && player.older)
        {
            Debug.Log("isFully charged");
            charge = 0;
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
        if (chargeLimit > chargeLimitMax)
        {
            player.charging = false;
            animator.SetBool("Charge", player.charging);
            TakeDamage(punishment);
            punished = true;
        }
        if (charge <= 0)
        {
            charge = 0;
        }
        if (!player.charging && chargeLimit > 0)
        {
            chargeLimit -= Time.deltaTime * speedC / 5;
        }
        if (punished)
        {
            if (chargeLimit < 1 && player.older)
            {
                punished = false;
                //Debug.Log("UNPUNISHED");
            }
        }
        if (cDJ)
        {
            if (charge !=0 && !player.older)
            {
                charge -= Time.deltaTime * speedC/2;
            }
            else
            {
                watch.SetActive(false);
                player.ult_press = false;
                cDJ = false;
                charge = 0;
                chargeLimit = 0;
            }
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
        
        yield return new WaitForSeconds(parryDur);
        isParrying = false;
        animator.SetBool("Parry", isParrying);

        yield return new WaitForSeconds(parryCD);
        canParry = true;
    }
    public void Age()
    {
        if (currentHealth <= (maxHealth / 2))
        {
            player.older = true;
            player.Old();
        }
        else
        {
            
            player.older = false;
            player.Old();
        }
    }
    public void ChargeReg(float x) 
    {
        if (Time.time > regDamage || !cDJ)
        {
          charge += x;
          regDamage = Time.time + 0.2;
        }
            
    }


    void Die()
    {
        GetComponent<DealDamage>().enabled = false;
        GetComponent<PlayerMain>().isFinished = true;
        Debug.Log("Game Over");
    }


}
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;


    public int maxHealth;
    [HideInInspector]
    public int currentHealth;
    double regDamage;
    public float parry;
    private bool canParry=true;
    public bool isParrying=false;
    public float parryDur;
    public float parryCD;

    [HideInInspector]
    public float charge=0f;
    float speedC= 10f;
    public float maxCharg;
    [HideInInspector]
    public float chargeLimit;
    public float chargeLimitMax;
    public int punishment;
    public float punishmentTime;
    [HideInInspector]
    public bool punished = false;



    public Transform knockBack;

    //Rigidbody2D rb;
    public DealDamage swing;
    public PlayerMain player;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GetComponent<PlayerMain>();
        swing = GetComponent<DealDamage>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.older && Input.GetKeyDown(KeyCode.X) && canParry && !player.charging && !player.kbd)
        {
            StartCoroutine(Parry());
            Debug.Log("tryed to parry...");
        }
        Age();
        if(Time.time > regDamage)
        {
            if (player.charging && (charge < maxCharg) && (chargeLimit < chargeLimitMax) && !punished)
            {
                chargeLimit += Time.deltaTime * speedC * 2;
                charge += Time.deltaTime * speedC;
            }
        }
        
        if (charge >= maxCharg)
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
        if (chargeLimit >= chargeLimitMax)
        {
            //chargeLimit -= Time.deltaTime * speedC / 5;
            player.charging = false;
            animator.SetBool("Charge", player.charging);
            TakeDamage(punishment);
            //regDamage = Time.time + punishmentTime;
            punished = true;
            
        }
        if (!player.charging && chargeLimit > 0)
        {
            chargeLimit -= Time.deltaTime * speedC / 5;
        }
        if (punished && chargeLimit < 1)
        {
            punished = false;
            //Debug.Log("UNPUNISHED");
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
                regDamage = Time.time + 0.5;
                
            }
            else
            {
                currentHealth -= damage;
                animator.SetTrigger("Hurt");
                animator.SetTrigger("Old_Hurt");
                regDamage = Time.time + 0.5;
                
                //if (GetComponent<PlayerMain>().isFinished)
                //{
                //    Debug.Log("For Some fucking reason the player still takes Damage");
                //}
            }
            if (player != null)
            {
                player.Knockback(transform);
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


    void Die()
    {
        GetComponent<DealDamage>().enabled = false;
        GetComponent<PlayerMain>().isFinished = true;
        
        Debug.Log("Game Over");
    }


}
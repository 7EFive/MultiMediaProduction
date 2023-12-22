
using UnityEngine;

public class Spawn_watch : MonoBehaviour
{
    public GameObject player;
    PlayerHealth stats;
    PlayerMain flip;
    public SpriteRenderer sprite;
    public Animator animte;
    public bool rise = false;
    public bool spin = false;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        stats = player.GetComponent<PlayerHealth>();
        flip = player.GetComponent<PlayerMain>();
        sprite = GetComponent<SpriteRenderer>();
        animte = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();

        if (stats.coolDown_Ult)
        {
            
            if (stats.coolDown_ult_first_anim)
            {
                
                rise = true;
                animte.SetBool("rise", rise);
            }
            else
            {
                
                animte.SetBool("rise", rise);
            }
            if (stats.coolDown_ult_last_anim)
            {
                spin = true;
                animte.SetBool("spin", rise);
            }
            else
            {
                animte.SetBool("spin", rise);
            }
        }
    }
    void Flip()
    {
        if (!stats.timeFrezze)
        {
            if (flip.facingRight)
            {
                //Debug.Log("Player ditected on right side");
                transform.localScale = new Vector3(0.25f, transform.localScale.y, transform.localScale.z);
                Debug.Log("Should face left");
            }
            else if (!flip.facingRight)
            {
                transform.localScale = new Vector3(-0.25f, transform.localScale.y, transform.localScale.z);
                Debug.Log("Should face right");
            }
        }
        
        
    }
}

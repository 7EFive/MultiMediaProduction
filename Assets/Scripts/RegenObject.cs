using UnityEngine;
using UnityEngine.UI;

public class RegenObject : MonoBehaviour
{
    public GameObject player;
    EnemyHealth dummy;
    PlayerHealth timeStop;

    [SerializeField]
    public SceneInfo sceneInfo;
    [SerializeField] ParticleSystem pFlame;
    [SerializeField] GameObject healthFillArea;
    Image hbc;
    public Color defSpellColor;
    void Start()
    {
        hbc = healthFillArea.GetComponentInChildren<Image>();
        hbc.color = defSpellColor;
        timeStop = player.GetComponent<PlayerHealth>();
        dummy = gameObject.GetComponent<EnemyHealth>();
        dummy.currentHealth = dummy.maxHealth;
    }
    void Update()
    {
        if (!timeStop.timeFrezze && !dummy.en.dead)
        {
            if (!pFlame.isPlaying)
            {
                timeStopPartlc();
            }
            
        }
        else
        {
            if (pFlame.isPlaying)
            {
                TSPhalt();
            }
            
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //if (!timeStop.timeFrezze && !sceneInfo.wasDestroyed )
        //{
        //    dummy.currentHealth = dummy.maxHealth;
        //    //Debug.Log("Particles should be displayed");
        //}
        
        if (!dummy.isEnemy && dummy.en.dead)
        {
            sceneInfo.wasDestroyed = dummy.en.dead;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
    void timeStopPartlc()
    {
        pFlame.Play();
    }
    void TSPhalt()
    {
        pFlame.Stop();
    }

}

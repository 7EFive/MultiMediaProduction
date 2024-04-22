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
    [SerializeField] PatrollingBeh patroll;
    Image hbc;
    Color colorP;
    //public Color defSpellColor;
    void Start()
    {
        hbc = healthFillArea.GetComponentInChildren<Image>();
        ColorUtility.TryParseHtmlString("#6C00C3", out colorP);
        hbc.GetComponent<Image>().color=colorP;
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
            if (!patroll.enabled)
            {
                patroll.enabled = true;
            }
        }
        else
        {
            if (pFlame.isPlaying)
            {
                TSPhalt();
            }
            if (patroll.enabled)
            {
                patroll.enabled = false;
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
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

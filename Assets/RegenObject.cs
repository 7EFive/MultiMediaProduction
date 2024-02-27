using UnityEditor;
using UnityEngine;

public class RegenObject : MonoBehaviour
{
    public GameObject player;
    public EnemyHealth obj;
    PlayerHealth timeStop;
    int i;
    public bool regen;
    public bool isDead;
    [SerializeField]
    public SceneInfo sceneInfo;
    void Start()
    {
        regen = true;
        timeStop = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (regen && !obj.en.dead && obj.currentHealth > 0)
        {
            obj.currentHealth = obj.maxHealth;
        }
        if (timeStop.timeFrezze)
        {
            regen = false;
        }
        else
        {
            regen = true;
        }
        if (!obj.isEnemy && obj.en.dead)
        {
            sceneInfo.wasDestroyed = obj.en.dead;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}

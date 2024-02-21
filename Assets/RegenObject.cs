using UnityEngine;

public class RegenObject : MonoBehaviour
{
    public GameObject player;
    public EnemyHealth obj;
    PlayerHealth timeStop;
    int i;
    public bool regenOn;
    void Start()
    {
        timeStop = player.GetComponent<PlayerHealth>();
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (regenOn)
        {
            if (timeStop.timeFrezze)
            {
                ++i;
            }
            if (obj.currentHealth != obj.maxHealth && i == 0)
            {
                obj.currentHealth = obj.maxHealth;
            }
        }
        if (!obj.isEnemy && obj.en.dead)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }

    }
}

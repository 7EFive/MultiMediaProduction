using UnityEngine;

public class OneSideBlockDoor : MonoBehaviour
{
    public bool isColliding;
    [SerializeField] BoxCollider2D bc2d;
    [SerializeField] GameObject Door;
    EnemyHealth EH;
    // Start is called before the first frame update
    void Start()
    {
        EH = Door.GetComponent<EnemyHealth>();
        isColliding = false;
        bc2d.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isColliding && EH.currentHealth != EH.maxHealth)
        {
            EH.currentHealth = EH.maxHealth;
        }
        if (EH.currentHealth == 0)
        {
            bc2d.enabled=false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}

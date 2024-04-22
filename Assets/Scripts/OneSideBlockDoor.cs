using UnityEngine;

public class OneSideBlockDoor : MonoBehaviour
{
    public bool isColliding;
    [SerializeField] BoxCollider2D bc2d;
    BoxCollider2D bc2dMain;
    [SerializeField] GameObject Door;
    EnemyHealth eh;
    // Start is called before the first frame update
    void Start()
    {
        eh = Door.GetComponent<EnemyHealth>();
        bc2dMain = Door.GetComponent<BoxCollider2D>();
        isColliding = false;
        bc2dMain.enabled = false;
        bc2d.enabled = true;
        //eh.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isColliding && eh.currentHealth != eh.maxHealth)
        {
            eh.currentHealth = eh.currentHealth;
        }
        if (eh.currentHealth == 0)
        {
            bc2d.enabled=false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && eh.currentHealth > 0)
        {
            isColliding = true;
            //eh.enabled = true;
            bc2dMain.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            //eh.enabled = false;
            bc2dMain.enabled = false;
        }
    }
}

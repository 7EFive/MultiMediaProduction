using UnityEngine;

public class Chasing : MonoBehaviour
{
    [SerializeField] Enemy enemyScript;
    [SerializeField] private Transform target;
    public bool isChasing;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Shit should work");
    }
    private void Update()
    {
        transform.position = target.position;
    }

    // Update is called once per frame


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyScript.isChasing=true;
            //Debug.Log("Should notice player");
            isChasing = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyScript.isChasing = false;
            isChasing = false;
        }
    }
}

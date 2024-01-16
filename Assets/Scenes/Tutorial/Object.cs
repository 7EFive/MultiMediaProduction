using UnityEngine;

public class Object : MonoBehaviour
{
    private Vector2 initialPosition;
    [SerializeField] GameObject player;
    void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            transform.position = initialPosition;
            
        }
    }
}

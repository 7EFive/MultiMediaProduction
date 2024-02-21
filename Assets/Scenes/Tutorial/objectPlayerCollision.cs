using UnityEngine;

public class objectPlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject player;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
        player.GetComponent<PlayerHealth>().TakeDamage(0);
        Debug.Log("Object is colliding with mainPlayer");
        }
    }
}

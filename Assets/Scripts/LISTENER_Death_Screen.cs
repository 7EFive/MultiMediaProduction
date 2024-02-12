using UnityEngine;

public class LISTENER_Death_Screen : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject deathScreenElements;
    
    void Update() {
        if (player.GetComponent<PlayerHealth>().currentHealth <= 0) {
            deathScreenElements.SetActive(true);
            Cursor.visible = true;
        }
    }
}

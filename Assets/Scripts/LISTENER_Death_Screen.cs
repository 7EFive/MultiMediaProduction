using UnityEngine;

public class LISTENER_Death_Screen : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject deathScreenElements;
    public bool deathScreanMenu;
    private void Start()
    {
    deathScreanMenu = false;

    }
    void Update() {
        if (player.GetComponent<PlayerHealth>().currentHealth <= 0) {
            deathScreenElements.SetActive(true);
            deathScreanMenu = true;
            
        }
        else
        {
            deathScreanMenu = false;
        }
    }
}

using UnityEngine;

public class Collider_EndOfTutorial : MonoBehaviour
{
    public bool playerPassedTheTutorial = false;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            playerPassedTheTutorial = true;
        }
    }
}

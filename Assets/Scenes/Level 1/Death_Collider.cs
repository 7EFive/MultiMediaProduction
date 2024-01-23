using UnityEngine;

public class Death_Collider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMain>().older = true;
            other.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }
}

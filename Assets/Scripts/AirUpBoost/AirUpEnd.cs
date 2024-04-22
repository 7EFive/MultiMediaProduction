using UnityEngine;

public class AirUpEnd : MonoBehaviour
{
    public bool collidingEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collidingEnd = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collidingEnd = false;
        }
    }
}

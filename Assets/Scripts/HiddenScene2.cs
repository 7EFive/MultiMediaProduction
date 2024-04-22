using UnityEngine;

public class HiddenScene2 : MonoBehaviour
{
    public bool start;

    void Start()
    {
        start = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            start = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            start = false;
        }

    }
}

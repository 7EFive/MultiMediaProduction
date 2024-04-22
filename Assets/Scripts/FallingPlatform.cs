using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float platformHold;
    public float platformRemove;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Falling());
        }
    }

    IEnumerator Falling()
    {
        yield return new WaitForSeconds(platformHold);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, platformRemove);
    }
}

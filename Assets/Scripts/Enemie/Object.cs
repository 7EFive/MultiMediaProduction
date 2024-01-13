using UnityEngine;

public class Object : MonoBehaviour
{
    public bool onGround;
    public Vector2 position;
    float regTime;

    void Start()
    {
        Vector2 position = transform.position;
        //Physics2D.IgnoreLayerCollision(7, 8);
    }
    void Update()
    {

        //Debug.Log(regTime + Time.deltaTime);
        if (Time.time >= regTime)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        if (onGround)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = new Vector2(position.x, position.y);
            //Debug.Log("Should reset to top again");
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            regTime = Time.time + 1f;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}

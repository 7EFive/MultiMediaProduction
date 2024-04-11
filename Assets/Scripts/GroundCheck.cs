using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool onGround=true;
    public string mainTag = "Ground";
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(mainTag))
        {
            onGround = true;
            //Debug.Log("Collieding with ground");
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(mainTag))
        {
            onGround = false;
            //Debug.Log("Collieding with ground");
        }
        //Debug.Log("Not Collieding with ground");
    }
}

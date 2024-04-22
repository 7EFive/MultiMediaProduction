using UnityEngine;

public class WalkBinds : MonoBehaviour
{
    public GameObject walkBinds;
    public int i = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&& i==0)
        {
            walkBinds.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && i == 0)
        {
            walkBinds.SetActive(false);
            i++;
        }
    }
    // Start is called before the first frame update

}

using UnityEngine;

public class HiddenScene : MonoBehaviour
{
    public Animator animator;
    public GameObject coll2;
    HiddenScene2 h2;
    public bool start;

    private void Start()
    {
        h2 = coll2.GetComponent<HiddenScene2>();
    }
    void Update()
    {
        if (start || h2.start)
        {
            animator.SetBool("AddScene", true);
        }
        else
        {
            animator.SetBool("AddScene", false);
        }
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

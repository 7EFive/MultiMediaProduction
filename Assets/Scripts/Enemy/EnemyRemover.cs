using UnityEngine;

public class EnemyRemover : MonoBehaviour
{
    public static EnemyRemover instance;
    public bool isReadyForRemoval;
    Animator animator;
    void Start()
    {
        animator= gameObject.GetComponent<Animator>();
    }
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (isReadyForRemoval)
        {
            animator.SetBool("Remove", isReadyForRemoval);
            remove();
        }
    }

    // Update is called once per frame
    public void remove()
    {
        Debug.Log("Enemy has been removed");
        this.gameObject.SetActive(false);
    }
}

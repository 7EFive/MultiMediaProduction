using UnityEditor;
using UnityEngine;

public class ShowItem : MonoBehaviour
{
    Animator animator;
    public bool showItem;
    public static ShowItem instance;
    
    // Start is called before the first frame update
    void Start()
    {
        showItem = false;
        animator = gameObject.GetComponent<Animator>();
    }
    private void Awake()
    {
        instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        if (showItem)
        {
            animator.SetBool("Start",showItem);
        }
        else
        {
            animator.SetBool("Start", showItem);
        }
    }
}

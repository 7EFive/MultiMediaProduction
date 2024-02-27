using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendInOut : MonoBehaviour
{
    public Animator animator;
    public bool start;

    void Update()
    {
        if(start)
        {
            animator.SetBool("AddScene", start);
        }
        else
        {
            animator.SetBool("AddScene", start);
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

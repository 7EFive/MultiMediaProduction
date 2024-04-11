using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyShowcase : MonoBehaviour
{
    [SerializeField] GameObject key;
    Animator animator;
    SceneChangeTrigger scT;
    bool showItem;
    // Start is called before the first frame update
    void Start()
    {
        showItem = false;
        scT = gameObject.GetComponent<SceneChangeTrigger>();
        animator = key.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scT.conditionsMade)
        {
            animator.SetBool("ItemDrop",showItem);
        }
    }
}

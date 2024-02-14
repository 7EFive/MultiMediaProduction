using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBinds : MonoBehaviour
{
    public GameObject walkBinds;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            walkBinds.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            walkBinds.SetActive(false);
        }
    }
    // Start is called before the first frame update

}

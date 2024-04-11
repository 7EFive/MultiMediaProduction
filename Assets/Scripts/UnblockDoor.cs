using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockDoor : MonoBehaviour
{

    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}

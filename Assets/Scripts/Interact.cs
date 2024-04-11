using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] GameObject main;
    public GameObject interactable;
    TutorialGameLogic tgl;

    void Start()
    {
        tgl = main.GetComponent<TutorialGameLogic>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tgl.canInteract = true;
            interactable.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tgl.canInteract = false;
            interactable.SetActive(false);
        }
    }
}

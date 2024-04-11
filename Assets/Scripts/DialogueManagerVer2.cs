using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManagerVer2 : MonoBehaviour
{
    [SerializeField] public GameObject dialogueObj, dialogueObj2;
    DialogueLogicVer2 dialogue, dialogue2;
    private void Start()
    {
        dialogue = dialogueObj.GetComponent<DialogueLogicVer2>();
        dialogueObj.SetActive(true);
        dialogue2 = dialogueObj2.GetComponent<DialogueLogicVer2>();
        dialogueObj2.SetActive(false);
    }
    private void Update()
    {
        if (dialogue.done)
        {
            dialogueObj.SetActive(false);
            dialogueObj2.SetActive(true);
        }
    }
}

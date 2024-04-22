using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLogicVer2 : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator animator2;
    [SerializeField] public float letterTypingIntervalTime;
    [SerializeField]
    public PlayerMain mainPlayer;
    [SerializeField]
    public GameObject dialogueObject; 

    //UI
    [SerializeField]
    private TMP_Text namePlaceHolder;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private Image charIconUi;

    //Content
    [SerializeField]
    private string[] charName;
    [SerializeField]
    [TextArea]
    private string[] dialogueContent;
    [SerializeField]
    private Sprite[] portrait;
    [SerializeField]
    private bool[] changeIcon;

    //In Clase variable
    bool isInZone;
    private int index;
    bool animate;
    public bool switchDialogue;
    [HideInInspector]
    public bool done=false;

    void Start()
    {
        done = false;
        dialogueObject.SetActive(true);
        animate = false;
        animator.SetBool("PopUp", animate);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInZone)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (index >= charName.Length)
                {
                    StopAllCoroutines();
                    animate = false;
                    animator.SetBool("PopUp", animate);
                    //dialogueObject.SetActive(false);
                    index = 0;
                    if (switchDialogue)
                    {
                        done = true;
                    }
                    mainPlayer.interactionStun = false;
                }
                else
                {
                    animate = true;
                    animator.SetBool("PopUp", animate);
                    mainPlayer.interactionStun = true;
                    //dialogueObject.SetActive(true);
                    NextLine();
                }

            }
        }
        
    }
    public void NextLine()
    {
        namePlaceHolder.text = charName[index];
        StopAllCoroutines();
        StartCoroutine(TypeDialogue(dialogueContent[index],letterTypingIntervalTime));
        //dialogueText.text = dialogueContent[index];
        if (charIconUi.sprite != portrait[index])
        {
            charIconUi.sprite = portrait[index];
            animator2.SetTrigger("ChangeIcon");
        }
        charIconUi.sprite = portrait[index];
        index++;
    }
    IEnumerator TypeDialogue(string sentance,float typeDealay)
    {
        dialogueText.text = "";
        //yield return new WaitForSeconds(typeDealay);
        foreach (char letter in sentance.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeDealay);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = true;
            //interaction.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isInZone = false;
    }
}

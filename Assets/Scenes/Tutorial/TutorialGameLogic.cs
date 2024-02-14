using UnityEngine;

public class TutorialGameLogic : MonoBehaviour
{
    //Text
    [SerializeField] GameObject interaction;
    [SerializeField] GameObject TEXT_Interact1;
    [SerializeField] GameObject TEXT_Interact2;
    [SerializeField] GameObject TEXT_Interact3;
    [SerializeField] GameObject TEXT_ult1;
    [SerializeField] GameObject TEXT_ult2;
    [SerializeField] GameObject TEXT_ult3;
    [SerializeField] GameObject TEXT_howToChargeAndParry;
    [SerializeField] GameObject TEXT_attackTheDoor;
    [SerializeField] GameObject TEXT_attackTheBox;
    [SerializeField] GameObject TEXT_dashThroughTheOpening;
    [SerializeField] GameObject TEXT_endOfTheTutorial;

    //buttons
    [SerializeField] GameObject bE;
    [SerializeField] GameObject bX;
    [SerializeField] GameObject bXC;
    [SerializeField] GameObject bX2;
    [SerializeField] GameObject bV;
    [SerializeField] GameObject bVHold;
    [SerializeField] GameObject bWalk;
    [SerializeField] GameObject bDash;
    [SerializeField] GameObject bJump;

    public int i;
    public bool canInteract=true;
    // Other Objects
    [SerializeField] GameObject player;
    [SerializeField] GameObject door;
    [SerializeField] GameObject box;
    [SerializeField] GameObject dummy;
    [SerializeField] GameObject dummyCollieder;

    [SerializeField] GameObject COLLIDER_Walk;
    [SerializeField] GameObject COLLIDER_endOfTheTutorial;

    void Start(){
        player.GetComponent<PlayerMain>().older = true;
        i = 0;
        COLLIDER_Walk.SetActive(false);
    }
    void Update(){
        if(i==0)
        {
            TEXT_Interact1.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("tryed to interact");
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            canInteract = true;
        }
        interactCheack();
        //Check if the player has already charged and is now young.
        if (!player.GetComponent<PlayerMain>().older) {
            TEXT_howToChargeAndParry.SetActive(false);
            bWalk.SetActive(false);
            bVHold.SetActive(false);
            bX.SetActive(false);
            TEXT_attackTheDoor.SetActive(true);
            bXC.SetActive(true);
        }

        //Check if the player has already opened the door.
        if(door.GetComponent<Enemy>().dead) {

            TEXT_attackTheDoor.SetActive(false);
            bXC.SetActive(false);
            
            if (i == 3)
            {
                ++i;
            }
        }

        //Check if the player has already broken the box.
        if(box.GetComponent<Enemy>().dead) {
            ++i;
            TEXT_attackTheBox.SetActive(false);
            bX2.SetActive(false);
            bDash.SetActive(true);
            TEXT_dashThroughTheOpening.SetActive(true);
        }

        //Check if the player has already dashed through the opening.
        if(COLLIDER_endOfTheTutorial.GetComponent<Collider_EndOfTutorial>().playerPassedTheTutorial) {
            bDash.SetActive(false);
            bJump.SetActive(false);
            TEXT_dashThroughTheOpening.SetActive(false);
            TEXT_endOfTheTutorial.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.E) && canInteract && i==4)
        {

            TEXT_ult1.SetActive(true);
            Debug.Log("started ult tutorial");
            canInteract = false;
            ++i;
        }
        ultCheack();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            interaction.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            interaction.SetActive(false);
        }
    }
    void interactCheack()
    {
        if(Input.GetKeyDown(KeyCode.E) && canInteract && i==0)
        {
            TEXT_Interact1.SetActive(false);
            TEXT_Interact2.SetActive(true);
            ++i;
            canInteract = false;
            bE.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && canInteract && i == 1)
        {
            TEXT_Interact2.SetActive(false);
            TEXT_Interact3.SetActive(true);
            ++i;
            canInteract = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && canInteract && i == 2)
        {
            interaction.SetActive(false);
            TEXT_Interact3.SetActive(false);
            TEXT_howToChargeAndParry.SetActive(true);
            ++i;
            canInteract = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            bWalk.SetActive(true);
            bVHold.SetActive(true);
            bX.SetActive(true);
            COLLIDER_Walk.SetActive(true);
        }
    }
    
    void ultCheack()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && canInteract && i == 5)
        {
            TEXT_ult1.SetActive(false);
            TEXT_ult2.SetActive(true);
            ++i;
            canInteract = false;
            
            
        }
        if (Input.GetKeyDown(KeyCode.E) && canInteract && i == 6)
        {
            TEXT_ult2.SetActive(false);
            TEXT_ult3.SetActive(true);
            bV.SetActive(true);
            ++i;
        }
        if (player.GetComponent<PlayerHealth>().timeFrezze && i == 7)
        {
            ++i;
        }
        if (dummy.GetComponent<Enemy>().dead && i == 8)
        {
            TEXT_ult3.SetActive(false);
            bV.SetActive(false);
            TEXT_attackTheBox.SetActive(true);
            bJump.SetActive(true);
            bX2.SetActive(true);
            dummyCollieder.SetActive(false);
        }
    }
}

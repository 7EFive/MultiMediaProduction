using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    //public int sceneToLoad;
    public GameObject canvas;
    LevelLoader fade;
    bool canInteract=false;
    public bool automaticTrigger;
    //public GameObject button;
    public GameObject interactor;
    Animator animator;
    public bool conditionsMade;
    public bool needsAKey;

    

    //public GameObject mainPlayer; asda

    private void Start()
    {
        animator= gameObject.GetComponent<Animator>();
        fade = canvas.GetComponent<LevelLoader>();
        //button.SetActive(false);
    }
    void Update()
    {
        if (needsAKey)
        {
            if ((Input.GetButtonDown("Interact") && canInteract))
            {
                animator.SetBool("Open", conditionsMade);
                needsAKey = false;
            }
        }
        else
        {
            ChangeScene();
        }

        
      }
    public void ChangeScene()
    {
        if ((Input.GetButtonDown("Interact") && canInteract) || (canInteract && automaticTrigger))
        {
            //button.SetActive(false);
            Debug.Log("Level Change");
            fade.LoadNextLevel();
            if (!automaticTrigger)
            {
                interactor.GetComponent<PlayerMain>().interactionStun = true;
            }

            //SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
    // triggers LevelLoder Script and Fade
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && conditionsMade)
        { 
            canInteract=true;
            //if (!automaticTrigger)
            //{
            //    
            //    button.SetActive(true);
            //}
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !conditionsMade)
        {
            canInteract = false;
            //button.SetActive(false);
        }
    }
    public void ButtonUp()
    {
        if(Input.GetButtonDown("Interact") && !canInteract && conditionsMade)
        {
            canInteract = true;
        }
    }
}

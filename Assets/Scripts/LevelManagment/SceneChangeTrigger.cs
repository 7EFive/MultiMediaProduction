using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    //public int sceneToLoad;
    public GameObject canvas;
    LevelLoader fade;
    bool canInteract=false;
    public bool automaticTrigger;
    public GameObject button;
    public GameObject interactor;
    

    //public GameObject mainPlayer; asda

    private void Start()
    {
        canInteract = false;
        fade = canvas.GetComponent<LevelLoader>();
        button.SetActive(false);
    }
    void Update()
    {
        
        if ((Input.GetKeyDown(KeyCode.E) && canInteract) || (canInteract && automaticTrigger))
        {
            button.SetActive(false);
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
        if (other.CompareTag("Player"))
        { 
            canInteract=true;
            if (!automaticTrigger)
            {
                
                button.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            button.SetActive(false);
        }
    }
}

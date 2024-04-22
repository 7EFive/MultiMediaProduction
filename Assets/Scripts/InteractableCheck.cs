using UnityEngine;

public class InteractableCheck : MonoBehaviour
{
    [SerializeField] GameObject interactable;
    PlayerMain player;
    private void Start()
    {
        interactable.SetActive(false);
        player = GetComponentInParent<PlayerMain>();
    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable")  && !player.interactionStun)
        {
            Debug.Log("Can Interract");
            interactable.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactable.SetActive(false);
        }
    }
}

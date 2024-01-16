using UnityEngine;

public class TutorialGameLogic : MonoBehaviour
{
    //Text
    [SerializeField] GameObject TEXT_howToChargeAndParry;
    [SerializeField] GameObject TEXT_attackTheDoor;
    [SerializeField] GameObject TEXT_attackTheBox;
    [SerializeField] GameObject TEXT_dashThroughTheOpening;
    [SerializeField] GameObject TEXT_endOfTheTutorial;

    // Other Objects
    [SerializeField] GameObject player;
    [SerializeField] GameObject door;
    [SerializeField] GameObject box;

    [SerializeField] GameObject COLLIDER_endOfTheTutorial;

    void Start(){
        player.GetComponent<PlayerMain>().older = true;
    }
    void Update(){
        //Check if the player has already charged and is now young.
        if(!player.GetComponent<PlayerMain>().older) {
            TEXT_howToChargeAndParry.SetActive(false);
            TEXT_attackTheDoor.SetActive(true);
        }

        //Check if the player has already opened the door.
        if(door.GetComponent<Enemy>().dead) {
            TEXT_attackTheDoor.SetActive(false);
            TEXT_attackTheBox.SetActive(true);
        }

        //Check if the player has already broken the box.
        if(box.GetComponent<Enemy>().dead) {
            TEXT_attackTheBox.SetActive(false);
            TEXT_dashThroughTheOpening.SetActive(true);
        }

        //Check if the player has already dashed through the opening.
        if(COLLIDER_endOfTheTutorial.GetComponent<Collider_EndOfTutorial>().playerPassedTheTutorial) {
        TEXT_dashThroughTheOpening.SetActive(false);
        TEXT_endOfTheTutorial.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirUp : MonoBehaviour
{
    [SerializeField] GameObject player;
    bool colliding;
    [SerializeField] GameObject groundChkObj;
    GroundCheck groundChk;
    [SerializeField] GameObject extraEndCollider;
    //[SerializeField] GameObject interactionSymbl;
    int i = 0;
    string groundLayerName = "Ground";
    string playerLayerName = "Player";
    string removeTag= "Untagged";
    string defaultTag = "Interactable";
    int groundLayer;
    int playerLayer;

    void Start()
    {
        groundChk = groundChkObj.GetComponent<GroundCheck>();
        groundLayer = LayerMask.NameToLayer(groundLayerName);
        playerLayer = LayerMask.NameToLayer(playerLayerName);
        groundLayer = Mathf.Clamp(groundLayer, 0, 31);
        playerLayer = Mathf.Clamp(playerLayer, 0, 31);
    }
    void Update()
    {
        /**
        if (colliding && i==0)
        {
            interactionSymbl.SetActive(true);
        }
        **/
        if (i >= 1 && !extraEndCollider.GetComponent<AirUpEnd>().collidingEnd)
        {
            gameObject.tag = removeTag;
            groundChk.mainTag = "Object";
            //Debug.Log("Collision off");
            extraEndCollider.SetActive(true);
            Physics2D.IgnoreLayerCollision(groundLayer,playerLayer, true);
            player.GetComponent<PlayerMain>().interactionStun = true;
            player.GetComponent<PlayerMain>().airUp = true;
        }
        else if(extraEndCollider.GetComponent<AirUpEnd>().collidingEnd)
        {
            Physics2D.IgnoreLayerCollision(groundLayer,playerLayer, false);
            player.GetComponent<PlayerMain>().airUp = false;
            if (!player.GetComponent<PlayerMain>().airUp && i == 1)
            {
                extraEndCollider.SetActive(false);
                player.GetComponent<PlayerMain>().interactionStun = false;
                i = 0;
                gameObject.tag = defaultTag;
                groundChk.mainTag = "Ground";
            }
        }
        playerConfirm();
    }
    // This method is called when the playerStats enters the trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Is colliding");
            colliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Is not colliding");
            colliding=false;
        }
    }
    void playerConfirm()
    {
        if (colliding && Input.GetKeyDown(KeyCode.E) && groundChk.onGround && i==0)
        {
            ++i;
            //interactionSymbl.SetActive(false);
        }
    }
}

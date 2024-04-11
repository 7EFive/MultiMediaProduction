using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem: MonoBehaviour
{
    [SerializeField] GameObject Exit;
    SceneChangeTrigger scriptExit;
    [SerializeField] GameObject HideItems;
    ShowItem showItem;

    private void Start()
    {
        scriptExit = Exit.GetComponent<SceneChangeTrigger>();
        showItem = HideItems.GetComponent<ShowItem>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            showItem.showItem=true;
            scriptExit.conditionsMade = true;
            if (scriptExit.conditionsMade)
            {
                Destroy(gameObject);
            }
        }
    }
}

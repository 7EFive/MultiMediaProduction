using UnityEngine;

public class NPC_managment : MonoBehaviour
{
    public SceneInfo sceneInfo;
    [Header("Test Dialog")]
    [SerializeField] GameObject npc1;
    [SerializeField] GameObject npc1_dialog;


   

    // Update is called once per frame
    void Update()
    {
        if (sceneInfo.wasDestroyed)
        {
            npc1.SetActive(false);
        }
    }
}

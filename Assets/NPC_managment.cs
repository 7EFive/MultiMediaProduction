using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_managment : MonoBehaviour
{
    public SceneInfo sceneInfo;
    public GameObject npc1;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!sceneInfo.wasDestroyed)
        {
            npc1.SetActive(true);
        }
    }
}

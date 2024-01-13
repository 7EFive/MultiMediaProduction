using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class SceneChangeTrigger : MonoBehaviour
{
    //public int sceneToLoad;
    public GameObject canvas;
    LevelLoader fade;
    //public GameObject player;

    private void Start()
    {
        fade = canvas.GetComponent<LevelLoader>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            
            Debug.Log("Level Change");
            fade.LoadNextLevel();
            //SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
}

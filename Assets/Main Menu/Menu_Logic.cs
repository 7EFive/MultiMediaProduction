//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Menu_Logic : MonoBehaviour
{
    public GameObject canvas;
    LevelLoader fade;

    void Start()
    {
        fade = canvas.GetComponent<LevelLoader>();
    }
    public void startGame() {
        fade.LoadNextLevel();
        //SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame() {
        Application.Quit();
    }
}

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Logic : MonoBehaviour
{
    public GameObject canvas;
    LevelLoader fade;
    Scene scene;

    void Start()
    {
        fade = canvas.GetComponent<LevelLoader>();
    }
    public void startGame() {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            fade.LoadNextLevel();
        }
        
        //SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame() {
        Application.Quit();
    }
}

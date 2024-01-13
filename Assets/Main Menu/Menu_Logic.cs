//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_Logic : MonoBehaviour
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

    [SerializeField] GameObject obj;
    public void ChangeActiveState() {
        obj.SetActive(!obj.activeSelf);
    }
}

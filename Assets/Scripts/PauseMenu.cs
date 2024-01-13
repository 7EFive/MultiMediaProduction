//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvas;
    LevelLoader fade;

    [SerializeField] GameObject menuPause;
    private static bool isPaused = false;

    void Start()
    {
        fade = canvas.GetComponent<LevelLoader>();
        
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            pauseGame();
        } else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            resumeGame();
        }
    }

    void pauseGame() {
        isPaused = true;
        menuPause.SetActive(true);
        Time.timeScale = 0f;
    }

    void resumeGame() {
        isPaused = false;
        menuPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void goToMainMenu() {
        Time.timeScale = 1f;
        fade.BackToMenu();
        //SceneManager.LoadScene("Main Menu");
    }

    public void test() {
        Debug.Log("Clicked");
    }
}
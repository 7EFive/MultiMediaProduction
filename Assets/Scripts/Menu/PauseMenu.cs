//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvas;
    LevelLoader fade;

    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject dialog;
    private static bool isPaused = false;

    void Start()
    {
        fade = canvas.GetComponent<LevelLoader>();
        Cursor.visible = false;
        
    }
    void Update() {
        if (isPaused)
        {
            dialog.SetActive(false);
        }
        else
        {
            dialog.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            pauseGame();
            Cursor.visible = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            resumeGame();
            Cursor.visible = false;
        }
    }

    void pauseGame() {
        
        isPaused = true;
        PlayerMain.isGamePaused = true;
        menuPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resumeGame() {
        isPaused = false;
        PlayerMain.isGamePaused = false;
        menuPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void goToMainMenu() {
        PlayerMain.isGamePaused = false;
        isPaused = false;
        Time.timeScale = 1f;
        fade.BackToMenu();
        
        //SceneManager.LoadScene("Main Menu");
    }
}
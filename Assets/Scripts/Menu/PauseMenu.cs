//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvas;
    LevelLoader fade;
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject dialog;
    private static bool isPaused = false;
    public bool isInDiffrentMenu;


    void Start()
    {
        isInDiffrentMenu=false;
        fade = canvas.GetComponent<LevelLoader>();
        Cursor.visible = false;
        
    }
    void Update() {
        if(dialog != null)
        {
            if (isPaused)
            {
                dialog.SetActive(false);
            }
            else
            {
                dialog.SetActive(true);
            }
        }
        if (isPaused)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            pauseGame();
        } else if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isInDiffrentMenu)
            {
                resumeGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isInDiffrentMenu)
            {
                ChangeActiveState();
            }
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
    public void ChangeActiveState()
    {
        isInDiffrentMenu = !isInDiffrentMenu;
        optionsCanvas.SetActive(!optionsCanvas.activeSelf);
        menuPause.SetActive(!menuPause.activeSelf);
    }
}
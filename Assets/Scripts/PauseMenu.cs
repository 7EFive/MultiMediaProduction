//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject menuPause;
    private static bool isPaused = false;

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
        SceneManager.LoadScene("Main Menu");
    }

    public void test() {
        Debug.Log("Clicked");
    }
}
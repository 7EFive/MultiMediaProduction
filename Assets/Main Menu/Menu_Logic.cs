//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_Logic : MonoBehaviour
{

    public void startGame() {
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame() {
        Application.Quit();
    }

    [SerializeField] GameObject obj;
    public void ChangeActiveState() {
        obj.SetActive(!obj.activeSelf);
    }
}

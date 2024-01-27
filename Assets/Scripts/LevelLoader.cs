using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator canvas;

    float transitionTime = 1f;
    // Load next level
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    // load to main menu
    public void BackToMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
    // load level with fade in and out
    IEnumerator LoadLevel(int levelIndex)
    {
        canvas.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}

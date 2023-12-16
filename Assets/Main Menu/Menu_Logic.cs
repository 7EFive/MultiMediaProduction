//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Buttons_Logic : MonoBehaviour
{
    public void QuitGame() {
        Application.Quit();
    }

    [SerializeField] GameObject obj;
    public void ChangeActiveState() {
        obj.SetActive(!obj.activeSelf);
    }
}

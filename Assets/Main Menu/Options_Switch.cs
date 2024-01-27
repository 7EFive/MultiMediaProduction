using UnityEngine;

public class Options_Canvas : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;
    public void ChangeActiveState() {
        optionsCanvas.SetActive(!optionsCanvas.activeSelf);
    }
}

using UnityEngine;

public class Options_Canvas : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] GameObject mainCanvasCanvas;
    public void ChangeActiveState() {
        optionsCanvas.SetActive(!optionsCanvas.activeSelf);
        mainCanvasCanvas.SetActive(!mainCanvasCanvas.activeSelf);
    }
    
}

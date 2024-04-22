using UnityEngine;

public class Options_Canvas : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] GameObject mainCanvasCanvas;
    [SerializeField] GameObject title;
    [SerializeField] GameObject CreditBG;
    
    public void ChangeActiveState() {
        if (CreditBG != null)
        {
            CreditBG.SetActive(!CreditBG.activeSelf);
        }
        title.SetActive(!title.activeSelf);
        optionsCanvas.SetActive(!optionsCanvas.activeSelf);
        mainCanvasCanvas.SetActive(!mainCanvasCanvas.activeSelf);
    }
    
}

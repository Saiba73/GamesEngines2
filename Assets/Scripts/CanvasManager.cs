using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Assign your canvases in the Inspector window
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject controlesCanvas;

    private void Start()
    {
        // Initialize the screen layout by default
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        //mainMenuCanvas.SetActive(true);
        controlesCanvas.SetActive(false);
    }

    public void ShowControles()
    {
        //mainMenuCanvas.SetActive(false);
        controlesCanvas.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject optionCanvas;
    public GameObject normalCanvas;
    public GameObject comandeCanvas;

    public void EnterOption()
    {
        optionCanvas.SetActive(true);
        if (normalCanvas != null)
            normalCanvas.SetActive(false);
    }
    public void ExitOption()
    {
        optionCanvas.SetActive(false);
        if (normalCanvas != null)
            normalCanvas.SetActive(true);
    }
    public void EnterComande()
    {
        comandeCanvas.SetActive(true);

        optionCanvas.SetActive(false);
    }
    public void ExitComande()
    {
        comandeCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }
}

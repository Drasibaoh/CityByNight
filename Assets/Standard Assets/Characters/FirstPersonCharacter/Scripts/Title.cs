using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject controlsUI;
    public void Controls()
    {
        if (controlsUI.activeSelf)
        {
            gameUI.SetActive(true);
            controlsUI.SetActive(false);
        }
        else
        {
            gameUI.SetActive(false);
            controlsUI.SetActive(true);
        }
    }
}

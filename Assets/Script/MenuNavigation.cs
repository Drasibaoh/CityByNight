using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuNavigation : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StartTheGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Win()
    {
        SceneManager.LoadScene(2);
    }
    public void Lose()
    {
        SceneManager.LoadScene(3);
    }    
}

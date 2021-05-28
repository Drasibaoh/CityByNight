using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class MenuNavigation : MonoBehaviour
{
    public VideoPlayer cred;
    public RawImage image;
    bool hasStarted;
    private void Start()
    {
        if(cred!=null)
            cred.Prepare();

    }
    private void Update()
    {
        if (hasStarted)
        {
            if (!cred.isPlaying)
            {
                cred.Prepare();
                image.gameObject.SetActive(false);
            }
        }
    }
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
    public void Credits()
    {
        image.gameObject.SetActive(true);
        cred.Play();
        hasStarted = true;
    }
    public void Skip()
    {
        cred.frame = 0;
        cred.Pause();
        image.gameObject.SetActive(false);
    }
    public void Lose()
    {
        SceneManager.LoadScene(3);
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Pause : MonoBehaviour
    {
        public FirstPersonController player;
        public GameObject pauseUI;
        public GameObject gameUI;
        public GameObject controlsUI;
        bool isStopped;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !isStopped)
            {
                pauseUI.SetActive(true);
                gameUI.SetActive(false); 
                player.enabled = false;
                isStopped = true;
                player.m_MouseLook.SetCursorLock(false);
                Time.timeScale = 0;
                Cursor.visible=true;
 
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && isStopped)
            {
                Game();
            }
        }
        public void Game()
        {
            pauseUI.SetActive(false);
            gameUI.SetActive(true);
            controlsUI.SetActive(false);
            Time.timeScale = 1;
            player.enabled = true;
            isStopped = false;
            player.m_MouseLook.Init(player.transform, player.m_Camera.transform);
            player.m_MouseLook.SetCursorLock(true);
            player.m_MouseLook.lockCursor = true;
        }
        public void Controls()
        {
            if (controlsUI.activeSelf)
            {
                pauseUI.SetActive(true);
                gameUI.SetActive(false);
                controlsUI.SetActive(false);
            }
            else
            {
                pauseUI.SetActive(false);
                gameUI.SetActive(false);
                controlsUI.SetActive(true);
            }

        }
    }
}
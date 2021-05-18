using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Pause : MonoBehaviour
    {
        public FirstPersonController player;
        public GameObject pauseUI;
        public GameObject gameUI;
        public GameObject controlsUI;
        public Playlist list;
        bool isStopped;
        public VideoPlayer crosshair;
        public VideoPlayer HUD;
        public List<Image> boutons;
        
        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isStopped)
            {
                Stop();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isStopped)
            {
                Game();
            }
        }
        public void Stop()
        {
            crosshair.Stop();
            pauseUI.SetActive(true);
            gameUI.SetActive(false);
            player.enabled = false;
            if (player.m_ControllerAddon.isDope)
            {
              for (int i=0; i < boutons.Count; i++)
                {
                    boutons[i].color = Color.green;
                }
            }
            isStopped = true;
            player.m_MouseLook.SetCursorLock(false);
            Time.timeScale = 0;
            Cursor.visible = true;
            list.Pause();
        }
        public void Game()
        {
            crosshair.Play();
            pauseUI.SetActive(false);
            gameUI.SetActive(true);
            controlsUI.SetActive(false);
            if (player.m_ControllerAddon.isDope)
            {
                for (int i = 0; i < boutons.Count; i++)
                {
                    boutons[i].color = Color.white;
                }
            }
            Time.timeScale = 1;
            player.enabled = true;
            isStopped = false;
            player.m_MouseLook.Init(player.transform, player.m_Camera.transform);
            player.m_MouseLook.SetCursorLock(true);
            player.m_MouseLook.lockCursor = true;
            list.Resume();
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
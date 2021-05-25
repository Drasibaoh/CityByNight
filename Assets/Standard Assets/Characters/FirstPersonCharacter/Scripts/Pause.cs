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
        public List<VideoPlayer> transitions;
        public List<RawImage> menu;
        public List<Image> boutons;
        
        // Start is called before the first frame update
        void Start()
        {
            GameManager.instance.restart.AddListener(Game);
        }

        // Update is called once per frame
        void Update()
        {

            if (Timestoper.instance.end)
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

        }
        public void Stop()
        {
            player.m_ControllerAddon.StopSound();
            crosshair.Stop();
            pauseUI.SetActive(true);
            gameUI.SetActive(false);
            player.enabled = false;
            menu[0].enabled = true;
            
            transitions[0].Play();
            menu[0].GetComponent<AudioSource>().Play();
            if (player.m_ControllerAddon.isDope)
            {
                for (int i=0; i < boutons.Count; i++)
                {
                    boutons[i].color = new Color(0.7f,1,0.7f);
                }
                for (int j = 0; j < menu.Count; j++)
                {
                    menu[j].color = new Color(0.7f, 1, 0.7f);
                    menu[j].gameObject.SetActive(true);
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
            
            if (player.m_ControllerAddon.isRecoiling)
                player.m_ControllerAddon.StartSound();
            crosshair.Play();
            pauseUI.SetActive(false);
            gameUI.SetActive(true);
            controlsUI.SetActive(false);
            menu[0].enabled=false;
            transitions[0].Prepare();
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
                transitions[1].frame = 1;
                Debug.Log(menu[1].color);
                transitions[1].Play();
                //menu[0].gameObject.SetActive(false);

            }
            else
            {
                pauseUI.SetActive(false);
                gameUI.SetActive(false);
                controlsUI.SetActive(true);                
                transitions[2].frame = 1;
                Debug.Log(menu[2].color);
                transitions[2].Play();
                //menu[0].gameObject.SetActive(false);

            }

        }
    }
}
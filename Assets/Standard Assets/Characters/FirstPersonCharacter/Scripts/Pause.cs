using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;
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
        bool isPaused;
        bool isTransIn;
        bool isTransOut;
        Color actColor = Color.white;
        // Start is called before the first frame update
        void Start()
        {
            GameManager.instance.restart.AddListener(Game);
            transitions[1].Prepare();
            for (int i = 0; i < menu.Count; i++)
            {

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused)
            {
                if (!transitions[0].isPlaying)
                {
                    pauseUI.SetActive(true);
                    isPaused = false;
                    transitions[0].Pause();
                }
            }
            if (isTransIn)
            {
                if (!transitions[1].isPlaying)
                {
                    controlsUI.SetActive(true);
                    isTransIn = false;
                }
            }
            if (isTransOut)
            {
                if (!transitions[2].isPlaying)
                {

                    pauseUI.SetActive(true);
                    isTransOut = false;
                }
            }
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
            menu[0].gameObject.SetActive(true);
            player.m_ControllerAddon.StopSound();
            crosshair.Stop();
            gameUI.SetActive(false);
            player.enabled = false;

            transitions[0].Play();
            isPaused = true;
            menu[0].GetComponent<AudioSource>().Play();
            if (player.m_ControllerAddon.isDope)
            {
                for (int i = 0; i < boutons.Count; i++)
                {
                    boutons[i].color = new Color(0.7f, 1, 0.7f);
                }
                for (int j = 0; j < menu.Count; j++)
                {
                    menu[j].color = new Color(0.7f, 1, 0.7f);
                    menu[j].gameObject.SetActive(true);
                }
                actColor = new Color(0.7f, 1, 0.7f, 1);

            }
            else
            {
                actColor = Color.white;
            }
            for (int i = 0; i < menu.Count; i++)
            {
                //menu[i].color = Color.clear;
                
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
            for (int i = 0; i < menu.Count; i++)
            {
                transitions[i].frame = 0;
                menu[i].gameObject.SetActive(false);
                //transitions[i].Prepare();
            }


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
                if (transitions[1].isPlaying)
                {
                    transitions[1].frame = (long)(transitions[1].frameCount - 1);

                }

                isTransOut = true;
                gameUI.SetActive(false);
                controlsUI.SetActive(false);
                transitions[2].frame = 0;
                Debug.Log(menu[2].color);
                transitions[2].Play();
                transitions[1].Prepare();
                //menu[0].gameObject.SetActive(false);
            }
            else
            {
                if (transitions[2].isPlaying)
                {
                    transitions[2].frame = (long)(transitions[2].frameCount - 1);

                }
                pauseUI.SetActive(false);
                gameUI.SetActive(false);
                isTransIn = true;
                transitions[1].frame = 0;
                Debug.Log(menu[1].color);
                transitions[1].Play();
                transitions[2].Prepare();


                //menu[0].gameObject.SetActive(false);

            }

        }
        public void Loop()
        {

        }
    }
}
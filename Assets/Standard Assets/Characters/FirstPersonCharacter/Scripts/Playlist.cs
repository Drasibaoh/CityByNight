using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;
namespace UnityStandardAssets.Characters.FirstPerson
{


    public class Playlist : MonoBehaviour
    {
        public int playingTrack = 0;
        public List<AudioClip> playlist;
        AudioSource phones;
        bool isPaused = true;
        bool isEmpty = false;
        bool started=false;
        public VideoPlayer spectrum;
        public List<VideoClip> spectrums;
        // Start is called before the first frame update
        void Start()
        {
            phones = GetComponent<AudioSource>();
            if (playingTrack < 0)
            {
                playingTrack = 0;
            }
            if (playlist.Count == 0)
            {
                isEmpty = true;
            }
            else
            {

            }


        }

        // Update is called once per frame
        void Update()
        {
            if (started)
            {
                if (!isEmpty)
                {
                    if (Time.timeScale > 0.2f)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            Back();
                        }
                        else if (Input.GetKeyDown(KeyCode.Mouse1))
                        {
                            Next();
                        }
                        if (Input.GetKeyDown(KeyCode.P))
                        {
                            if (!isPaused)
                                Pause();
                            else
                                Resume();
                        }
                        if (!phones.isPlaying && !isPaused)
                        {
                            Next();
                        }
                    }

                }
            }
            else
            {
                if (Timestoper.instance.end)
                {
                    started = true;
                    phones.clip = playlist[0];
                    spectrum.clip = spectrums[0];
                    spectrum.Play();
                    phones.Play();
                }
            }
           

        }
        public void Pause()
        {
            phones.Pause();
            isPaused = true;
            spectrum.Pause();
        }
        public void Resume()
        {
            phones.Play();
            isPaused = false;
            spectrum.Play();
        }
        public void Next()
        {
            playingTrack++;
            if (playingTrack > playlist.Count - 1)
                playingTrack = 0;
            phones.clip = playlist[playingTrack];
            spectrum.clip = spectrums[playingTrack];
            phones.Play();
            spectrum.Play();

        }
        public void Back()
        {
            playingTrack--;
            if (playingTrack < 0)
                playingTrack = playlist.Count - 1;
            phones.clip = playlist[playingTrack];
            spectrum.clip = spectrums[playingTrack];
            phones.Play();
            spectrum.Play();
        }

    }
}
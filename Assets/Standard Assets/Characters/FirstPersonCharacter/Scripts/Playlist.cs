﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Playlist : MonoBehaviour
{
    public int playingTrack=0;
    public List<AudioClip> playlist;
    AudioSource phones;
    bool isNotPaused=true;
    // Start is called before the first frame update
    void Start()
    {
        phones = GetComponent<AudioSource>();
        phones.clip = playlist[playingTrack];
    }

    // Update is called once per frame
    void Update()
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
            if (isNotPaused)
                Pause();
            else
                Resume();
        }
        if (!phones.isPlaying && isNotPaused)
        {
            Next();
        }
    }
    public void Pause()
    {
        phones.Pause();
        isNotPaused = false;
    }
    public void Resume()
    {
        phones.Play();
        isNotPaused = true;
    }
    public void Next()
    {
        playingTrack++;
        if (playingTrack > playlist.Count - 1)
            playingTrack = 0;
        phones.clip = playlist[playingTrack];
        phones.Play();
    }
    public void Back()
    {
        playingTrack--;
        if (playingTrack < 0)
            playingTrack = playlist.Count - 1;
        phones.clip = playlist[playingTrack];
        phones.Play();
    }
    
}
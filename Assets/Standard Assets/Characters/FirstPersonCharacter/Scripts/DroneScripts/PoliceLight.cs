﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class PoliceLight : MonoBehaviour
    {
        public float timeInLight = 0f;
        public float speed = 1;
        public bool isLost = false;
        public bool noMove;
        public bool isIn;
        public AIAgent agent = null;
        private PostProcessVolume LightEffect;
        private ControllerAddon player;

        // Start is called before the first frame update
        void Start()
        {
            LightEffect = GetComponent<PostProcessVolume>();
            GameManager.instance.restart.AddListener(Return);
        }

        // Update is called once per frame
        void Update()
        {
            if (isIn)
            {
                timeInLight += Time.deltaTime;

                if (timeInLight >= 0.8f)
                {
                    agent.navAgent.enabled = true;
                    player.respawnPoint.Death();
                  
                }
            }
            if (isLost)
            {
                timeInLight -= Time.deltaTime * speed;
                if (timeInLight <= 0)
                {

                    noMove = false;
                    timeInLight = 0;
                    isLost = false;
                }
            }
            LightEffect.weight = timeInLight / 1.4f;
            

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (player==null)
                    player = other.GetComponent<ControllerAddon>();
                agent.navAgent.enabled = false;
                noMove = true;
                isLost = false;
                isIn = true;

            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (agent!=null)
                    agent.navAgent.enabled = true;
                Invoke("Wait", 0.5f);
                isLost = true;
                isIn = false;
            }
        }
        public void Return()
        {
            isIn = false;
            noMove = false;
            timeInLight = 0;
            isLost = false;
        }
        public void Wait()
        {
            if (agent != null)
                agent.m_fsm.ChangeState(StateNames.AI_PATROL);
        }
    }
}
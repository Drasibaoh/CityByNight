﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Wall : MonoBehaviour
    {
        float x;
        float z;
        bool isIn;
        bool jump;
        public ControllerAddon actor;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isIn && Input.GetKeyDown(KeyCode.Space) && !jump)
                {
                    Debug.Log(actor.fpControler.m_MoveDir);
                    actor.fpControler.m_MoveDir.y = actor.fpControler.m_JumpSpeed;
                    jump = true;
                }
            
        }
        private void OnTriggerEnter(Collider other)
        {
            jump = false;
            actor = other.GetComponent<ControllerAddon>();
            actor.isOnWall = true;
            actor.fpControler.m_GravityMultiplier=0;
            x = actor.rigibody.velocity.x;
            z = actor.rigibody.velocity.z;
            isIn = true;
            actor.rigibody.velocity = Vector3.zero;
            Invoke("GetOnGround", 0.5f);
        }
        public void GetOnGround()
        {
            actor.fpControler.m_GravityMultiplier = 2;

        }
        private void OnTriggerExit(Collider other)
        {
            isIn = false;
            actor.isOnWall = false;
            if (IsInvoking())
            {
                CancelInvoke();
            }

            actor.fpControler.m_GravityMultiplier = 2;
        }
    }
}

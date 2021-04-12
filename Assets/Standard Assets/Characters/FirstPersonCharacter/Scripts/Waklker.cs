using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Waklker : MonoBehaviour
    {
        public Respawn poli;
        public Rigidbody rigibody;
        public float walkSpeed;
        public float accel = 0.1f;
        public FirstPersonController e;
        public CharacterController player;
        public Vector3 center;
        public float height;
        private float timer;
        public float fallHeight;
        public float deathFall;
        public bool falling;
        // Start is called before the first frame update
        void Start()
        {
            
            walkSpeed = e.m_WalkSpeed;
            player = GetComponent<CharacterController>();
            height = player.height;
            center = player.center;
            walkSpeed = e.m_WalkSpeed;
            
        }

        // Update is called once per frame
        void Update()
        {


            if (Input.GetKey(KeyCode.LeftShift))
            {
                e.m_WalkSpeed = walkSpeed;
            }
            else
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    if (e.m_WalkSpeed <= e.m_RunSpeed)
                        e.m_WalkSpeed += 1 * accel;
                    else
                    {
                        e.m_WalkSpeed -= 1 * accel;
                    }
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    if (e.m_WalkSpeed <= e.m_RunSpeed - 7)
                        e.m_WalkSpeed += 1 * accel;
                    else
                    {
                        e.m_WalkSpeed -= 1 * accel;
                    }
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    if (e.m_WalkSpeed <= e.m_RunSpeed - 7)
                        e.m_WalkSpeed += 1 * accel;
                    else
                    {
                        e.m_WalkSpeed -= 1 * accel;
                    }
                }
                if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Q))
                {
                    e.m_WalkSpeed = walkSpeed;
                }
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {

                player.center = new Vector3(player.center.x, 0.6f, player.center.z);
                player.height = 1.2f;
                if (IsInvoking("GetUp"))
                {
                    CancelInvoke("GetUp");
                }
                Invoke("GetUp", 1f);
            }
            
            if (player.isGrounded)
            {
                if (fallHeight - deathFall > transform.position.y)
                {
                    poli.Death();
                }

                fallHeight = transform.position.y;
                falling = false;
                
            }
            else if (!falling)
            {
                fallHeight = transform.position.y;
                falling = true;
            }
           /* if (!player.isGrounded)   falldamage test 1
            {
                timer += Time.deltaTime;
                
            }
            else
            {
                if (timer>=2f)
                    Debug.Log("death");
               timer = 0;
            }*/
        }

        public void GetUp()
        {
            player.center = center;

            player.height = height;

        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class ControllerAddon : MonoBehaviour
    {
        public Respawn respawnPoint;
        public Rigidbody rigibody;
        public float walkSpeed;
        public float accel = 0.1f;
        public FirstPersonController fpControler;
        public CharacterController player;
        public Vector3 center;
        public float height;
        public float slidetime;
        private float timer;
        public float fallHeight;
        public float deathFall;
        public bool falling;
        public int DopeCount=3;
        public bool isSliding;
        // Start is called before the first frame update
        void Start()
        {
            
            walkSpeed = fpControler.m_WalkSpeed;
            player = GetComponent<CharacterController>();
            height = player.height;
            center = player.center;
            walkSpeed = fpControler.m_WalkSpeed;
            
        }

        // Update is called once per frame
        void Update()
        {


            if (Input.GetKey(KeyCode.LeftShift))
            {
                fpControler.m_WalkSpeed = walkSpeed;
            }
            else
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    if (fpControler.m_WalkSpeed <= fpControler.m_RunSpeed)
                        fpControler.m_WalkSpeed += 1 * accel;
                    else
                    {
                        fpControler.m_WalkSpeed -= 1 * accel;
                    }
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    if (fpControler.m_WalkSpeed <= fpControler.m_RunSpeed - 7)
                        fpControler.m_WalkSpeed += 1 * accel;
                    else
                    {
                        fpControler.m_WalkSpeed -= 1 * accel;
                    }
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    if (fpControler.m_WalkSpeed <= fpControler.m_RunSpeed - 7)
                        fpControler.m_WalkSpeed += 1 * accel;
                    else
                    {
                        fpControler.m_WalkSpeed -= 1 * accel;
                    }
                }
                if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Q))
                {
                    fpControler.m_WalkSpeed = walkSpeed;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (fpControler.m_WalkSpeed >= 8)
                {
                    if (!isSliding)
                        Slide();
                }
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                slidetime += Time.deltaTime;
                if (slidetime > 2)
                    GetUp();

            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                if (slidetime <= 1)
                    Invoke("GetUp", 1 - slidetime);
                else
                {
                    GetUp();
                    if (IsInvoking("GetUp"))
                    {
                        CancelInvoke("GetUp");
                    }
                }
            }

            if (player.isGrounded)
            {
                if (fallHeight - deathFall > transform.position.y)
                {
                    respawnPoint.Death();
                }

                fallHeight = transform.position.y;
                falling = false;

            }
            else if (!falling)
            {
                fallHeight = transform.position.y;
                falling = true;
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (!IsInvoking("DopeTime") && !IsInvoking("ReturnToNormal") && DopeCount > 0)
                {
                    DopeCount--;
                    walkSpeed += 10;
                    fpControler.m_WalkSpeed = walkSpeed;
                    fpControler.m_RunSpeed += 10;
                    fpControler.m_JumpSpeed += 5;
                    Invoke("DopeTime", 10f);
                }
                else
                    Debug.Log("nope");
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
        public void DopeTime()
        {
            walkSpeed -= 10;
            fpControler.m_WalkSpeed -= 10;
            fpControler.m_RunSpeed -= 10;
            fpControler.m_JumpSpeed -= 5;
            DopeDownTime();
        }
        public void DopeDownTime()
        {
            walkSpeed -= 3;
            fpControler.m_RunSpeed -= 4;
            fpControler.m_JumpSpeed -= 2;
            Invoke("ReturnToNormal", 4f);
        }
        public void ReturnToNormal()
        {
            walkSpeed += 3;
            fpControler.m_RunSpeed += 4;
            fpControler.m_JumpSpeed += 2;
        }
        public void Slide() 
        {
            fpControler.m_WalkSpeed += 2;
            fpControler.m_RunSpeed = 17;
            player.center = new Vector3(player.center.x, 0.6f, player.center.z);
            player.height = 1.2f;
            isSliding = true;
        }
        public void GetUp()
        {
            walkSpeed = 5;
            fpControler.m_WalkSpeed = walkSpeed;
            fpControler.m_RunSpeed = 15;
            player.center = center;
            slidetime = 0;
            player.height = height;
            isSliding = false;
        }
    }
}


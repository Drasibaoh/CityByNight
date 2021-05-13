using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
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
        public int DopeCount = 3;
        public bool isSliding;
        public bool headBlock;
        public bool isOnWall;
        public PostProcessVolume DopePP;
        public PostProcessVolume RecoilPP;
        bool isDope;
        public int walljumps;
        int maxwalljumps = 3;
        float t;
        float t2;
        bool recoilOver=true;
        public GameObject BoostBar;
        float dopetime;
        float dopedowntime;
        public List<Image> dose;
        // Start is called before the first frame update
        void Start()
        {
            walljumps = maxwalljumps;
            walkSpeed = fpControler.m_WalkSpeed;
            player = GetComponent<CharacterController>();
            height = player.height;
            center = player.center;
            walkSpeed = fpControler.m_WalkSpeed;
            GameManager.instance.restart.AddListener(Death);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameManager.instance.restart.Invoke();
            }

            else
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

                }
            }

            if (player.isGrounded)
            {
                if (fallHeight - deathFall > transform.position.y)
                {
                    Debug.Log("eee");
                    respawnPoint.Death();
                }
                fallHeight = transform.position.y;
                falling = false;
                walljumps = maxwalljumps;
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
                    for (int i = 0; i < dose.Count; i++)
                    {
                        if (i <= DopeCount-1)
                            dose[i].color = Color.white;
                        else 
                            dose[i].color = Color.clear;
                    }
                    isDope = true;
                    Debug.Log("walk speed" + fpControler.m_WalkSpeed);
                    Debug.Log("Run speed" + fpControler.m_RunSpeed);
                    walkSpeed += 10;
                    fpControler.m_WalkSpeed = walkSpeed;
                    fpControler.m_RunSpeed += 10;
                    fpControler.m_JumpSpeed += 3;
                    Debug.Log("walk speed" + fpControler.m_WalkSpeed);
                    Debug.Log("Run speed" + fpControler.m_RunSpeed);
                    Invoke("DopeTime", dopetime);
                    
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
            if (headBlock)
            {
                GetUp();
                Debug.Log("yo");
            }
            if (walkSpeed < 2)
            {
                walkSpeed = 2;
                fpControler.m_WalkSpeed = walkSpeed;
            }
            //dope
            if (isDope)
            {
                t += 0.4f * Time.deltaTime;
                t = Mathf.Clamp(t, 0, 1);
                DopePP.weight = Mathf.Lerp(DopePP.weight, 1, t);
                dopetime -= Time.deltaTime;
                BoostBar.transform.localScale = new Vector3(dopetime/10, 1, 1);
            }
            else
            {
                dopetime = 10;
                t -= 0.4f * Time.deltaTime;
                t = Mathf.Clamp(t, 0, 1);
                
                DopePP.weight = Mathf.Lerp(0, DopePP.weight, t);
            }
            if (IsInvoking("ReturnToNormal"))
            {
                t2 += 0.4f*Time.deltaTime;
                t2 = Mathf.Clamp(t2, 0, 1);
                dopedowntime =Mathf.Clamp(dopedowntime+ Time.deltaTime,0,4);
                BoostBar.transform.localScale = new Vector3(dopedowntime/4, 1, 1);
                RecoilPP.weight = Mathf.Lerp(RecoilPP.weight, 1, t2);


            }
            if (IsInvoking("Wait"))
                {
                    recoilOver = false;
                }
            if (!recoilOver)
            {
                t2 -= 0.4f * Time.deltaTime;
                t2 = Mathf.Clamp(t2, 0, 1);
                RecoilPP.weight = Mathf.Lerp(0, RecoilPP.weight, t2);
                if (t2 <= 0)
                {
                    recoilOver = true;
                }
            }

        }
        public void DopeTime()
        {
            Debug.Log("walk speed" + fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
            isDope=false;
            walkSpeed -= 10;
            fpControler.m_WalkSpeed = walkSpeed;
            fpControler.m_RunSpeed -= 10;
            fpControler.m_JumpSpeed -= 3;
            Debug.Log("walk speed"+fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
            DopeDownTime();
        }
        public void DopeDownTime()
        {

          //  DopePP.profile.settings.Find(settings => settings.name == PostProcessNames.PP_DEPTH_OF_FIELD).active = true;
            
            //  walkSpeed = Mathf.Clamp(-3, 1, 140);
            Debug.Log("walk speed" + fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
            walkSpeed -= 3;
            fpControler.m_WalkSpeed = walkSpeed;
            fpControler.m_RunSpeed -= 4;
            fpControler.m_JumpSpeed -= 2;
            Debug.Log("walk speed" + fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
            Invoke("ReturnToNormal", 4f);
        }
        public void ReturnToNormal()
        {
            Debug.Log("walk speed" + fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
            dopedowntime = 0;
            walkSpeed += 3;
            fpControler.m_WalkSpeed = walkSpeed;
            fpControler.m_RunSpeed += 4;
            fpControler.m_JumpSpeed += 2;
            Invoke("Wait",4f);
            Debug.Log("walk speed" + fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
        }
        public void Slide()
        {

            walkSpeed += 2;
            fpControler.m_WalkSpeed = walkSpeed;
            fpControler.m_RunSpeed += 2;
            player.center = new Vector3(player.center.x, 0.6f, player.center.z);
            player.height = 1.2f;
            isSliding = true;
            

        }
        public void GetUp()
        {
            if (IsInvoking("GetUp"))
            {
                CancelInvoke("GetUp");
            }
            int layerMask = 1;
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), out Hit, 3, layerMask))
            {

                Debug.DrawRay(transform.position, transform.up * Hit.distance, Color.red);
                Debug.Log("hit" + Hit.collider.tag);
                headBlock = true;

            }
            else
            {

                    walkSpeed -= 2;
                    fpControler.m_WalkSpeed = walkSpeed;
                    fpControler.m_RunSpeed -= 2;
                    player.center = center;
                    slidetime = 0;
                    player.height = height;
                    isSliding = false;
                    headBlock = false;
            }
        }
        public void Wait()
        {

        }
        public void Death()
        {
            GetUp();
            walkSpeed = 5;
            fpControler.m_WalkSpeed = walkSpeed;
            fpControler.m_RunSpeed = 15;
            fpControler.m_JumpSpeed = 7;
            isDope = false;
            DopePP.weight = 0;
            RecoilPP.weight = 0;
            CancelInvoke();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.Video;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class ControllerAddon : MonoBehaviour
    {
        public Respawn respawnPoint;
        public Rigidbody rigibody;
        public Animator charcater;
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
        public float slideHeight=1.2f;
        public bool isSliding;
        public bool isAutoSliding;
        public bool headBlock;
        public bool isOnWall;
        public PostProcessVolume DopePP;
        public PostProcessVolume RecoilPP;
        [SerializeField] GameObject NormalUi;
        [SerializeField] GameObject BoostedUi;
        public bool isDope;
        public int walljumps;
        int maxwalljumps = 3;
        float t;
        float t2;
        bool recoilOver=true;
        public GameObject BoostBar;
        public GameObject BBoostBar;
        float dopetime;
        float dopedowntime;
        public List<Image> dose;
        public List<Image> Bdose;
        public RawImage CrossHair;
        public RawImage HUD;
        public VideoPlayer CH;
        public VideoPlayer HD;
        public RawImage Spectrum;
        Color spectrCol;
        [SerializeField] private AudioClip exhaustion;
        [SerializeField] private AudioClip gulp;
        [SerializeField] private AudioSource added;
        bool started=false;
        public bool isRecoiling = false;
        public GameObject mesh;
        private Playlist playlist;
        // Start is called before the first frame update
        void Start()
        {
            walljumps = maxwalljumps;
            walkSpeed = fpControler.m_WalkSpeed;
            player = GetComponent<CharacterController>();
            height = player.height;
            center = player.center;
            walkSpeed = fpControler.m_WalkSpeed;
            spectrCol = Spectrum.color;
            playlist = GetComponentInChildren<Playlist>();
            Debug.Log(playlist);
            GameManager.instance.restart.AddListener(Death);
            GameManager.instance.Over.AddListener(Over);
            HD.Play();
            CH.Play();
            CH.Pause();
            HD.Pause();
        }

        // Update is called once per frame
        void Update()
        {
            if (!started)
            {
                if (Timestoper.instance.end)
                {
                    HD.Play();
                    CH.Play();
                    started = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                respawnPoint.Death();
                //GameManager.instance.restart.Invoke();
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
                        if (isSliding)
                        {
                            
                        }
                        else
                        {
                            if (fpControler.m_WalkSpeed <= fpControler.m_RunSpeed)
                                fpControler.m_WalkSpeed += 1 * accel * Time.deltaTime;
                            else
                            {
                                fpControler.m_WalkSpeed -= 1 * accel * Time.deltaTime;
                            }
                            charcater.SetFloat("z", 1);
                        }

                    }

                    else if (Input.GetKeyDown(KeyCode.Q))
                    {
                        /*if (fpControler.m_WalkSpeed <= fpControler.m_RunSpeed - 7)
                            fpControler.m_WalkSpeed += 1 * accel;
                        else
                        {
                            fpControler.m_WalkSpeed -= 1 * accel;
                        }*/
                        fpControler.m_WalkSpeed = 10;
                        charcater.SetFloat("X", 1);
                        //mesh.transform.position = new Vector3(mesh.transform.position.x+0.2f, mesh.transform.position.y, mesh.transform.position.z);
                        mesh.transform.Rotate(new Vector3(0,-90,0));
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        fpControler.m_WalkSpeed = 10;
                        charcater.SetFloat("X", -1);
                        //mesh.transform.position = new Vector3(mesh.transform.position.x-0.2f, mesh.transform.position.y, mesh.transform.position.z);
                        mesh.transform.Rotate(new Vector3(0, 90, 0));
                    }

                
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        charcater.SetFloat("z", -1);
                    }

                    if (Input.GetKeyUp(KeyCode.Z))
                    {
                        fpControler.m_WalkSpeed = walkSpeed;
                        charcater.SetFloat("z", 0);
                    }
                    else if (Input.GetKeyUp(KeyCode.Q))
                    {
                        charcater.SetFloat("X", 0);
                        //mesh.transform.position = new Vector3(mesh.transform.position.x - 0.2f, mesh.transform.position.y, mesh.transform.position.z);
                        mesh.transform.Rotate(new Vector3(0, 90, 0));

                    }
                    else if (Input.GetKeyUp(KeyCode.D))
                    {
                        charcater.SetFloat("X", 0);
                        //mesh.transform.position = new Vector3(mesh.transform.position.x + 0.2f, mesh.transform.position.y, mesh.transform.position.z) ;
                        mesh.transform.Rotate(new Vector3(0, -90, 0));

                    }
                    else if (Input.GetKeyUp(KeyCode.S))
                    {
                        charcater.SetFloat("z", 0);
                    }
                }
            }
         
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (fpControler.m_WalkSpeed >= 8)
                {
                    if (!isSliding && !isAutoSliding)
                        Slide();
                }
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
               
                slidetime += Time.deltaTime;

                if (slidetime > 1.4f)
                    GetUp();

            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                if (isSliding && !isAutoSliding)
                {
                    if (slidetime <= 0.7f)
                        Invoke("GetUp", 0.7f - slidetime);
                    else
                    {
                        GetUp();
                    }
                }

            }

            if (player.isGrounded)
            {
                if (fallHeight - deathFall > transform.position.y)
                {
                    Debug.Log("fall death");
                 //   respawnPoint.Death();
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
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!IsInvoking("DopeTime") && !IsInvoking("ReturnToNormal") && DopeCount > 0)
                {
                    DopeCount--;
                    for (int i = 0; i < dose.Count; i++)
                    {
                        if (i <= DopeCount - 1)
                        {
                            dose[i].color = Color.white;
                        }
                        else
                        {
                            Bdose[i].color = Color.clear;
                            dose[i].color = Color.clear;
                        }
                    }
                    playlist.tracklist.color = new Color(0.7f, 1f, 0.7f);
                    Spectrum.color = new Color(0.7f,1f,0.7f);
                    HUD.color = new Color(0.7f, 1f, 0.7f);
                    CrossHair.color = new Color(0.7f, 1, 0.7f);
                    added.clip = gulp;
                    added.Play();
                    NormalUi.SetActive(false);
                    BoostedUi.SetActive(true);
                    isDope = true;
                    Debug.Log("walk speed" + fpControler.m_WalkSpeed);
                    Debug.Log("Run speed" + fpControler.m_RunSpeed);
                    walkSpeed += 5;
                    fpControler.m_WalkSpeed = walkSpeed;
                    fpControler.m_RunSpeed += 5;
                    fpControler.m_JumpSpeed += 1;
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
                BBoostBar.transform.localScale = new Vector3(dopetime / 10, 1, 1);
                BoostBar.transform.localScale = new Vector3(dopetime / 10, 1, 1);
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
                BoostBar.transform.localScale = new Vector3(dopedowntime / 4, 1, 1);
                BBoostBar.transform.localScale = new Vector3(dopedowntime / 4, 1, 1);
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
            if (isSliding && Input.GetKeyDown(KeyCode.Space))
            {
                CancelInvoke("GetUp");
                GetUp();
                fpControler.m_MoveDir.y = fpControler.m_JumpSpeed + 2;
                Debug.Log("Has Cancelled");
            }
            if (isSliding)
            {
                Debug.Log(fpControler.m_CharacterController.isGrounded);
                player.Move(transform.forward * 4*Time.deltaTime);
            }

        }
        public void Over()
        {
            fpControler.m_MouseLook.Init(fpControler.transform, fpControler.m_Camera.transform);
            fpControler.m_MouseLook.SetCursorLock(false);
           fpControler.m_MouseLook.lockCursor = false;
        }
        public void DopeTime()
        {
            Debug.Log("walk speed" + fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
            isDope=false;
            walkSpeed -=5;
            fpControler.m_WalkSpeed = walkSpeed;
            fpControler.m_RunSpeed -= 5;
            fpControler.m_JumpSpeed -= 1;
            Debug.Log("walk speed"+fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
            DopeDownTime();
        }
        public void DopeDownTime()
        {

            //  DopePP.profile.settings.Find(settings => settings.name == PostProcessNames.PP_DEPTH_OF_FIELD).active = true;

            //  walkSpeed = Mathf.Clamp(-3, 1, 140);
            isRecoiling = true;
            HUD.color = Color.white;
            CrossHair.color = Color.white;
            Spectrum.color = spectrCol;
            playlist.tracklist.color = new Color(0.07843138f, 0.7098039f, 0.6627451f);
            BoostedUi.SetActive(false);
            NormalUi.SetActive(true);
            added.clip = exhaustion;
            added.Play();
            added.loop = true;
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
            isRecoiling = false;
            Debug.Log("walk speed" + fpControler.m_WalkSpeed);
            Debug.Log("Run speed" + fpControler.m_RunSpeed);
            added.loop = false;
            added.Pause();
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
           // fpControler.m_WalkSpeed -=2;
           // fpControler.m_RunSpeed += 2;
            player.center = new Vector3(player.center.x, 0.6f, player.center.z);
            player.height = slideHeight;
            mesh.transform.position = new Vector3(mesh.transform.position.x, mesh.transform.position.y+slideHeight, mesh.transform.position.z);
            isSliding = true;
            charcater.SetTrigger("Slide");

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

                //  walkSpeed -= 2;
                fpControler.m_WalkSpeed -= 3;
                //  fpControler.m_RunSpeed -= 2;
                //    walkSpeed += 2;
                player.center = center;
                slidetime = 0;
                player.height = height;
                isSliding = false;
                headBlock = false;
                mesh.transform.position = new Vector3(mesh.transform.position.x, mesh.transform.position.y - slideHeight, mesh.transform.position.z);
                player.Move(transform.up * 4 * Time.deltaTime);
            }
        }
        public void Wait()
        {

        }
        public void Death()
        {
            HUD.color = Color.white;
            CrossHair.color = Color.white;
            Spectrum.color = new Color(0.1019608f, 0.7490196f, 0.7490196f);
            playlist.tracklist.color = new Color(0.07843138f, 0.7098039f, 0.6627451f);
            BoostedUi.SetActive(false);
            NormalUi.SetActive(true);
            BoostBar.transform.localScale = Vector3.one;
            BBoostBar.transform.localScale = Vector3.one;
            if (!isSliding)
            {
                mesh.transform.position = new Vector3(mesh.transform.position.x, mesh.transform.position.y + 1.3f, mesh.transform.position.z);
            }
            GetUp();
            walkSpeed = 5;
            fpControler.m_WalkSpeed = walkSpeed;
            fpControler.m_RunSpeed = 15;
            fpControler.m_JumpSpeed = 7;
            mesh.transform.rotation = new Quaternion(0,0,0,0);
            isDope = false;
            DopePP.weight = 0;
            RecoilPP.weight = 0;
            added.clip = null;
            CancelInvoke();
        }
        public void StopSound()
        {
            added.Pause();
        }
        public void StartSound()
        {
            added.Play();
        }
    }
}


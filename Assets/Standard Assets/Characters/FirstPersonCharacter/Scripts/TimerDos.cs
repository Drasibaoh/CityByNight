using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class TimerDos : MonoBehaviour
    {
        public float timer = 0.0f;
        public float maxtimer = 10.0f;
        public float actualtimer = 0.0f;
        float savedMin;
        float savedS;
        public bool Timsup = false;
        [SerializeField]private float min;
        public Text line;
        public Text BoostedLine;
        [SerializeField] GameObject NormalUi;
        // Start is called before the first frame update
        private void Awake()
        {
            GameManager.instance.timerd = this;
        }
        void Start()
        {
            GameManager.instance.ChangeObjectif();
            if (maxtimer<=60)
                 timer = maxtimer;
            else
            {
                timer = maxtimer%60;
                min = maxtimer / 60 - maxtimer%1;
            }
            SaveTimer();
           // GameManager.instance.restart.AddListener(ResetTimer);
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.instance.end)
            {
                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    if (min > 0)
                    {
                        min--;
                        timer = 60;
                    }
                        Debug.Log("e");
                }
            }
            if (NormalUi.activeSelf)
            line.text = (min-min%1)+": "+((timer-timer%0.1));
            else
                BoostedLine.text= (min - min % 1) + ": " + ((timer - timer % 0.1));

        }
        public void ResetTimer()
        {
            min = savedMin;
            timer = savedS;
        }
        public void AddTime(float newtime)
        {
            float newmin=newtime/60 - (newtime/60) % 1;
            newtime -= newmin * 60;
            if (newtime + timer > 120)
            {
                timer += newtime - 120;
                min += newmin + 2;
            }
            else if (newtime + timer > 60)
            {
                timer += newtime - 60; // 50+30-60=20
                min += newmin + 1;
            }
            else
            {
                timer += newtime;
                min += newmin;
            }
            SaveTimer();
            
        }
        public void SaveTimer()
        {
            savedMin = min;
            savedS = timer;
        }
        public void FinalSave()
        {
            GameManager.instance.finalMin = min;
            GameManager.instance.finalTime = timer;
        }
        /*private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Livraison1"))
            {
                actualtimer = timer;
                timer = maxtimer + actualtimer;
            }
            if (other.CompareTag("Livraison2"))
            {
                actualtimer = timer;
                timer = maxtimer + actualtimer;
            }
            if (other.CompareTag("Livraison3"))
            {
                Timsup = true;
                if (timer <= 0)
                {
                    Debug.Log("Lose");
                }
                else if (timer > 0)
                {
                    Debug.Log("Win");
                }
            }
        }*/
    }
}
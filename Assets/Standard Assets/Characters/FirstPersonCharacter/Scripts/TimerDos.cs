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
        public bool Timsup = false;
        private float min;
        public Text line;

        // Start is called before the first frame update
        void Start()
        {
            if (maxtimer<=60)
                 timer = maxtimer;
            else
            {
                timer = maxtimer%60;
                min = maxtimer / 60;
            }
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
                        Debug.LogError("e");
                }
            }
            line.text = (min-min%1)+": "+((timer-timer%0.1));
            
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
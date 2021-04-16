using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class FaillingLight : MonoBehaviour
    {
        public GameObject droneLight;
        public float offTime = 1f;
        public float onTime = 2f;
        public bool off;
        private float timer = 0f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (off)
            {
                timer += Time.deltaTime;
                if (timer >= offTime)
                {
                    droneLight.SetActive(true);
                    timer = 0;
                    off=!off;
                }
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= onTime)
                {
                    droneLight.SetActive(false);
                    timer = 0;
                    off = !off;
                }
            }
        }
    }
}
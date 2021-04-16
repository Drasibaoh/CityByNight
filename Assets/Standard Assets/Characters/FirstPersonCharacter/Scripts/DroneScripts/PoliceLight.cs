using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class PoliceLight : MonoBehaviour
    {
        public float timeInLight = 0f;
        public float speed = 1;
        public bool isLost = false;
        public bool noMove;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
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
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                noMove = true;
                timeInLight += Time.deltaTime;
                if (timeInLight >= 1.2f)
                {
                    other.GetComponent<ControllerAddon>().respawnPoint.Death();
                    timeInLight = 0;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isLost = true;
                
            }
        }
    }
}
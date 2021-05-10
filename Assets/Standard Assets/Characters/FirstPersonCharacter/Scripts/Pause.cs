using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Pause : MonoBehaviour
    {
        public FirstPersonController player;
        bool isStopped;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isStopped)
            {
                Time.timeScale = 0;
                player.enabled = false;
                isStopped = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isStopped)
            {
                Time.timeScale = 1;
                player.enabled = true;
                isStopped = false;
            }
        }
    }
}
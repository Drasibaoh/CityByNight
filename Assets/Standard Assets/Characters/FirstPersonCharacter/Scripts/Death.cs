using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Death : MonoBehaviour
    {
        ControllerAddon player;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (player==null)
                    player = other.GetComponent<ControllerAddon>();
                player.Death();
            }
        }
    }
}
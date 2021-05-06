using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Delivery : MonoBehaviour
    {
        bool hasDelivered;
        bool isInteractable;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isInteractable)
            {
                if (Input.GetKeyDown(KeyCode.E) && !hasDelivered)
                {
                    GameManager.instance.ChangeObjectif();
                    hasDelivered = true;
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInteractable = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInteractable = false;
            }
        }
    }
}
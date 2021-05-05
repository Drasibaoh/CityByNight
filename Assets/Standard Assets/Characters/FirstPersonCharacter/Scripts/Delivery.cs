using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Delivery : MonoBehaviour
    {
        bool hasDelivered;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && !hasDelivered)
                {
                    GameManager.instance.ChangeObjectif();
                    hasDelivered = true;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class CheckPoint : MonoBehaviour
    {
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
            Debug.Log("hey ho");
            if (other.CompareTag("Player"))
            {
                other.GetComponent<ControllerAddon>().respawnPoint.transform.position = transform.position;
            }
        }
    }
}
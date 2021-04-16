using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.FirstPerson
{
    public class AutoSlider : MonoBehaviour
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
            if (other.CompareTag("Player"))
            {
                ControllerAddon yolo= other.GetComponent<ControllerAddon>();
                yolo.player.center = new Vector3(yolo.player.center.x, 0.6f, yolo.player.center.z);
                yolo.player.height = 1.2f;
                yolo.isSliding = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<ControllerAddon>().GetUp();
                
            }
        }
    }
}
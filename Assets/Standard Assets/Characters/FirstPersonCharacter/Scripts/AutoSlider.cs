using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.FirstPerson
{
    public class AutoSlider : MonoBehaviour
    {
        private ControllerAddon addon;
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
                if (addon==null)
                    addon= other.GetComponent<ControllerAddon>();
                addon.player.center = new Vector3(addon.player.center.x, 0.6f, addon.player.center.z);
                addon.player.height = 1.2f;
                addon.isSliding = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                addon.GetUp();
                
            }
        }
    }
}
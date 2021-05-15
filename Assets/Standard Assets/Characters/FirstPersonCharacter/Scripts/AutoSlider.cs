using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.FirstPerson
{
    public class AutoSlider : MonoBehaviour
    {
        private ControllerAddon addon;
        [SerializeField] float impulse=0.2f;
        public bool isIn;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (isIn)
            {
                addon.player.Move(transform.forward* impulse);
                Debug.Log(transform.forward);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (addon==null)
                    addon= other.GetComponent<ControllerAddon>();
                isIn = true;
               /* addon.walkSpeed += 2;
                addon.fpControler.m_WalkSpeed = addon.walkSpeed;
                addon.fpControler.m_RunSpeed += 2;*/
                addon.player.center = new Vector3(addon.player.center.x, 0.6f, addon.player.center.z);
                addon.player.height = 1.2f;
                addon.isAutoSliding = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
              /*  addon.walkSpeed -= 2;
                addon.fpControler.m_WalkSpeed = addon.walkSpeed;
                addon.fpControler.m_RunSpeed -= 2;
                */addon.player.center = addon.center;
                addon.slidetime = 0;
                addon.player.height = addon.height;
                addon.isAutoSliding = false;
                addon.headBlock = false;
                isIn = false;
                addon.player.enabled = true;
            }
        }
    }
}
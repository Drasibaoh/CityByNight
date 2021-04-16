using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Wall : MonoBehaviour
    {
        float x;
        float z;
        public ControllerAddon actor;
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
            actor = other.GetComponent<ControllerAddon>();
            actor.fpControler.m_GravityMultiplier=0;
            x = actor.rigibody.velocity.x;
            z = actor.rigibody.velocity.z;
            
            actor.rigibody.velocity = Vector3.zero;
            Invoke("GetOnGround", 0.5f);
        }
        public void GetOnGround()
        {
            actor.fpControler.m_GravityMultiplier = 2;

        }
        private void OnTriggerExit(Collider other)
        {
            if (IsInvoking())
            {
                CancelInvoke();
            }

            actor.fpControler.m_GravityMultiplier = 2;
        }
    }
}

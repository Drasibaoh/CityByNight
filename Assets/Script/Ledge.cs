using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Ledge : MonoBehaviour
    {
        public Transform standPoint;
        public float imp=1;
        bool canReach;
        bool impulsing;
        ControllerAddon player;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (canReach)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    impulsing = true;
                if (impulsing == true)
                player.fpControler.m_CharacterController.Move(Vector3.up * imp);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (player==null)
                    player = other.GetComponent<ControllerAddon>();
                Debug.Log("tagged");
                //play anim
                canReach = true;
                if (player.fpControler.m_MoveDir.y>=0)
                    impulsing=true;
                Debug.Log("pushed");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            impulsing = false;
            canReach = false;
        }
    }
}
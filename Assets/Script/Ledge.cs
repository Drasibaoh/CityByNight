using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Ledge : MonoBehaviour
    {
        public Transform standPoint;
        public float imp=1;
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
                Debug.Log("tagged");
                //play anim
                other.gameObject.GetComponent<Waklker>().e.m_CharacterController.Move(Vector3.up*imp);
                Debug.Log("pushed");
            }
        }
    }
}
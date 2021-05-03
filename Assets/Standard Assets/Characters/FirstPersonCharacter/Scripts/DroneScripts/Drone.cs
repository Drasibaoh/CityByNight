using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    
    public class Drone : MonoBehaviour
    {
        private PoliceLight pLight;
        public List<Transform> goTo;
        public int steps;
        void Start()
        {
            pLight = GetComponentInChildren<PoliceLight>();
        }

        void Update()
        {

        }

        public void OnReset()
        {

        }
    }
}
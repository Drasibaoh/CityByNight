using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    
    public class Drone : MonoBehaviour
    {
        private PoliceLight pLight;
        Transform goTo;
        public int steps;
        private AIAgent agent;
        void Start()
        {
            goTo= this.transform;

            pLight = GetComponentInChildren<PoliceLight>();
            agent = GetComponent<AIAgent>();
            GameManager.instance.restart.AddListener(OnReset);
        }

        void Update()
        {

        }

        public void OnReset()
        {
            //Debug.Log("eee");
            this.transform.position = goTo.transform.position;
            this.transform.rotation =new Quaternion(0,0,0,0);
            if (agent != null)
            {
                
                agent.Return();
            }
            

            
        }
    }
}
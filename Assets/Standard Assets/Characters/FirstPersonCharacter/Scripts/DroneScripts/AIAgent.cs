using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class AIAgent : MonoBehaviour
    {
        public PoliceLight droneLight;
        public Transform[] path;
        public float minDistBeforePathChange = 0.5f;
        public Transform target;
        private Transform orPos;
        [HideInInspector] public Vector3 targetLastPos;

        public AIAgent save;
        private FSM m_fsm = null;
        public Animation moving;

        public NavMeshAgent navAgent { get; private set; }


        // Start is called before the first frame update
        void Start()
        {
            this.navAgent = this.gameObject.GetComponent<NavMeshAgent>();

            this.m_fsm = this.gameObject.GetComponent<FSM>();
            this.m_fsm.agent = this;

            this.m_fsm.AddState(new AIStatePatrol());
            this.m_fsm.AddState(new AIStateOnLoseSight());

            this.m_fsm.ChangeState(StateNames.AI_PATROL);
            orPos = transform;
        }

        // Update is called once per frame
        void Update()
        {
            // if(Input.GetMouseButtonDown(0)) {
            //     Vector3 pos = Input.mousePosition;
            //     RaycastHit hit;
            //     Ray ray = Camera.main.ScreenPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
            //     Physics.Raycast(ray, out hit);

            //     this.m_navAgent.destination = hit.point;
            // }

            if (this.navAgent.hasPath)
            {
                // debug visual
                NavMeshPath path = this.navAgent.path;
                Vector3[] corners = path.corners;

                int len = corners.Length;
                for (int i = 0; i < len; i++)
                {
                    corners[i].y += 1.0f;
                }

            }
        } 
        public void Return()
        {
            m_fsm.ChangeState(StateNames.AI_PATROL);
        }

    }

}
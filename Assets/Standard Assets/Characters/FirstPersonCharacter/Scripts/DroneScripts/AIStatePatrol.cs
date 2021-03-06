using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class AIStatePatrol : State
    {

        public AIStatePatrol() : base(StateNames.AI_PATROL)
        {

        }

        public override void Begin()
        {
            if (fsm.prev != null)
            {
                m_pathIndex = 0;
            }
            else
            {
            // feedback to show that we're attacking
            int closestPathIndex = -1;
            float closestDist = float.MaxValue;
            fsm.agent.moving.Play("Forward Start");
            fsm.agent.moving.PlayQueued("Forward Idle");
            // find the closest point on our path
            int len = this.fsm.agent.path.Length;
            for (int i = 0; i < len; i++)
            {
                Transform point = this.fsm.agent.path[i];
                float dist = Vector3.Distance(this.fsm.agent.transform.position, point.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestPathIndex = i;
                }
            }
            this.m_pathIndex = closestPathIndex;
            }


            // start moving to the closest point

            this.fsm.agent.navAgent.destination = this.fsm.agent.path[this.m_pathIndex].position;
        }

        public override void Update()
        {

            if (this.fsm.agent.navAgent.hasPath)
            {
                // if we're close to our point, get the next one
                Vector3 toDestination = this.fsm.agent.navAgent.destination - this.fsm.agent.transform.position;
                toDestination.y = 0.0f;
                if (toDestination.magnitude < this.fsm.agent.minDistBeforePathChange)
                {

                    // this.m_pathIndex = Random.Range(0, this.path.Length); // random point
                    this.m_pathIndex = (this.m_pathIndex + 1) % this.fsm.agent.path.Length; // next point

                    this.fsm.agent.navAgent.destination = this.fsm.agent.path[this.m_pathIndex].position;
                }
            }
            if (this.fsm.agent.droneLight.noMove)
            {
             //   this.fsm.ChangeState(StateNames.AI_ON_LOSE_SIGHT);
            }

            // check if we can see the player

        }




    }
}
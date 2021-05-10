using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.FirstPerson
{
    public class AIStateOnLoseSight : State
    {
        private float timer = 0;
        public AIStateOnLoseSight() : base(StateNames.AI_ON_LOSE_SIGHT)
        {

        }

        public override void Begin()
        {
            this.fsm.agent.navAgent.destination = this.fsm.agent.targetLastPos;
            Debug.Log("lost");
        }

        public override void Update()
        {
            if (fsm.agent.droneLight.noMove)
            {
            timer += Time.deltaTime;
            }
            // check if we've arrived at our destination, then look around

            if (timer >= 2)
            {
                timer = 0;
                this.fsm.ChangeState(StateNames.AI_PATROL);
            }

        }

    }
}

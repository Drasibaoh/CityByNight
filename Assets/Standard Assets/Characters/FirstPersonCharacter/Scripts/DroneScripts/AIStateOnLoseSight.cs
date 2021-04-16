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
        }

        public override void Update()
        {
            // check if we've arrived at our destination, then look around
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                timer = 0;
                this.fsm.ChangeState(StateNames.AI_PATROL);
            }

        }

    }
}

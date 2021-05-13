using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class State
    {
        public FSM fsm = null;
        public int m_pathIndex = -1;
        public string name { get; private set; }

        public State(string name)
        {
            this.name = name;
        }

        public virtual void Begin()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void End()
        {

        }

    }
}
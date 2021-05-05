using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class FSM : MonoBehaviour
    {
        private Dictionary<string, State> m_states = new Dictionary<string, State>();

        [HideInInspector] public AIAgent agent = null;

        public State curr { get; private set; }
        public State prev { get; private set; }

        public void AddState(State state)
        {
            state.fsm = this;
            this.m_states[state.name] = state;
        }

        public void ChangeState(string nextStateName)
        {
            State state = null;
            this.m_states.TryGetValue(nextStateName, out state);
            if (state == null)
            {
                Debug.LogError($"[FSM] We don't have a state with the name {nextStateName}");
                return;
            }



            if (this.curr != null)
            {
                this.curr.End();
            }
            this.prev = curr;
            this.curr = state;
            this.curr.Begin();
            Debug.Log($"[FSM] Started state {this.curr.name}");
        }

        void Update()
        {
            if (this.curr != null)
            {
                this.curr.Update();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aho
{
    public class State
    {

        /** effective the size of the keyword */
        private  int depth;

        /** only used for the root state to refer to itself in case no matches have been found */
        private  State rootState;

    /**
     * referred to in the white paper as the 'goto' structure. From a state it is possible to go
     * to other states, depending on the character passed.
     */
    private Dictionary<char, State> success = new Dictionary<char, State>();

        /** if no matching states are found, the failure state will be returned */
        private State failure = null;

        /** whenever this state is reached, it will emit the matches keywords for future reference */
        private HashSet<string> emits = null;

        public State():this(0)
        {
            
        }

        public State(int depth)
        {
            this.depth = depth;
            this.rootState = depth == 0 ? this : null;
        }

        private State nextState(char character, bool ignoreRootState)
        {

            State nextState = this.success.TryGetValue(character, out nextState) ? nextState : null;
            if (!ignoreRootState && nextState == null && this.rootState != null)
            {
                nextState = this.rootState;
            }
            return nextState;
        }

        public State nextState(char character)
        {
            return nextState(character, false);
        }

        public State nextStateIgnoreRootState(char character)
        {
            return nextState(character, true);
        }

        public State addState(char character)
        {
            State nextState = nextStateIgnoreRootState(character);
            if (nextState == null)
            {
                nextState = new State(this.depth + 1);
                this.success.Add(character, nextState);
            }
            return nextState;
        }

        public int getDepth()
        {
            return this.depth;
        }

        public void addEmit(string keyword)
        {
            if (this.emits == null)
            {
                this.emits = new HashSet<string>();
            }
            this.emits.Add(keyword);
        }
        public void addEmit(HashSet<string> emits)
        {
            foreach (string emit in emits)
            {
                addEmit(emit);
            }
        }

        public void addEmit(Collection<string> emits)
        {
            foreach (string emit in emits)
            {
                addEmit(emit);
            }
        }

        public HashSet<string> emit()
        {
            return (this.emits == null) ? new HashSet<string>() : this.emits;
        }

        public State getFailure()
        {
            return this.failure;
        }

        public void setFailure(State failState)
        {
            this.failure = failState;
        }

        public HashSet<State> getStates()
        {
            return new HashSet<State>(this.success.Values);
        }

        public HashSet<char> getTransitions()
        {
            return new HashSet<char>(this.success.Keys);
        }

    }

}


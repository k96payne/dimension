namespace StateConfig
{
    public class StateMachine<T>
    {
        public State<T> currentState { get; private set; }
        public T Agent;

        public StateMachine(T agent)
        {
            Agent = agent;
            currentState = null;
        }

        public void ChangeState(State<T> newState)
        {
            if(currentState != null)
            {
                currentState.ExitState(Agent);
            }
            currentState = newState;
            currentState.EnterState(Agent);
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState(Agent);
            }
        }
    }

    public abstract class State<T>
    {
        public abstract void EnterState(T agent);
        public abstract void ExitState(T agent);
        public abstract void UpdateState(T agent);
    }
}

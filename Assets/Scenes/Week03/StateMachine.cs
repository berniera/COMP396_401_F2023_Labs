using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    //This is similar to the onCollisionEnter ... but for states
    //This is for the system itself
    public class State {
        public string Name;
        public Action onFrame; //DEFAULT ACTION (State Handler when the state is active)
        public Action onEnter; //What happens when this state is entered
        public Action onExit; //What happens when this state is exited

        public override string ToString() { return Name; }
    }

    Dictionary<string, State> states = new Dictionary<string, State>();

    public State currentState { get; private set; }

    public State initialState;

    //Using the factory pattern; therefore, we are not using an explicit constructor, only the default
    public State CreateState(string name) { 
        State state = new State();
        state.Name = name;

        if (states.Count == 0) 
            initialState = state;       

        states[name] = state;
        
        return state;        
    }


    public void Update() {

        // No states yet
        if (states.Count == 0) {
            Debug.LogError("*** State machine has no states! ***");
            return;
        }

        //No current state yet
        if(currentState == null) 
            ChangeState(initialState);
        

        if (currentState.onFrame != null) 
            currentState.onFrame();        
    }

    public void ChangeState(State newState)
    {
        if (newState == null) {
            Debug.LogError("*** Cannot change to a null state ***");
            return;
        }

        //Do onExit of current state
        if (currentState != null && currentState.onExit != null) 
            currentState.onExit();        

        //Change to newState
        Debug.LogFormat("Changing from state {0} to {1}", currentState, newState);
        currentState = newState;

        //Do onEnter to the newState
        if(currentState.onEnter != null) 
             currentState.onEnter();           
    }

    public void ChangeState(string newStateName) { 
        if(!states.ContainsKey(newStateName)) {
            Debug.LogErrorFormat("*** State machine does not have the state {0} ***", newStateName);
            return;
        }

        ChangeState(states[newStateName]);
    }
}
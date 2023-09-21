using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;

    public void Initialise()
    {
        patrolState = new PatrolState();
        ChangeState(patrolState);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check if activeState != null
        if(activeState != null)
        {
            //run cleanup on activeState
            activeState.Exit();
        }
        //change to new state
        activeState = newState;

        //fail-safe nell check to make sure new state isn't null
        if(activeState != null)
        {
            //Setup new state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }

    }
}

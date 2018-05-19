using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public List<BaseState> m_states = new List<BaseState>();
    private BaseState m_currentState = null;
    public StateIDs.States m_initStateID = StateIDs.States.SID_Count;

	// Use this for initialization
	void Start () {
        Agent agent = GetComponent<Agent>();
        Debug.Assert(agent != null, "AGENT IS NULL, NEED AGENT for State machine");
        foreach (BaseState state in m_states)
        {
            state.agent = agent;
        }

        ChangeState(m_initStateID);
        Debug.Log("ChangeState");
    }

    BaseState FindState(StateIDs.States stateID)
    {

        //Check for state ID compatibility
        foreach (BaseState state in m_states)
        {
            if (state.GetName() == stateID)
            {
                return state;
            }
        }
        return null;
    }

	// Update is called once per frame
	void Update () {
		if (m_currentState != null)
        {
            m_currentState.OnUpdate(Time.deltaTime);
        }
	}
    
    void ChangeState(StateIDs.States newStateID)
    {
        if (m_currentState != null)
        {
            m_currentState.OnExit();
        }

        m_currentState = FindState(newStateID);
        Debug.Assert(m_currentState != null, "INVALID INITIAL STATAE");
        m_currentState.OnEnter();
    }
    
}

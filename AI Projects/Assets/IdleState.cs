using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState {
    public IdleState() { }

    private AIAgentController m_agentController = null;
    public override void OnEnter()
    {
        agent.Mood = Agent.EMood.Neutral;
        m_agentController = agent.GetComponent<AIAgentController>();
        Debug.Assert(m_agentController != null, "MISSING AGENT CONTROLLER");
    }
    public override void OnUpdate(float deltaTime)
    {
        //Logics
    }

    public override void OnExit()
    {
        m_agentController.StopMoving();
    }
    public override StateIDs.States GetName()
    {
        return StateIDs.States.SID_Idle;
    }

}

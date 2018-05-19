using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
    public FleeState() { }

    private AIAgentController m_agentController = null;
    public override void OnEnter()
    {
        agent.Mood = Agent.EMood.Happy;
        m_agentController = agent.GetComponent<AIAgentController>();
        Debug.Assert(m_agentController != null, "MISSING AGENT CONTROLLER");
    }
    public override void OnUpdate(float deltaTime)
    {
        m_agentController.RunFromTarget();
        m_agentController.TurnFromTarget();
    }

    public override void OnExit()
    {
    }
    public override StateIDs.States GetName()
    {
        return StateIDs.States.SID_Happy;
    }

}

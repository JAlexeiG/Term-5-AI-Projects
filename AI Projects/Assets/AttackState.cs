using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public AttackState() { }

    private AIAgentController m_agentController = null;
    public override void OnEnter()
    {
        agent.Mood = Agent.EMood.Angry;
        m_agentController = agent.GetComponent<AIAgentController>();
        Debug.Assert(m_agentController != null, "MISSING AGENT CONTROLLER");
    }
    public override void OnUpdate(float deltaTime)
    {
        m_agentController.TurnTowardsTarget();
        m_agentController.MoveTowardsTarget();
    }

    public override void OnExit()
    {
    }
    public override StateIDs.States GetName()
    {
        return StateIDs.States.SID_Angry;
    }
    
}

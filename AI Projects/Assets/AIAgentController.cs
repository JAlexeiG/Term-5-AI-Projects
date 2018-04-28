using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgentController : MonoBehaviour {

    private Agent m_agent = null;

    public Transform m_chaseTarget = null;

    public float m_visionFOV = 45.0f;
    public float m_closeDistance = 2.0f;

    private Agent.EMood m_mood;
    // Use this for initialization
    void Start()
    {
        m_agent = GetComponent<Agent>();
        m_mood = Agent.EMood.Neutral;
    }


	//void CheckForThreats()
 //   {
 //       Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, m_threatRadius, LayerMask.GetMask("Player"));
 //       if (hitColliders.Length > 0)
 //       {
 //           m_state = AIAgentState.AS_Threatened;
 //           UpdateColor();
 //           Vector3 threatToAgent = this.transform.position - hitColliders[0].transform.position;
 //           threatToAgent.Normalize();
 //           m_escapeDirection = threatToAgent;
 //       }
 //       else
 //       {
 //           m_state = AIAgentState.AS_Neutral;
 //           UpdateColor();
 //           m_escapeDirection = Vector3.zero;
 //       }
 //   }

    //implement chase behaviour
    public void TurnTowardsTarget()
    {
        //find out if we need to turn towards target
        Vector3 toTarget = m_chaseTarget.position - transform.position;
        toTarget.Normalize();
        float angleDegrees = Vector3.Angle(transform.forward, toTarget);
        float rightDotResult = (angleDegrees > m_visionFOV) ? Vector3.Dot(transform.right, toTarget) : 0.0f;
        m_agent.Turn(rightDotResult);

    }

    public void MoveTowardsTarget()
    {
        float distance = Mathf.Clamp(Vector3.Distance(m_chaseTarget.position, transform.position), m_closeDistance, 100.0f);
        float speed = 1.0f - (m_closeDistance / distance);
        m_agent.Move(speed);
    }

    public void RunFromTarget()
    {
        float distance = Mathf.Clamp(Vector3.Distance(transform.position, m_chaseTarget.position), m_closeDistance, 100.0f);
        float speed = 1.0f;
        if (distance >= 10)
        {
            speed = 0;
        }
        m_agent.Move(speed);
    }
    public void TurnFromTarget()
    {
        //find out if we need to turn towards target
        Vector3 toTarget = transform.position - m_chaseTarget.position;
        toTarget.Normalize();
        float angleDegrees = Vector3.Angle(transform.forward, toTarget);
        float rightDotResult = (angleDegrees > m_visionFOV) ? Vector3.Dot(transform.right, toTarget) : 0.0f;
        m_agent.Turn(rightDotResult);

    }

    public void StopMoving()
    {
        m_agent.Move(0);
        m_agent.Turn(0);
    }
    // Update is called once per frame
    void Update()
    {

        if (m_agent == null)
        {
            return;
        }
        if (m_mood == Agent.EMood.Angry)
        {
            TurnTowardsTarget();
            MoveTowardsTarget();
        }
        else if (m_mood == Agent.EMood.Happy)
        {
            RunFromTarget();
            TurnFromTarget();
        }
        else
        {
            StopMoving();
        }
    }
    public void MoodSwitch()
    {
        if (m_mood == Agent.EMood.Happy)
        {
            m_mood = Agent.EMood.Neutral;
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (m_mood == Agent.EMood.Neutral)
        {
            m_mood = Agent.EMood.Angry;
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (m_mood == Agent.EMood.Angry)
        {
            m_mood = Agent.EMood.Happy;
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}

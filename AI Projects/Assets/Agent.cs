using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    private static Color[] colors = { Color.green, Color.red };
    static int BLUE = 0;
    static int RED = 1;
    static int GREEN = 2;
    static int YELLOW = 3;

    public enum EMood
    {
        Neutral = 0,
        Angry,
        Happy,
        Scared,
        Count,
    };

    private EMood m_mood = EMood.Neutral;
    public EMood Mood { set; get; }

    //meters/second
    public float MaxLinearSpeed = 5.0f;
    private float m_linearSpeed = 0.0f;
    //meters/second
    public float MaxStrafeSpeed = 3.0f;
    private float m_strafeSpeed = 0.0f;
    //radians/second
    public float MaxAngularSpeed = 2.0f;
    private float m_angularSpeed = 0.0f;

    private Rigidbody m_rb = null;

    void UpdateColor()
    {
        int colorIndex = (m_mood == EMood.Angry) ? RED : GREEN;
        GetComponent<Renderer>().material.color = colors[colorIndex];
    }

    public void Move(float speedRatio)
    {
        m_linearSpeed = Mathf.Clamp(speedRatio, -1.0f, 1.0f) * MaxLinearSpeed;
    }

    public void Strafe(float speedRatio)
    {
        m_strafeSpeed = Mathf.Clamp(speedRatio, -1.0f, 1.0f) * MaxStrafeSpeed;
    }


    public void Turn(float speedRatio)
    {
        m_angularSpeed = Mathf.Clamp(speedRatio, -1.0f, 1.0f) * MaxAngularSpeed;
    }

    public void StopMoving()
    {
        m_linearSpeed = 0.0f;
    }

    public void StopStrafing()
    {
        m_strafeSpeed = 0.0f;
    }

    public void StopTurning()
    {
        m_angularSpeed = 0.0f;
    }

    // Use this for initialization
    void Start () {
        m_rb = GetComponent<Rigidbody>();
        UpdateColor();
    }
	

	// Update is called once per frame
	void Update () {
        Vector3 upVelocity = m_rb.velocity.y * transform.up;
        Vector3 linearVelocity = transform.forward * m_linearSpeed;
        linearVelocity.y = 0.0f;
        Vector3 strafeVelocity = transform.right * m_strafeSpeed;
        Vector3 angularVelocity = transform.up * m_angularSpeed;

        m_rb.velocity = linearVelocity + strafeVelocity + upVelocity;
        m_rb.angularVelocity = angularVelocity;
    }
}

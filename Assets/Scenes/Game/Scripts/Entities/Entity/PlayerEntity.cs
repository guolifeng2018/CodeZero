using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MoveEntity
{
    protected Vector2 m_turn;
    protected bool m_accelerate;
    protected bool m_fire;
    protected float m_recordTime;

    public override void InitEntity()
    {
        base.InitEntity();
        m_angle = 0f;
        GameInputController.Instance.RegisterAction(SetTurnState, SetAccelerateState, SetFireState);
    }

    public override void DestroyEntity()
    {
        base.DestroyEntity();
    }

    public void SetTurnState(Vector2 turnDir)
    {
        m_turn = turnDir;
    }

    public void SetAccelerateState(bool accelerate)
    {
        m_accelerate = accelerate;
        m_recordTime = 0f;
    }

    public void SetFireState(bool fire)
    {
        m_fire = fire;
    }

    protected float GetSpeed()
    {
        return m_accelerate ? m_accelerateSpeed : m_moveSpeed;
    }

    private void Update()
    {
        m_velocity += Forward() * GetSpeed() * Time.deltaTime;
        m_velocity = Vector3.ClampMagnitude(m_velocity, m_maxSpeed);
        m_rigidBody.velocity = m_velocity;

        m_angle -= m_turn.normalized.x * m_turnSpeed * Time.deltaTime;
        m_rigidBody.MoveRotation(m_angle);
    }
}

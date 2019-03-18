using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MoveEntity
{
    [SerializeField] private GameObject m_cameraFollowNode;
    
    protected Vector2 m_turn;
    protected bool m_accelerate;
    protected bool m_fire;
    protected float m_recordTime;
    protected Vector2 m_beginVelocity;
    protected float m_t;
    
    public Action<Vector2> m_updatePlayerPositionAction;

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
        m_beginVelocity = m_rigidBody.velocity;
        m_t = 0f;
    }

    public void SetFireState(bool fire)
    {
        m_fire = fire;
        if (m_bulletLauncher != null)
        {
            m_bulletLauncher.Fire(fire);
        }
    }

    protected float GetSpeed()
    {
        return m_accelerate ? m_accelerateSpeed : m_moveSpeed;
    }

    protected float GetMaxSpeed()
    {
        return m_accelerate ? m_maxAccelerateSpeed : m_maxIdleSpeed;
    }

    private void Update()
    {
        m_velocity += Forward() * GetSpeed() * Time.deltaTime;
        m_velocity = Vector3.ClampMagnitude(m_velocity, GetMaxSpeed());
        m_t += Time.deltaTime * m_lerpSpeed;
        m_t = Mathf.Clamp01(m_t);
        
        m_rigidBody.velocity = Vector2.Lerp(m_beginVelocity, m_velocity, m_t);
        
        m_angle -= m_turn.normalized.x * m_turnSpeed * Time.deltaTime;
        m_rigidBody.MoveRotation(m_angle);

        if (m_updatePlayerPositionAction != null)
        {
            m_updatePlayerPositionAction(transform.position);
        }
    }
}

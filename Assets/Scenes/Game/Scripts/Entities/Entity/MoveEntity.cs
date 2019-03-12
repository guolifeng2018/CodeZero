using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveEntity : Entity
{
	protected Rigidbody2D m_rigidBody;
	protected BoxCollider2D m_collider;
	protected float m_angle;
	protected Vector2 m_velocity;

	[Range(0f, 360f)] public float m_turnSpeed;
	[Range(0f, 100f)] public float m_moveSpeed;
	[Range(0f, 100f)] public float m_accelerateSpeed;
	[Range(0f, 100f)] public float m_maxIdleSpeed;
	[Range(0f, 100f)] public float m_maxAccelerateSpeed;
	[Range(0f, 5f)] public float m_lerpSpeed;
	
	public override void InitEntity()
	{
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_collider = GetComponent<BoxCollider2D>();
		
		m_rigidBody.mass = m_mass;
	}

	public override void DestroyEntity()
	{
		GameObject.Destroy(m_rigidBody);
		m_rigidBody = null;

		Destroy(m_collider);
		m_collider = null;
	}

	protected Vector2 LocalForward()
	{
		return (transform.worldToLocalMatrix * transform.forward).normalized;
	}
	
	protected Vector2 Forward()
	{
		return transform.up.normalized;
	}
	
}

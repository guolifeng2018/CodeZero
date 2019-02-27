using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveEntity : Entity
{
	protected Rigidbody2D m_rigidBody;
	protected BoxCollider2D m_collider;
	
	public MoveEntity(int id, EntityType entityType) 
		: base(id, entityType)
	{
	}

	public MoveEntity(int id, int mass, EntityType entityType) 
		: base(id, mass, entityType)
	{
	}

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
}

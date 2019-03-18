using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class OnceDammageBullet : BulletBase
{
	[SerializeField] private float m_bulletSpeed;
	[SerializeField] private float m_bulletLifeTime;

	private bool m_enable;
	private float m_flyTime;
	private Vector3 m_dir;

	public override void BulletOnInit(Action<BulletBase> onBulletDisable, Action<BulletBase> onBulletEnable)
	{
		base.BulletOnInit(onBulletDisable, onBulletEnable);
	}

	public override void BulletOnDestory()
	{
		base.BulletOnDestory();
	}

	public override void BulletOnEnable()
	{
		base.BulletOnEnable();
		m_enable = true;
		m_flyTime = 0f;

		if (OnBulletEnable != null)
		{
			OnBulletEnable(this);
		}
	}

	public override void BulletOnDisable()
	{
		base.BulletOnDisable();
		m_enable = false;
		
		if (OnBulletDisable != null)
		{
			OnBulletDisable(this);
		}
	}

	public override void BulletOnUpdate()
	{
		base.BulletOnUpdate();

		if (m_enable)
		{
			if (m_flyTime > m_bulletLifeTime)
			{
				BulletOnDisable();
			}
			else
			{
				transform.position += m_dir.normalized * m_bulletSpeed * Time.deltaTime;
			}
		}
		m_flyTime += Time.deltaTime;
	}

	public override void Fly(Vector3 dir, Quaternion rotation)
	{
		m_dir = dir;
		transform.rotation = rotation;
		BulletOnEnable();
	}

	public override void TakeDamage()
	{
		throw new System.NotImplementedException();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		//BulletOnDisable();
	}
}

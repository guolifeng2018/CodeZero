using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBase
{
	public abstract void Init();

	public virtual void Update(){}
	
	protected virtual void AddListener(){}
	protected virtual void RemoveListener(){}
}

public abstract class Singleton<T> : SingletonBase
	where T : SingletonBase, new()
{
	private static T m_instance;

	public static T GetInstance()
	{
		if (m_instance == null)
		{
			m_instance = new T();
			m_instance.Init();
		}

		return m_instance;
	}
}

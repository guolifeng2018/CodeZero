using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunc
{

	public static bool CheckParentIs(GameObject go, string name)
	{
		if (go == null)
		{
			return false;
		}

		if (go.name == name)
		{
			return true;
		}
		
		if (go.transform.parent != null)
		{
			return CheckParentIs(go.transform.parent.gameObject, name);
		}

		return false;
	}

	public static GameObject GetChild(GameObject go, string name, bool includeSelf = false)
	{
		if (go == null || string.IsNullOrEmpty(name))
		{
			return go;
		}

		if (includeSelf && go.name == name)
		{
			return go;
		}

		// cache component
		Transform rootTransform = go.transform;
		Transform tranChild = null;
		if (name.IndexOf('/') != -1)
		{
			tranChild = rootTransform.Find(name);
			if (tranChild != null)
			{
				return tranChild.gameObject;
			}
			return null;
		}
        

		tranChild = rootTransform.Find(name);
		if (tranChild != null)
		{
			return tranChild.gameObject;
		}

		// 函数的这部分怎么还在？ 
		for (int i = 0; i < rootTransform.childCount; i++)
		{
			GameObject goChild = rootTransform.GetChild(i).gameObject;
			//if (goChild.name == name)
			//    return goChild;

			goChild = GetChild(goChild, name);
			if (goChild != null)
			{
				return goChild;
			}
		}
		return null;
	}

	public static T AddSingleComponent<T>(GameObject go)
		where T : Component
	{
		T component = go.GetComponent<T>();
		if (component == null)
		{
			component = go.AddComponent<T>();
		}

		return component;
	}
}

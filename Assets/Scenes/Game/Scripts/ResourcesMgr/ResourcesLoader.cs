using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourcesLoader : MonoBehaviour, ILoader
{
	private EResLoadingState m_loadState = EResLoadingState.Idle;
	private EResourcesType m_resourcesType;
	private string m_resName;

	private Coroutine m_coroutine;

	private static WaitForEndOfFrame m_waitForEndOfFrame = new WaitForEndOfFrame();

	private void Start()
	{
	}

	public void PrepareLoad(EResourcesType resourcesType, string resName)
	{
		m_resourcesType = resourcesType;
		m_resName = resName;
		m_loadState = EResLoadingState.WaitForLoad;
	}

	public ResourceInfo StartLoad()
	{
		m_loadState = EResLoadingState.WaitForLoad;
		string path = GetResPath();
		Object res = Resources.Load(path);
		m_loadState = EResLoadingState.Idle;
		ResourceInfo resInfo = new ResourceInfo(res, m_resName, m_resourcesType);
		return resInfo;
	}

	public void StartLoadAsync(Action<ResourceInfo> callBack)
	{
		m_loadState = EResLoadingState.Loading;
		m_coroutine = StartCoroutine(Loading(callBack));
	}

	public IEnumerator Loading(Action<ResourceInfo> callBack)
	{
		string path = GetResPath();
		ResourceRequest request = Resources.LoadAsync(path);
		while (!request.isDone)
		{
			yield return m_waitForEndOfFrame;
		}

		if (callBack != null)
		{
			ResourceInfo resInfo = new ResourceInfo(request.asset, m_resName, m_resourcesType);
			callBack(resInfo);
		}
		
		EndLoad();
	}

	public void CancelLoad()
	{
		if (m_coroutine != null)
		{
			StopCoroutine(m_coroutine);
		}
		EndLoad();
	}

	void EndLoad()
	{
		m_loadState = EResLoadingState.Idle;
	}

	public bool IsLoading()
	{
		return m_loadState != EResLoadingState.Idle;
	}

	public string GetResPath()
	{
		string path = ResourcesConst.GetResourcesPath(m_resourcesType, m_resName);
		if (string.IsNullOrEmpty(path))
		{
			Debug.LogError(string.Format("Resource path cannot find!    ResourceType : {0}   |  ResourceName : {1}",
				m_resourcesType, m_resName));
			return null;
		}

		return path;
	}

	public string GetLoadingKey()
	{
		return ResourceInfo.GetResourcesKey(m_resourcesType, m_resName);
	}
}

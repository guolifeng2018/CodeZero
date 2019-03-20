using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoaderMgr
{
    private const int MAX_LOADER_COUNT = 4;

    private ILoader[] m_loaderArr;
    private ILoader m_noneAsyncLoader;

    private Queue<ResLoadTask> m_laodTaskQueue;
    
    public ResourcesLoaderMgr(GameObject obj)
    {
        m_laodTaskQueue = new Queue<ResLoadTask>();
        
        m_loaderArr = new ILoader[MAX_LOADER_COUNT];
        for (int i = 0; i < MAX_LOADER_COUNT; i++)
        {
            m_loaderArr[i] = CreateLoader(obj, i);
        }

        m_noneAsyncLoader = CreateLoader(obj, MAX_LOADER_COUNT);
    }

    private ILoader GetIdleResourcesLoader()
    {
        for (int i = 0; i < m_loaderArr.Length; i++)
        {
            if (!m_loaderArr[i].IsLoading())
            {
                return m_loaderArr[i];
            }
        }

        return null;
    }

    public void Update()
    {
        ILoader loader = GetIdleResourcesLoader();
        if (loader == null){return;}
        
        ResLoadTask task = DequeueLoadTask();
        loader.PrepareLoad(task.m_resType, task.m_name);
        loader.StartLoad();
    }

    public void AppendLoadTask(EResourcesType resType, string resName, Action<ResourceInfo> callBack)
    {
        m_noneAsyncLoader.PrepareLoad(resType, resName);
        ResourceInfo resInfo = m_noneAsyncLoader.StartLoad();
        if (resInfo != null && callBack != null)
        {
            callBack(resInfo);
        }
    }

    public void AppendLoadTaskAsync(EResourcesType resType, string resName, Action<ResourceInfo> callBack)
    {
        ResLoadTask task = new ResLoadTask(resName, resType, ELoadSpeedType.Normal, callBack);
        m_laodTaskQueue.Enqueue(task);
    }

    public void CancelLoadTask(EResourcesType resType, string resName)
    {
        for (int i = 0; i < m_loaderArr.Length; i++)
        {
            if (m_loaderArr[i].GetLoadingKey().Equals(ResourceInfo.GetResourcesKey(resType, resName)))
            {
                m_loaderArr[i].CancelLoad();
            }
        }
    }

    public bool IsLoading(EResourcesType resType, string resName)
    {
        for (int i = 0; i < m_loaderArr.Length; i++)
        {
            if (m_loaderArr[i].GetLoadingKey().Equals(ResourceInfo.GetResourcesKey(resType, resName)) && m_loaderArr[i].IsLoading())
            {
                return true;
            }
        }

        return false;
    }
    
    private ResourcesLoader CreateLoader(GameObject obj, int i)
    {
        GameObject loaderGo = new GameObject(string.Format("Loader{0}", i));
        loaderGo.transform.parent = obj.transform;
        loaderGo.transform.localPosition =Vector3.zero;
        loaderGo.transform.localRotation = Quaternion.identity;
        loaderGo.transform.localScale = Vector3.one;
        ResourcesLoader loader = loaderGo.AddComponent<ResourcesLoader>();
        return loader;
    }

    private ResLoadTask DequeueLoadTask()
    {
        if (m_laodTaskQueue.Count > 0)
        {
            return m_laodTaskQueue.Dequeue();
        }

        return null;
    }
}

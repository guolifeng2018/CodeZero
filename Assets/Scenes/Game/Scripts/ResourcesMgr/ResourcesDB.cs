using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesDB
{
    private Dictionary<string, ResourceInfo> m_resourcesInfoDic;

    public ResourcesDB()
    {
        m_resourcesInfoDic = new Dictionary<string, ResourceInfo>();
    }

    public ResourceInfo GetResourceInfo(EResourcesType resType, string resName)
    {
        string key = ResourceInfo.GetResourcesKey(resType, resName);

        ResourceInfo info = null;
        if (m_resourcesInfoDic.TryGetValue(key, out info))
        {
            return info;
        }

        return null;
    }

    public void AddResource(ResourceInfo resInfo)
    {
        if (resInfo == null)
        {
            return;
        }
        if (m_resourcesInfoDic.ContainsKey(resInfo.GetKey()))
        {
            UnloadResource(m_resourcesInfoDic[resInfo.GetKey()]);
            m_resourcesInfoDic[resInfo.GetKey()] = resInfo;
        }
        else
        {
            m_resourcesInfoDic.Add(resInfo.GetKey(), resInfo);
        }
    }
    
    public void AddResources(List<ResourceInfo> resInfo)
    {
        for (int i = 0; i < resInfo.Count; i++)
        {
            AddResource(resInfo[i]);
        }
    }

    public void UnloadResource(EResourcesType resType, string name)
    {
        string key = ResourceInfo.GetResourcesKey(resType, name);
        UnloadResource(key);
    }

    public void UnloadResource(string key)
    {
        ResourceInfo info = null;
        if (m_resourcesInfoDic.TryGetValue(key, out info))
        {
            info.Unload();
            info = null;
            m_resourcesInfoDic.Remove(key);
        }
    }

    public void UnloadResource(ResourceInfo info)
    {
        if (info != null)
        {
            UnloadResource(info.GetKey());
        }
    }

    public void UnloadResources(List<ResourceInfo> infos)
    {
        for (int i = 0; i < infos.Count; i++)
        {
            UnloadResource(infos[i]);
        }
    }

    public bool IsResourceExist(EResourcesType resType, string name)
    {
        string key = ResourceInfo.GetResourcesKey(resType, name);
        return IsResourceExist(key);
    }

    public bool IsResourceExist(string key)
    {
        return (m_resourcesInfoDic.ContainsKey(key));
    }
}

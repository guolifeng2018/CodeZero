using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInfo
{
    private Object m_asset;
    private string m_name;
    private EResourcesType m_resType;
    private string m_key;

    public ResourceInfo(Object resource, string name, EResourcesType resourcesType)
    {
        m_asset = resource;
        m_name = name;
        m_resType = resourcesType;
        m_key = GetResourcesKey(resourcesType, name);
    }

    public string GetKey()
    {
        return m_key;
    }

    public T GetAsset<T>()
        where T : Object
    {
        return m_asset as T;
    }

    public void Unload()
    {
        Resources.UnloadAsset(m_asset);
        m_name = string.Empty;
        m_resType = EResourcesType.MaxType;
    }

    public static string GetResourcesKey(EResourcesType resType, string resName)
    {
        return string.Format("{0}_{1}", resType, resName);
    }
}

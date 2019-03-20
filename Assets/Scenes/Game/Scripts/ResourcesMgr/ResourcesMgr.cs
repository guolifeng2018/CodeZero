using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourcesMgr : UnitySingleton<ResourcesMgr>
{
    //卸载不用资源
    private float UNLOAD_UNUSEDASSET_TIME = 10f;
    
    private bool m_enableUnload = true;

    private ResourcesDB m_resourcesDB;
    private ResourcesLoaderMgr m_resourceLoader;
    private float m_countTime;
    
    public override void Init()
    {
        m_resourcesDB = new ResourcesDB();
        m_resourceLoader = new ResourcesLoaderMgr(gameObject);
    }

    public void SwitchEnableUnload(bool enable)
    {
        m_enableUnload = enable;
    }
    
    public Object Load(EResourcesType resourceType, string resName)
    {
        string path = ResourcesConst.GetResourcesPath(resourceType, resName);
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError(string.Format("Resource path cannot find!    ResourceType : {0}   |  ResourceName : {1}", resourceType, resName));
            return null;
        }

        Object res = Resources.Load(path);
        return null;
    }
    
    public T Load<T>(EResourcesType resourceType, string resName)
        where T : Object
    {
       
        return Load(resourceType, resName) as T;
    }

    public void LoadResAsync(EResourcesType resourceType, string resName, Action<string, ResourceInfo> callBack)
    {
        
    }

    public void UnLoad(EUnloadTriggerType triggerType)
    {
        if (!m_enableUnload)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (m_countTime >= UNLOAD_UNUSEDASSET_TIME)
        {
            m_countTime = 0f;

            UnLoad(EUnloadTriggerType.Auto);
        }

        m_countTime += Time.fixedDeltaTime;
    }
}

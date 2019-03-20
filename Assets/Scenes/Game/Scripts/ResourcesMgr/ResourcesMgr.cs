using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourcesMgr : UnitySingleton<ResourcesMgr>
{
    //卸载不用资源
    private float UNLOAD_UNUSEDASSET_TIME = 10f;
    
    private bool m_enableUnload = true;
    private bool m_enableUnloadUnusedAsset = true;

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

    public void SwitchEnableUnloadUnusedAssets(bool enable)
    {
        m_enableUnloadUnusedAsset = enable;
    }
    
    public void Load(EResourcesType resourceType, string resName, ELoadSpeedType speedType, Action<ResourceInfo> callBack)
    {
        //检测是否已经加载过了
        ResourceInfo info = null;
        if (m_resourcesDB.IsResourceExist(resourceType, resName))
        {
            info = m_resourcesDB.GetResourceInfo(resourceType, resName);

            if (info != null)
            {
                if(callBack != null)
                {
                    callBack(info);
                }
                return;
            }
        }

        //走资源加载
        switch (speedType)
        {
            case ELoadSpeedType.Immediately:
            {
                m_resourceLoader.AppendLoadTask(resourceType, resName, callBack);
            }
                break;
            case ELoadSpeedType.Normal:
            {
                m_resourceLoader.AppendLoadTaskAsync(resourceType, resName, callBack);
            }
                break;
        }
    }

    public void UnloadAsset(EResourcesType resType, string resName)
    {
        if (m_resourceLoader != null && !m_resourceLoader.IsLoading(resType, resName))
        {
            m_resourcesDB.UnloadResource(resType, resName);
        }
    }

    public void UnLoad(EUnloadTriggerType triggerType)
    {
        if (!m_enableUnload)
        {
            return;
        }

        switch (triggerType)
        {
                case EUnloadTriggerType.Auto:
                    if (m_enableUnloadUnusedAsset)
                    {
                        Resources.UnloadUnusedAssets();
                    }
                    break;
                case EUnloadTriggerType.Assets:
                    Resources.UnloadUnusedAssets();
                    break;
                    
        }
    }

    private void LateUpdate()
    {
        //Loader加载
        if (m_resourceLoader != null)
        {
            m_resourceLoader.Update();
        }
    }

    private void FixedUpdate()
    {
        //Unload
        if (m_countTime >= UNLOAD_UNUSEDASSET_TIME)
        {
            m_countTime = 0f;

            UnLoad(EUnloadTriggerType.Auto);
        }

        m_countTime += Time.fixedDeltaTime;
    }
}

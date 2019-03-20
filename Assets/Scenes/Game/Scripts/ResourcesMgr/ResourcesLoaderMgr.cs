using UnityEngine;

public class ResourcesLoaderMgr
{
    private const int MAX_LOADER_COUNT = 4;

    private ILoader[] m_loaderArr;
    
    public ResourcesLoaderMgr(GameObject obj)
    {
        m_loaderArr = new ILoader[MAX_LOADER_COUNT];
        for (int i = 0; i < MAX_LOADER_COUNT; i++)
        {
            m_loaderArr[i] = CreateLoader(obj, i);
        }
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

    public void AppendLoadTask(EResourcesType resType, string resName)
    {
        ResLoadTask task = new ResLoadTask();
    }

    public void AppendLoadTaskAsync(EResourcesType resType, string resName)
    {
        
    }

    public void CancelLoadTask()
    {
        
    }
}

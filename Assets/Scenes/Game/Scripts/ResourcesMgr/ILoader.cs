using System;
using System.Collections;
using Object = UnityEngine.Object;

public interface ILoader
{
    void PrepareLoad(EResourcesType resourcesType, string resName);
    
    ResourceInfo StartLoad();
    
    void StartLoadAsync(Action<ResourceInfo> callBack);
    IEnumerator Loading(Action<ResourceInfo> callBack);
    void CancelLoad();
    
    bool IsLoading();
    string GetResPath();
    string GetLoadingKey();
}

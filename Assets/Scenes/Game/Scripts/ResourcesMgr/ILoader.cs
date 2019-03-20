using System;
using System.Collections;
using Object = UnityEngine.Object;

public interface ILoader
{
    void PrepareLoad(EResourcesType resourcesType, string resName);
    
    Object StartLoad();
    
    void StartLoadAsync(Action<ResourceInfo> callBack);
    IEnumerator Loading(Action<ResourceInfo> callBack);
    
    bool IsLoading();
    string GetResPath();
}

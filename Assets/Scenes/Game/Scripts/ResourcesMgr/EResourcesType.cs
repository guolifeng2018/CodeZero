using System.Collections.Generic;

public enum EResourcesType
{
    Audio = 0,
    Bullet = 1,
    BulletLauncher = 2,
    FX = 3,
    Plane = 4,
    Textures = 5,
    UI_Prefab = 6,
    UI_Sprite = 7,
    
    MaxType = 8,
}

public enum ELoadSpeedType
{
    Immediately = 0,
    Normal = 1,
}

public enum EUnloadTriggerType
{
    Auto = 0,
    UI = 1,
    Scene = 2,
}

public enum EResLoadingState
{
    Idle = 0,
    WaitForLoad = 1,
    Loading = 1,
	
}

public class ResourcesConst
{
    private static Dictionary<EResourcesType, string> m_resourcesPathDic = new Dictionary<EResourcesType, string>
    {
        {EResourcesType.Audio, "Audio/{0}"},
        {EResourcesType.Bullet, "Bullet/{0}"},
        {EResourcesType.BulletLauncher, "BulletLauncher/{0}"},
        {EResourcesType.FX, "FX/{0}"},
        {EResourcesType.Plane, "Plane/{0}"},
        {EResourcesType.Textures, "Textures/{0}"},
        {EResourcesType.UI_Prefab, "UI/Prefab/{0}"},
        {EResourcesType.UI_Sprite, "UI/Sprite/{0}"},
    };

    public static string GetResourcesPath(EResourcesType resType, string resName)
    {
        string path = string.Empty;
        if (m_resourcesPathDic.TryGetValue(resType, out path))
        {
            return string.Format(path, resName);
        }

        return path;
    }
}
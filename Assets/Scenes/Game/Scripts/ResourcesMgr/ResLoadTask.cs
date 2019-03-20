using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResLoadTask
{
    public string m_name;
    public EResourcesType m_resType;
    public ELoadSpeedType m_loadSpeedType;
    public Action<ResourceInfo> m_callBack;

    public ResLoadTask()
    {
        
    }

    public ResLoadTask(string name, EResourcesType resType, ELoadSpeedType speedType, Action<ResourceInfo> callBack)
    {
        m_resType = resType;
        m_name = name;
        m_loadSpeedType = speedType;
        m_callBack = callBack;
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public abstract class UnitySingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected void Start()
    {
        Init();
    }

    public virtual void Init(){}
} 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] private EBulletDamageType m_dammageType;
    [SerializeField] private float m_dammageValue;

    public virtual void BulletOnInit()
    {

    }

    public virtual void BulletOnDestory()
    {
        
    }

    public virtual void BulletOnUpdate()
    {
        
    }

    public abstract void Fly();
    
}

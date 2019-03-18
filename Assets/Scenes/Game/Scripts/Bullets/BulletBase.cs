using System;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] private EBulletDamageType m_dammageType;
    [SerializeField] private float m_dammageValue;

    private BoxCollider m_collider;

    public Action<BulletBase> OnBulletDisable;
    public Action<BulletBase> OnBulletEnable;

    public virtual void BulletOnInit(Action<BulletBase> onBulletDisable, Action<BulletBase> onBulletEnable)
    {
        m_collider = GetComponent<BoxCollider>();
        OnBulletEnable = onBulletEnable;
        OnBulletDisable = onBulletDisable;
    }

    public virtual void BulletOnEnable()
    {
        
    }

    public virtual void BulletOnDisable()
    {
        
    }

    public virtual void BulletOnDestory()
    {
        
    }

    private void Update()
    {
        BulletOnUpdate();
    }

    public virtual void BulletOnUpdate()
    {
        
    }

    public abstract void Fly(Vector3 dir, Quaternion rotation);

    public abstract void TakeDamage();

}

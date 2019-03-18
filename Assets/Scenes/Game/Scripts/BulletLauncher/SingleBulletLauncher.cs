using System.Collections.Generic;
using UnityEngine;

public class SingleBulletLauncher : BulletLauncherBase
{
    [SerializeField] private BulletBase m_template;
    
    private List<BulletBase> m_activeBullet;
    private List<BulletBase> m_cacheBullet;
    
    protected override void OnLauncherInit()
    {
        base.OnLauncherInit();
        m_activeBullet = new List<BulletBase>();
        m_cacheBullet = new List<BulletBase>();
    }

    protected override void OnLauncherUpdate()
    {
        base.OnLauncherUpdate();
        if (m_fire && IsArriveFireTime())
        {
            PushBullet();
            m_fireTime = 0f;
        }
    }

    protected override void OnLauncherDestory()
    {
        base.OnLauncherDestory();
    }
    
    public override void Fire(bool fire)
    {
        m_fire = fire;
    }

    private void PushBullet()
    {
        BulletBase bullet = GetBullet();
        bullet.gameObject.SetActive(true);
        bullet.Fly(GetFireDir(), m_template.transform.rotation);
    }

    protected override Vector3 GetFireTowards()
    {
        return transform.localToWorldMatrix * transform.forward;
    }

    private Vector3 GetFireDir()
    {
        return (m_forward.transform.position - m_fireNode.transform.position).normalized;
    }

    private BulletBase GetBullet()
    {
        BulletBase bullet = null;
        if (m_cacheBullet.Count > 0)
        {
            bullet = m_cacheBullet[0];
            m_cacheBullet.Remove(bullet);
        }
        else
        {
            bullet = Instantiate(m_template);
            bullet.BulletOnInit(HandleBulletDisable, HandleBulletEnable);
        }

        bullet.transform.position = m_fireNode.transform.position;
        return bullet;
    }

    private void HandleBulletDisable(BulletBase bullet)
    {
        m_activeBullet.Remove(bullet);
        bullet.gameObject.SetActive(false);
        m_cacheBullet.Add(bullet);
    }

    private void HandleBulletEnable(BulletBase bullet)
    {
        m_activeBullet.Add(bullet);
    }
}

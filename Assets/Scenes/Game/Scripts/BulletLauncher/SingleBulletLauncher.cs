using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBulletLauncher : BulletLauncherBase
{
    protected override void OnLauncherInit()
    {
        base.OnLauncherInit();
    }

    protected override void OnLauncherUpdate()
    {
        base.OnLauncherUpdate();
    }

    protected override void OnLauncherDestory()
    {
        base.OnLauncherDestory();
    }
    
    public override void Fire()
    {
    }

    protected override Vector3 GetFireTowards()
    {
        return transform.localToWorldMatrix * transform.forward;
    }
}

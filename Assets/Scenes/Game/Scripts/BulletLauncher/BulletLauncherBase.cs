using UnityEngine;

public abstract class BulletLauncherBase : MonoBehaviour
{
    #region Datas

    [SerializeField] private string m_bulletName = "BulletA";
    [SerializeField] private int m_maxBulletCount = 100;
    [SerializeField] private EBulletLauncherType m_launcherType = EBulletLauncherType.Single;
    [SerializeField] private float m_launcherRate = 0.1f;
    [SerializeField] private bool m_havePowerStorage = false;
    [SerializeField] private float m_storageTime;
    //[SerializeField] private float[] m_storageTimes; //充能时间统一or不统一 maybe。。。
    [SerializeField] private EBulletStorageStage m_maxStorageTime;

    #endregion

    protected EBulletStorageStage m_curStorageStage;
    protected bool m_isStoraging;
    protected float m_fireTime;
    
    public abstract void Fire();

    protected abstract Vector3 GetFireTowards();

    protected bool IsArriveFireTime()
    {
        return m_fireTime >= m_launcherRate;
    }

    private void Start()
    {
        OnLauncherInit();
    }

    private void Update()
    {
        OnLauncherUpdate();
    }

    private void OnDestroy()
    {
        OnLauncherDestory();
    }

    protected virtual void OnLauncherInit()
    {
        m_curStorageStage = EBulletStorageStage.Normal;
        m_isStoraging = false;
        m_fireTime = 0f;
    }

    protected virtual void OnLauncherUpdate()
    {
        m_fireTime += Time.deltaTime;
    }

    protected virtual void OnLauncherDestory()
    {
        
    }
}

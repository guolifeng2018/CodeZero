using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraControl : MonoBehaviour
{
    private const float CAMERA_COORD_Z = -10f;
    
    [Range(0, 100)] public float m_maxShakeAngle;
    [Range(0, 10)] public float m_maxOffset;
    [Range(0, 3)] public float m_traumaDecreaseSpeed = 1f;
    [Range(0, 0.5f)] public float m_allowDistance = 0.2f;
    [SerializeField] public bool m_useRandomSeed;
    [SerializeField] public string m_seed;
    
    [Space(10)]
    [Range(0, 20)] public float m_followSpeed = 1f;

    [Range(0, 100)] public float m_smooth = 1f;
    
    private Camera m_camera;

    #region CameraShake

    private float m_trauma;
    
    #endregion

    #region FollowTarget
    
    private Vector2 m_targetPos;
    private Vector2 m_startPos;
    private bool m_isParamSet = false;

    #endregion

    private void Start()
    {
        m_camera = GetComponent<Camera>();
        m_isParamSet = false;
    }


    public void SetShakeCameraParam(float trauma)
    {
        m_trauma += trauma;
        m_trauma = Mathf.Clamp01(m_trauma);
    }

    public void UpdateCameraTargetPosition(Vector2 position)
    {
        m_targetPos = position;
        if (!m_isParamSet)
        {
            m_startPos = position;
            m_isParamSet = true;
        }
    }

    private void Update()
    {
        //跟随角色
        Vector2 cameraPos = CalculateCameraPosition();
        m_startPos = cameraPos;
        
        //处理摄像机振动
        float angle = m_maxShakeAngle * m_trauma * GetRandomFloatNegOneToOne();
        float offsetX = m_maxOffset * m_trauma * GetRandomFloatNegOneToOne();
        float offsetY = m_maxOffset * m_trauma * GetRandomFloatNegOneToOne();
        cameraPos += new Vector2(offsetX, offsetY);
        UpdateCameraPosition(cameraPos, angle);
        
        m_trauma -= Time.deltaTime * m_traumaDecreaseSpeed;
        m_trauma = Mathf.Clamp01(m_trauma);
    }

    private void UpdateCameraPosition(Vector2 position, float angle)
    {
        Debug.LogError(position);
//        if (Vector2.Distance(m_camera.transform.position, position) >= m_allowDistance)
//        {
//            m_camera.transform.position = new Vector3(position.x, position.y, CAMERA_COORD_Z);
//        }

//        float velocityY = 0f;
//        float velocityX = 0f;
//        float newPosY = Mathf.SmoothDamp(transform.position.y, position.y, ref velocityY, m_smooth);
//        float newPosX = Mathf.SmoothDamp(transform.position.x, position.x, ref velocityX, m_smooth);
//        m_camera.transform.position = new Vector3(newPosX, newPosY, CAMERA_COORD_Z);
        
        Vector3 pos = new Vector3(m_targetPos.x, m_targetPos.y, 0f);
        //if (CheckCameraOutView(pos))
        {
            m_camera.transform.position = new Vector3(position.x, position.y, CAMERA_COORD_Z);
        }
        m_camera.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private Vector2 CalculateCameraPosition()
    {
        Vector2 dir = (m_targetPos - m_startPos);
        Vector2 pos = GetCurCameraPos() + dir * m_followSpeed;

        return pos;
    }

    private Vector2 GetCurCameraPos()
    {
        if (m_camera != null)
        {
            return m_camera.gameObject.transform.position;
        }
        
        return Vector2.zero;
    }

    private float GetRandomFloatNegOneToOne()
    {
        Random.seed = GetSeed();
        return Random.Range(-1f, 1f);
    }

    private int GetSeed()
    {
        int seed = 0;
        if (m_useRandomSeed)
        {
            seed = Time.time.GetHashCode();
        }
        else
        {
            seed = m_seed.GetHashCode();
        }

        return seed;
    }

    private bool CheckCameraOutView(Vector3 position)
    {
        Vector3 pos = m_camera.WorldToViewportPoint(position);
        return (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1);
    }
}

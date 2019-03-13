
using UnityEngine;

public class GameStarter : UnitySingleton<GameStarter>
{
    private GameCameraControl m_cameraControl;
    
    public override void Init()
    {
        base.Init();

        InitCamera();
        InitPlayerEntity();
    }

    private void InitCamera()
    {
        m_cameraControl = GameObject.FindObjectOfType<GameCameraControl>();
    }

    private void InitPlayerEntity()
    {
        PlayerEntity entity = GameObject.FindObjectOfType<PlayerEntity>();
        if (entity != null)
        {
            entity.m_updatePlayerPositionAction = m_cameraControl.UpdateCameraTargetPosition;
            GameEntitiesManager.GetInstance().AddEntity(entity);
        }
    }
}

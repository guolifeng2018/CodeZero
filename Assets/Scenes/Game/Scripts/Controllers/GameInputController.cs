using System;
using UnityEngine;

/// <summary>
/// 上帝视角的飞机游戏，飞机通过调整朝向、加速、开火 来控制飞机移动飞行
/// </summary>
public class GameInputController : UnitySingleton<GameInputController>
{
    private GameControlBase m_curControl;
    
    protected GameControlBase CurControl
    {
        get { return m_curControl; }
    }
    
    private void Start()
    {
        InitCurControl();
    }
    
    public void RegisterAction(Action<Vector2> moveAction, Action<bool> accelerateAction, Action<bool> fireAction)
    {
        InitCurControl();
        
        CurControl.MoveAction = moveAction;
        CurControl.AccelerateAction = accelerateAction;
        CurControl.FireAction = fireAction;
    }

    private void InitCurControl()
    {
        if (m_curControl == null)
        {
            m_curControl = gameObject.AddComponent<GameKeyBoardControl>();
        }
    }
}

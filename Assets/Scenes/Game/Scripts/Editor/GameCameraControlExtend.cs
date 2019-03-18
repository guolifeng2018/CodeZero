using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameCameraControl))]
public class GameCameraControlExtend : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GameCameraControl control = target as GameCameraControl;
        if (control != null && GUILayout.Button("ShakeCamera"))
        {
            control.SetShakeCameraParam(0.5f);
        }
    }
}

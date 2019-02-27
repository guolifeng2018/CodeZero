using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlBase : MonoBehaviour
{
    public Action<Vector2> MoveAction;
    public Action<bool> AccelerateAction;
    public Action<bool> FireAction;
}

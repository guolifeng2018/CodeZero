using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MoveEntity 
{
    public PlayerEntity(int id, EntityType entityType) 
        : base(id, entityType)
    {
    }

    public PlayerEntity(int id, int mass, EntityType entityType) 
        : base(id, mass, entityType)
    {
    }

    public override void InitEntity()
    {
        base.InitEntity();
    }

    public override void DestroyEntity()
    {
        base.DestroyEntity();
    }
}

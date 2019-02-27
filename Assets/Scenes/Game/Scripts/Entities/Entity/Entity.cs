using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Player_Self = 0,        //玩家自己
    Player_TeamMate = 1,    //玩家队友
    Player_TeamEnemy = 2,   //玩家敌人
    AI_TeamMate = 3,        //AI队友
    AI_TeamEnemy = 4,       //AI敌人
    
}

public abstract class Entity : MonoBehaviour
{
    protected static int GlobalID = 0;
    
    protected int m_entityID;

    protected int m_mass;

    protected EntityType m_entityType;
    
    public int EntityID
    {
        get { return m_entityID; }
    }

    public EntityType EntityType
    {
        get { return m_entityType; }
    }

    public Entity(int id, EntityType entityType)
    {
        m_entityID = id;
        m_mass = 1;
        m_entityType = entityType;
    }

    public Entity(int id, int mass, EntityType entityType)
    {
        m_entityID = id;
        m_mass = mass;
        m_entityType = entityType;
    }

    public abstract void InitEntity();

    public abstract void DestroyEntity();
}

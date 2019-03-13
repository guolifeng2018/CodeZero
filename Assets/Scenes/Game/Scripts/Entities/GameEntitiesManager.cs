using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEntitiesManager : Singleton<GameEntitiesManager>
{
    private Dictionary<EntityType, List<Entity>> m_entitiesByEntityType;

    private Dictionary<int, Entity> m_allEntites;
    
    public override void Init()
    {
        m_allEntites = new Dictionary<int, Entity>();

        m_entitiesByEntityType = new Dictionary<EntityType, List<Entity>>();
        for (EntityType i = EntityType.Player_Self; i <= EntityType.AI_TeamEnemy; i++)
        {
            m_entitiesByEntityType[i] = new List<Entity>();
        }
    }

    public void AddEntity(Entity entity)
    {
        if(entity == null)
            return;
        
        if (m_allEntites.ContainsKey(entity.EntityID))
        {
            Debug.LogError(string.Format("Entity is contains  : {0}", entity.EntityID));
            return;
        }

        m_allEntites[entity.EntityID] = entity;
        m_entitiesByEntityType[entity.EntityType].Add(entity);
    }

    public void RemoveEntity(int id)
    {
        Entity entity = GetEntity<Entity>(id);
        RemoveEntity(entity);
    }
    
    public void RemoveEntity(Entity entity)
    {
        if (entity != null)
        {
            m_allEntites.Remove(entity.EntityID);
            m_entitiesByEntityType[entity.EntityType].Remove(entity);
            entity.DestroyEntity();
            GameObject.Destroy(entity.gameObject);
            entity = null;
        }
    }

    public T GetEntity<T>(int id)
        where T : Entity
    {
        Entity entity = null;
        if (m_allEntites.TryGetValue(id, out entity))
        {
            return entity as T;
        }

        return null;
    }
}

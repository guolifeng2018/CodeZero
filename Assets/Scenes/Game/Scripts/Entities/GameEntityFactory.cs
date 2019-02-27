using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntityFactory 
{

	public static T GenerateEntity<T>()
		where T : Entity
	{
		Entity entity = null;
		return entity as T;
	}
}

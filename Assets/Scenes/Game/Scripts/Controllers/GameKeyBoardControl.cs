using System;
using UnityEngine;

public class GameKeyBoardControl : GameControlBase
{
	private void LateUpdate()
	{
		//turn
		Vector2 dir = Vector2.zero;
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			dir = new Vector2(-1f, 0f);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			dir = new Vector2(1f, 0f);
		}

		if (MoveAction != null)
		{
			MoveAction(dir);
		}
		
		//accelerate
		if (AccelerateAction != null)
		{
			AccelerateAction(Input.GetKey(KeyCode.UpArrow));
		}

		if (FireAction != null)
		{
			FireAction(Input.GetKey(KeyCode.X));
		}
	}
}

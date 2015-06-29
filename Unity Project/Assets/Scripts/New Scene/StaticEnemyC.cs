using UnityEngine;
using System.Collections;

public class StaticEnemyC : MonoBehaviour
{
	Entity playerController;
	Entity entity;

	void Start()
	{
		playerController = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		entity = gameObject.GetComponent<Entity>();
	}
	public GameStateMachine.UpdateStates SetNewBox ()
	{
		if (entity.ret == GameStateMachine_N.UpdateStates.UPDATE_KEEP)
		{
			switch(direction)
			{
			case 1:
				if(playerController.currentBox == entity.currentBox.GetComponent<Box>().upBox)
				{
					entity.targetBox = playerController.currentBox;
				}
				
				break;
				
			case 2:
				if(playerController.currentBox == entity.currentBox.GetComponent<Box>().rightBox)
				{
					entity.targetBox = playerController.currentBox;
				}
				break;
				
			case 3:
				if(playerController.currentBox == entity.currentBox.GetComponent<Box>().downBox)
				{
					entity.targetBox = playerController.currentBox;
				}
				break;
				
			case 4:
				if(playerController.currentBox == entity.currentBox.GetComponent<Box>().leftBox)
				{
					entity.targetBox = playerController.currentBox;
				}
				break;
			default:
				break;
			}
		}
		
		if (currentPosition != targetPosition)
		{
			if (Mathf.Abs (currentPosition.x - targetPosition.x + currentPosition.z - targetPosition.z) < 0.1)
				currentPosition = targetPosition;
			else
				currentPosition += (targetPosition - currentPosition) / 10;
			transform.position = currentPosition;
		}
		else
		{
			if (currentBox == playerController.currentBox && playerController.hasMoved && !dead)
				playerController.Kill ();
			ret = GameStateMachine.UpdateStates.UPDATE_NEXT;
		}
		return ret;
	}
}
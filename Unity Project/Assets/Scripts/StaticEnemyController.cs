using UnityEngine;
using System.Collections;

public class StaticEnemyController : Enemy
{
	public override GameStateMachine.UpdateStates UpdateEnemy ()
	{
		if (playerController.hasMoved && ret == GameStateMachine.UpdateStates.UPDATE_KEEP)
		{
			switch(direction)
			{
			case 1:
				if(playerController.currentBox == currentBox.GetComponent<Box>().upBox && playerController.hasMoved)
				{
					currentBox = playerController.currentBox;
					targetPosition = currentBox.transform.position;
				}

				break;

			case 2:
				if(playerController.currentBox == currentBox.GetComponent<Box>().rightBox && playerController.hasMoved)
				{
					currentBox = playerController.currentBox;
					targetPosition = currentBox.transform.position;
				}
				break;

			case 3:
				if(playerController.currentBox == currentBox.GetComponent<Box>().downBox && playerController.hasMoved)
				{
					Debug.Log("yep");
					currentBox = playerController.currentBox;
					targetPosition = currentBox.transform.position;
				}
				break;

			case 4:
				if(playerController.currentBox == currentBox.GetComponent<Box>().leftBox && playerController.hasMoved)
				{
					currentBox = playerController.currentBox;
					targetPosition = currentBox.transform.position;
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
			if (currentBox == playerController.currentBox && playerController.hasMoved)
				playerController.Kill ();
			ret = GameStateMachine.UpdateStates.UPDATE_NEXT;
		}
		return ret;
	}
}

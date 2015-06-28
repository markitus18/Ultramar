using UnityEngine;
using System.Collections;

public class RunnerEnemyController : Enemy
{

	public override GameStateMachine.UpdateStates UpdateEnemy ()
	{
		if (playerController.hasMoved && !isMoving && ret == GameStateMachine.UpdateStates.UPDATE_KEEP)
			UpdateBox();
		else
			MoveEnemy ();
		return ret;
	}

	void UpdateBox()
	{
		switch(direction)
		{
		case 1:
			currentBox = currentBox.GetComponent<Box>().upBox;
			targetPosition = currentBox.transform.position;
			isMoving = true;
			Debug.Log ("R_top");
			break;
			
		case 2:
			currentBox = currentBox.GetComponent<Box>().rightBox;
			targetPosition = currentBox.transform.position;
			isMoving = true;
			Debug.Log ("R_right");
			break;
			
		case 3:
			currentBox = currentBox.GetComponent<Box>().downBox;
			targetPosition = currentBox.transform.position;
			isMoving = true;
			Debug.Log ("R_down");
			break;
			
		case 4:
			currentBox = currentBox.GetComponent<Box>().leftBox;
			targetPosition = currentBox.transform.position;
			isMoving = true;
			Debug.Log ("R_left");
			break;
			
		default:
			break;
		}
	}

	void SetNewDirection()
	{
		switch (direction)
		{
		case 1:
			if (currentBox.GetComponent<Box>().upBox == null)
				direction = 3;
			break;
		case 2:
			if (currentBox.GetComponent<Box>().rightBox == null)
				direction = 4;
			break;
		case 3:
			if (currentBox.GetComponent<Box>().downBox == null)
				direction = 1;
			break;
		case 4:
			if (currentBox.GetComponent<Box>().leftBox == null)
				direction = 2;
			break;
		}
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
	}

	void MoveEnemy()
	{
		if (currentPosition != targetPosition && isMoving)
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
			isMoving = false;
			SetNewDirection ();
		}
	}
}

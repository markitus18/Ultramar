using UnityEngine;
using System.Collections;

public class Entity_L : MonoBehaviour
{
	public GameObject lastBox;
	public GameObject currentBox;
	public GameObject targetBox;

	public float movementSpeed = 2;

	public Vector3 distanceToMove;
	[HideInInspector] public bool moving;
	[HideInInspector] public bool targetAssigned;
	[HideInInspector] public Vector3 currentPosition;
	[HideInInspector] public Vector3 targetPosition;

	int currentLvl = 1;

	public enum enumDirections
	{
		up = 1,
		right,
		down,
		left,
	}
	public enumDirections startingDirection;
	[HideInInspector] public int direction;

	[HideInInspector] public GameStateMachine.UpdateStates ret;

	void Start ()
	{
		SetCurrentBox ();
		direction = (int)startingDirection;
		currentPosition = currentBox.transform.position;
		moving = false;
		transform.position = currentPosition;
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
		currentBox.GetComponent<Box_L>().OnBoxEnter();
	}

	public GameStateMachine.UpdateStates UpdateEntity ()
	{
		ret = GameStateMachine.UpdateStates.UPDATE_KEEP;
		if (moving)
		{
			Move ();
		}
		return ret;
	}

	public void Move()
	{
		if (moving)
		{
			if (currentPosition != targetPosition)
			{
				currentPosition += distanceToMove / (16 / movementSpeed);
				transform.position = currentPosition;
			}
			else
			{
				currentBox = targetBox;
				moving = false;
				ret = GameStateMachine.UpdateStates.UPDATEEXT;
			}
		}
		else
		{
			ret = GameStateMachine.UpdateStates.UPDATEEXT;
		}

	}

	public void SetNewPosition()
	{
		if (targetBox)
			targetBox.GetComponent<Box>().UpdatePosition (gameObject);
	}

	void SetCurrentBox()
	{
		currentBox = GameObject.Find("Box_start2");
		lastBox = currentBox;
		while (currentLvl < GameControl.control.unlockedLevel)
		{
			if (currentBox.GetComponent<Box_L>().upBox && currentBox.GetComponent<Box_L>().upBox != lastBox)
			{
				lastBox = currentBox;
				currentBox = currentBox.GetComponent<Box_L>().upBox;
			}
			else if (currentBox.GetComponent<Box_L>().leftBox && currentBox.GetComponent<Box_L>().leftBox != lastBox)
			{
				lastBox = currentBox;
				currentBox = currentBox.GetComponent<Box_L>().leftBox;
			}
			else if (currentBox.GetComponent<Box_L>().rightBox && currentBox.GetComponent<Box_L>().rightBox != lastBox)
			{
				lastBox = currentBox;
				currentBox = currentBox.GetComponent<Box_L>().rightBox;
			}
			else if (currentBox.GetComponent<Box_L>().downBox && currentBox.GetComponent<Box_L>().downBox != lastBox)
			{
				lastBox = currentBox;
				currentBox = currentBox.GetComponent<Box_L>().downBox;
			}
			currentLvl++;
		}
	}
}

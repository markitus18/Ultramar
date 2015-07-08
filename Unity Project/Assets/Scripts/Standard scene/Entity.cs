using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public GameObject currentBox;
	public GameObject targetBox;

	public float movementSpeed = 2;

	public Vector3 distanceToMove;
	[HideInInspector] public bool moving;
	[HideInInspector] public bool targetAssigned;
	[HideInInspector] public Vector3 currentPosition;
	[HideInInspector] public Vector3 targetPosition;

	public enum enumDirections
	{
		up = 1,
		right,
		down,
		left
	}
	public enumDirections startingDirection;
	[HideInInspector] public int direction;

	[HideInInspector] public GameStateMachine.UpdateStates ret;

	void Awake()
	{
		direction = (int)startingDirection;
		currentPosition = currentBox.transform.position;
	}
	void Start ()
	{
		moving = false;
		transform.position = currentPosition;
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
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

	public void SetNewBox ()
	{
		switch(direction)
		{
		case 1:
			targetBox = currentBox.GetComponent<Box>().upBox;
			targetBox.GetComponent<Box>().enemies.Add (gameObject);
			currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			break;
		case 2:
			targetBox = currentBox.GetComponent<Box>().rightBox;
			targetBox.GetComponent<Box>().enemies.Add (gameObject);
			currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			break;
		case 3:
			targetBox = currentBox.GetComponent<Box>().downBox;
			targetBox.GetComponent<Box>().enemies.Add (gameObject);
			currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			break;
		case 4:
			targetBox = currentBox.GetComponent<Box>().leftBox;
			targetBox.GetComponent<Box>().enemies.Add (gameObject);
			currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			break;
		default:
			break;
		}
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
	}

	public void SetNewPosition()
	{
		if (targetBox)
			targetBox.GetComponent<Box>().UpdatePosition (gameObject);
	}
}

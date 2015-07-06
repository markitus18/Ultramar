using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{

	public GameObject previousBox;
	public GameObject currentBox;
	public GameObject targetBox;

	public float movementSpeed = 2;

	public Vector3 distanceToMove;
	[HideInInspector] public bool moving;

	[HideInInspector] public Vector3 previousPosition;
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

	[HideInInspector] public GameStateMachine_N.UpdateStates ret;

	void Awake()
	{
		direction = (int)startingDirection;
		currentPosition = currentBox.transform.position;
	}
	void Start ()
	{
		moving = false;
		previousBox = currentBox;
		previousPosition = currentPosition;
		transform.position = currentPosition;
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
	}

	public GameStateMachine_N.UpdateStates UpdateEntity ()
	{
		ret = GameStateMachine_N.UpdateStates.UPDATE_KEEP;
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
				previousBox.GetComponent<Box_N> ().enemies.Remove(gameObject);
				previousBox = currentBox;
				moving = false;
				previousPosition = currentPosition;
				ret = GameStateMachine_N.UpdateStates.UPDATE_NEXT;
			}
		}
		else
		{
			ret = GameStateMachine_N.UpdateStates.UPDATE_NEXT;
		}

	}

	public void SetNewBox ()
	{
		switch(direction)
		{
		case 1:
			currentBox = currentBox.GetComponent<Box_N>().upBox; moving = true;
			break;
		case 2:
			currentBox = currentBox.GetComponent<Box_N>().rightBox; moving = true;
			break;
		case 3:
			currentBox = currentBox.GetComponent<Box_N>().downBox; moving = true;
			break;
		case 4:
			currentBox = currentBox.GetComponent<Box_N>().leftBox; moving = true;
			break;
		default:
			break;
		}
		if (moving)
		{
			currentBox.GetComponent<Box_N> ().enemies.Add (gameObject);
			currentBox.GetComponent<Box_N> ().UpdatePositions ();
			distanceToMove = targetPosition - previousPosition;
		}
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
	}
}

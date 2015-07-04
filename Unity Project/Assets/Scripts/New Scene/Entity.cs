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
	bool boxAsigned;

	Vector3 currentPosition;
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
		boxAsigned = false;
		while (!boxAsigned)
		{
			switch(direction)
			{
			case 1:
				targetPosition =  currentBox.GetComponent<Box_N>().upBox.transform.position;
				distanceToMove = targetPosition - currentBox.transform.position;
				currentBox = currentBox.GetComponent<Box_N>().upBox;
				currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
				moving = true;
				boxAsigned = true;
				break;
			case 2:
				targetPosition =  currentBox.GetComponent<Box_N>().rightBox.transform.position;
				distanceToMove = targetPosition - currentBox.transform.position;
				currentBox = currentBox.GetComponent<Box_N>().rightBox;
				currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
				moving = true;
				boxAsigned = true;
				break;
			case 3:
				targetPosition =  currentBox.GetComponent<Box_N>().downBox.transform.position;
				distanceToMove = targetPosition - currentBox.transform.position;
				currentBox = currentBox.GetComponent<Box_N>().downBox;
				currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
				moving = true;
				boxAsigned = true;
				break;
			case 4:
				targetPosition = currentBox.GetComponent<Box_N>().leftBox.transform.position;
				distanceToMove = targetPosition - currentBox.transform.position;
				currentBox = currentBox.GetComponent<Box_N>().leftBox;
				currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
				moving = true;
				boxAsigned = true;
				break;
			default:
				break;
			}
		}
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
	}
}

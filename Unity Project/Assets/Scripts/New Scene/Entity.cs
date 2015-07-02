using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public GameObject currentBox;
	public GameObject targetBox;

	public float movementSpeed = 2;

	/*[HideInInspector]*/ public Vector3 distanceToMove;
	[HideInInspector] public bool moving;

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
	void Start ()
	{
		direction = (int)startingDirection;
		moving = false;
		currentPosition = currentBox.transform.position;
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
//				distanceToMove = targetPosition - currentBox.transform.position;
				currentPosition += distanceToMove / (16 / movementSpeed);
				transform.position = currentPosition;
			}
			else
			{
				if (gameObject.tag == "Enemy")
				{
					targetBox.GetComponent<Box_N> ().enemies.Add (gameObject);
					currentBox.GetComponent<Box_N> ().enemies.Remove (gameObject);
				}
				moving = false;
				ret = GameStateMachine_N.UpdateStates.UPDATE_NEXT;
			}
		}
		else
		{
			ret = GameStateMachine_N.UpdateStates.UPDATE_NEXT;
		}

	}
}

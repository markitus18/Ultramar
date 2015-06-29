using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public GameObject currentBox;
	public GameObject targetBox;

	public float movementSpeed = 2;

	Vector3 distanceToMove;
	[HideInInspector] public bool moving;

	Vector3 currentPosition;
	Vector3 targetPosition;
	public int direction;

	public GameStateMachine_N.UpdateStates ret;
	void Start ()
	{
		moving = false;
		currentPosition = currentBox.transform.position;
		transform.position = currentPosition;
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
	}

	public GameStateMachine_N.UpdateStates UpdateEntity ()
	{
		ret = GameStateMachine_N.UpdateStates.UPDATE_KEEP;
		if (moving)
			Move ();
		return ret;
	}

	public void Move()
	{
		if (targetBox)
		{
			if (currentPosition != targetBox.transform.position)
			{
				Debug.Log ("Moving");
				distanceToMove = targetBox.transform.position - currentBox.transform.position;
				currentPosition += distanceToMove / (10 / movementSpeed);
				transform.position = currentPosition;
			}
			else
			{
				currentBox = targetBox;
				moving = false;
				ret = GameStateMachine_N.UpdateStates.UPDATE_NEXT;
			}
		}
		else
			ret = GameStateMachine_N.UpdateStates.UPDATE_NEXT;
	}

	void OnMouseUp()
	{
		moving = true;
	}
}

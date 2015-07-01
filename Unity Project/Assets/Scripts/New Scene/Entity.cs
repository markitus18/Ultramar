using UnityEngine;
using System.Collections;

public enum directionsEnum
{   up = 1,
	right = 2,
	down = 3,
	left = 4
};

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
		RaycastHit hit;
		Vector3 SearchFrom = transform.position;
		SearchFrom.y += 1;
		Ray DownRay = new Ray(SearchFrom, Vector3.down);
		
		if (Physics.Raycast(DownRay, out hit, 1))
		{
			if(hit.collider.tag == "Box")
			{
				currentBox = hit.transform.gameObject;
			}
		}

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
		if (targetBox)
		{
			if (currentPosition != targetBox.transform.position)
			{
				distanceToMove = targetBox.transform.position - currentBox.transform.position;
				currentPosition += distanceToMove / (16 / movementSpeed);
				transform.position = currentPosition;
			}
			else
			{
				if (gameObject.tag == "Enemy")
				{
					targetBox.GetComponent<Box_N>().enemies.Add(gameObject);
					currentBox.GetComponent<Box_N>().enemies.Remove (gameObject);
				}
				currentBox = targetBox;
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
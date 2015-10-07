using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public GameObject startingBox;
    public GameObject currentBox;
	public GameObject targetBox;

	public float movementSpeed = 2;

	public Vector3 distanceToMove;
	/*[HideInInspector] */public bool moving;
	[HideInInspector] public bool targetAssigned;
	[HideInInspector] public Vector3 currentPosition;
	[HideInInspector] public Vector3 targetPosition;

/*	[HideInInspector] */public bool active;
	public enum enumDirections
	{
		up = 1,
		right,
		down,
		left,
	}
	public enumDirections startingDirection;
	/*[HideInInspector]*/ public int direction;

	[HideInInspector] public GameStateMachine.UpdateStates ret;

    public void Reset ()
    {
		currentBox.GetComponent<Box>().enemies.Remove(gameObject);
        if (!gameObject.GetComponent<PlayerController>())
        {
			gameObject.SetActive(true);
            startingBox.GetComponent<Box>().enemies.Add(gameObject);
			targetBox = null;
        }
		active = true;
		if (Application.loadedLevel != 12 && Application.loadedLevel != 14)
        moving = false;
        currentBox = startingBox;
        direction = (int)startingDirection;
        currentPosition = currentBox.transform.position;
        transform.position = currentPosition;
        transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
    }

	void Awake()
	{
        startingBox = currentBox;
		direction = (int)startingDirection;
		currentPosition = currentBox.transform.position;
		transform.position = currentPosition;
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
		moving = false;
	}
	void Start ()
	{
		active = true;
		currentBox = startingBox;
		gameObject.GetComponent<Collider>().enabled = true;

		currentBox.GetComponent<Box>().enemies.Add(gameObject);
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
				currentPosition += distanceToMove / movementSpeed;
				transform.position = currentPosition;
			}
			else
			{
				currentBox = targetBox;
				moving = false;
				ret = GameStateMachine.UpdateStates.UPDATENEXT;
			}
		}
		else
		{
			ret = GameStateMachine.UpdateStates.UPDATENEXT;
		}

	}

	public void SetNewBox ()
	{
		switch(direction)
		{
		case 1:
			if (currentBox.GetComponent<Box>().upBox)
			{
				targetBox = currentBox.GetComponent<Box>().upBox;
				targetBox.GetComponent<Box>().enemies.Add (gameObject);
				currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			}
			break;
		case 2:
			if (currentBox.GetComponent<Box>().rightBox)
			{
				targetBox = currentBox.GetComponent<Box>().rightBox;
				targetBox.GetComponent<Box>().enemies.Add (gameObject);
				currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			}
			break;
		case 3:
			if (currentBox.GetComponent<Box>().downBox)
			{
				targetBox = currentBox.GetComponent<Box>().downBox;
				targetBox.GetComponent<Box>().enemies.Add (gameObject);
				currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			}
			break;
		case 4:
			if (currentBox.GetComponent<Box>().leftBox)
			{
				targetBox = currentBox.GetComponent<Box>().leftBox;
				targetBox.GetComponent<Box>().enemies.Add (gameObject);
				currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			}
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

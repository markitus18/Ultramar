using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public GameObject currentBox;
	public GameObject text;
//	public GameStateMachine gameStateMachine;

	Vector3 targetPosition;
	Vector3 currentPosition;
	
	bool isMoving;
	[HideInInspector]
	public bool hasMoved;
	bool pausePlayer;

	GameStateMachine.UpdateStates ret;

	void Start ()
	{
		//Moving the player to our starting box
		isMoving = false;
		currentPosition = GameObject.Find ("Box_start").transform.position;
		Debug.Log ("Player moved to starting box");
		transform.position = currentPosition;
		targetPosition = currentPosition;
		currentBox = GameObject.Find ("Box_start");
	}
	
	// Update is called once per frame
	public GameStateMachine.UpdateStates UpdatePlayer()
	{
		ret = GameStateMachine.UpdateStates.UPDATE_KEEP;
		if (!pausePlayer)
		{
			if (isMoving)
				MovePlayer (currentBox);
		}
		return ret;
	}

	public void MovePlayer(GameObject newBox)
	{
		if (!pausePlayer)
		{
			if (newBox != currentBox && !isMoving)
			{
				if (currentBox.GetComponent<LevelBox>().upBox == newBox)
				{
					currentBox = newBox;
					targetPosition = currentBox.transform.position;
					isMoving = true;
					hasMoved = false;
				}
				
				else if (currentBox.GetComponent<LevelBox>().downBox == newBox)
				{
					currentBox = newBox;
					targetPosition = currentBox.transform.position;
					isMoving = true;
					hasMoved = false;
				}
				
				else if (currentBox.GetComponent<LevelBox>().rightBox == newBox)
				{
					currentBox = newBox;
					targetPosition = currentBox.transform.position;
					isMoving = true;
					hasMoved = false;
				}
				
				else if (currentBox.GetComponent<LevelBox>().leftBox == newBox)
				{
					currentBox = newBox;
					targetPosition = currentBox.transform.position;
					isMoving = true;
					hasMoved = false;
				}
			}

			if (currentPosition != targetPosition && isMoving)
			{
				if (Mathf.Abs (currentPosition.x - targetPosition.x + currentPosition.z - targetPosition.z) < 0.1)
					currentPosition = targetPosition;
				else
					currentPosition += (targetPosition - currentPosition) / 10;
				transform.position = currentPosition;
			}
			else if (isMoving)
			{
				isMoving = false;
				hasMoved = true;
				ret = GameStateMachine.UpdateStates.UPDATE_NEXT;
			}
		}
	}

	public void Kill()
	{
		pausePlayer = true;
		text.GetComponent<Renderer>().enabled = true;
	}
}

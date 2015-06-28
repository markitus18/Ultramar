using UnityEngine;
using System.Collections;

public class RandomEnemyController : MonoBehaviour
{
	//Game variables
	public PlayerController playerController;
	public GameObject currentBox;

	//Self-controller variables
	int direction;
	bool isMoving;

	Vector3 currentPosition;
	Vector3 targetPosition;


	void Start ()
	{
		currentPosition = currentBox.transform.position;
		transform.position = currentPosition;
		targetPosition = currentPosition;
	}


	void Update ()
	{
		if (!isMoving && playerController.hasMoved)
		{
			direction = Random.Range (1, 5);
			MoveEnemy(direction);
		}
		else
		{
			MoveEnemy(0);
		}
		if (playerController.currentBox == currentBox)
		{
			playerController.Kill();
		}
	}

	void MoveEnemy (int direction)
	{
		switch(direction)
		{
		case 1:
			if (currentBox.GetComponent<Box>().upBox)
			{
				currentBox = currentBox.GetComponent<Box>().upBox;
			}
			else if (currentBox.GetComponent<Box>().downBox)
			{
				currentBox = currentBox.GetComponent<Box>().downBox;
			}
			targetPosition = currentBox.transform.position;
			isMoving = true;
			break;

		case 2:
			if (currentBox.GetComponent<Box>().rightBox)
			{
				currentBox = currentBox.GetComponent<Box>().rightBox;
			}
			else if (currentBox.GetComponent<Box>().leftBox)
			{
				currentBox = currentBox.GetComponent<Box>().leftBox;
			}
			targetPosition = currentBox.transform.position;
			isMoving = true;
			break;

		case 3:
			if (currentBox.GetComponent<Box>().downBox)
			{
				currentBox = currentBox.GetComponent<Box>().downBox;
			}
			else if (currentBox.GetComponent<Box>().upBox)
			{
				currentBox = currentBox.GetComponent<Box>().upBox;
			}
			targetPosition = currentBox.transform.position;
			isMoving = true;
			break;

		case 4:
			if (currentBox.GetComponent<Box>().leftBox)
			{
				currentBox = currentBox.GetComponent<Box>().leftBox;
			}

			else if (currentBox.GetComponent<Box>().rightBox)
			{
				currentBox = currentBox.GetComponent<Box>().rightBox;
			}

			targetPosition = currentBox.transform.position;
			isMoving = true;
			break;

		default:
			break;
		}

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
			isMoving = false;
			playerController.hasMoved = false;
		}

	}
}

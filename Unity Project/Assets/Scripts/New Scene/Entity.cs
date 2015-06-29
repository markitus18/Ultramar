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

	void Start ()
	{
		moving = false;
		currentPosition = currentBox.transform.position;
		transform.position = currentPosition;
	}

	void Update ()
	{
		if (moving)
			Move ();
	}

	void Move()
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
		}
	}

	void OnMouseUp()
	{
		moving = true;
	}
}

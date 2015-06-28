using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
	public GameObject upBox;
	public GameObject downBox;
	public GameObject rightBox;
	public GameObject leftBox;
	public PlayerController playerController;
	public GameStateMachine gameStateMachine;

	Color originalColor;

	void Start ()
	{
		//Creating four box-detector rays
		gameStateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		RaycastHit hit;
		Ray upRay = new Ray(transform.position, Vector3.forward);
		Ray downRay = new Ray(transform.position, Vector3.back);
		Ray rightRay = new Ray(transform.position, Vector3.right);
		Ray leftRay = new Ray(transform.position, Vector3.left);

		//Setting the closest boxes
		if (Physics.Raycast(upRay, out hit, 100) && upBox == null)
		{
			if(hit.collider.tag == "box")
			{
				upBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(downRay, out hit, 100) && downBox == null)
		{
			if(hit.collider.tag == "box")
			{
				downBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(rightRay, out hit, 100) && rightBox == null)
		{
			if(hit.collider.tag == "box")
			{
				rightBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(leftRay, out hit, 100) && leftBox == null)
		{
			if(hit.collider.tag == "box")
			{
				leftBox = hit.transform.gameObject;
			}
		}

		//Setting variables to start point
		originalColor = transform.GetComponent<Renderer>().material.color;
	}

	void Update ()
	{

	}

	void OnMouseUp()
	{
		Debug.Log (name + " clicked");
		if (gameStateMachine.state == GameStateMachine.GameStates.PLAYER_TURN)
			playerController.MovePlayer(gameObject);
	}

	void OnMouseEnter()
	{
		transform.GetComponent<Renderer>().material.color = Color.blue;
	}

	void OnMouseExit()
	{
		transform.GetComponent<Renderer>().material.color = originalColor;
	}
}
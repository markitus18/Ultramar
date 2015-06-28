using UnityEngine;
using System.Collections;

public class LevelBox : MonoBehaviour
{
	public Level_PlayerController playerController;

	public GameObject upBox;
	public GameObject downBox;
	public GameObject rightBox;
	public GameObject leftBox;

	public int levelToLoad;

	Color originalColor;
	
	void Start ()
	{
		//Creating four box-detector rays
		playerController = GameObject.FindWithTag("Player").GetComponent<Level_PlayerController>();
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

	void OnMouseDown()
	{
		if (playerController.currentBox != gameObject)
			playerController.MovePlayer(gameObject);
		else if (playerController.currentBox == gameObject)
			Application.LoadLevel(levelToLoad);
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
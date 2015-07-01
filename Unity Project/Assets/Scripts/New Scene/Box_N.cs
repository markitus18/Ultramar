using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Box_N : MonoBehaviour {
	//Directions un which you can move
	public bool up;
	public bool right;
	public bool down;
	public bool left;
	//Linked boxes
	public GameObject upBox;
	public GameObject downBox;
	public GameObject rightBox;
	public GameObject leftBox;

	PlayerController_N playerController;
	public List<GameObject> enemies;

	Color originalColor;

	void Start ()
	{
		//Assigning linked boxes
		Debug.Log("Assigning linked boxes");
		RaycastHit hit;
		Ray upRay = new Ray(transform.position, Vector3.forward);
		Ray downRay = new Ray(transform.position, Vector3.back);
		Ray rightRay = new Ray(transform.position, Vector3.right);
		Ray leftRay = new Ray(transform.position, Vector3.left);

		if (Physics.Raycast(upRay, out hit, 1) && upBox == null && up==true)
		{
			if(hit.collider.tag == "Box")
			{
				upBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(downRay, out hit, 1) && downBox == null && up==down)
		{
			if(hit.collider.tag == "Box")
			{
				downBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(rightRay, out hit, 1) && rightBox == null && right==true)
		{
			if(hit.collider.tag == "Box")
			{
				rightBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(leftRay, out hit, 1) && leftBox == null && left==true)
		{
			if(hit.collider.tag == "Box")
			{
				leftBox = hit.transform.gameObject;
			}
		}

		//Setting all variables
		playerController = GameObject.FindWithTag ("Player").GetComponent<PlayerController_N>();
		//Saving original color
		originalColor = transform.GetComponent<Renderer>().material.color;
	}
	

	void Update ()
	{
	
	}

	void OnMouseEnter()
	{
		transform.GetComponent<Renderer>().material.color = Color.blue;
	}
	
	void OnMouseExit()
	{
		transform.GetComponent<Renderer>().material.color = originalColor;
	}

	void OnMouseUp()
	{
		Debug.Log ("doing something");
		playerController.SetNewBox(gameObject);
	}
}

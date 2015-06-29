using UnityEngine;
using System.Collections;

public class Box_N : MonoBehaviour {

	//Linked boxes
	public GameObject upBox;
	public GameObject downBox;
	public GameObject rightBox;
	public GameObject leftBox;

	PlayerController_N playerController;

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

		if (Physics.Raycast(upRay, out hit, 1) && upBox == null)
		{
			if(hit.collider.tag == "Box")
			{
				upBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(downRay, out hit, 1) && downBox == null)
		{
			if(hit.collider.tag == "Box")
			{
				downBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(rightRay, out hit, 1) && rightBox == null)
		{
			if(hit.collider.tag == "Box")
			{
				rightBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(leftRay, out hit, 1) && leftBox == null)
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
		playerController.SetNewBox(gameObject);
	}
}

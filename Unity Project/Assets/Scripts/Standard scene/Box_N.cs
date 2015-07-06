using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Box_N : MonoBehaviour {
	//Directions in which you can move
	public bool up;
	public bool right;
	public bool down;
	public bool left;
	//Linked boxes
	public GameObject upBox;
	public GameObject downBox;
	public GameObject rightBox;
	public GameObject leftBox;

	public int boxDistance;
//	public Material standardMaterial;
	PlayerController_N playerController;
	/*[HideInInspector]*/ public List<GameObject> enemies;

	public Material standardMaterial;
	[HideInInspector] public Material originalMaterial;
	public Material highlightMaterial;

	void Awake ()
	{
		//Assigning linked boxes
		RaycastHit hit;
		Ray upRay = new Ray(transform.position, Vector3.forward);
		Ray downRay = new Ray(transform.position, Vector3.back);
		Ray rightRay = new Ray(transform.position, Vector3.right);
		Ray leftRay = new Ray(transform.position, Vector3.left);

		if (Physics.Raycast(upRay, out hit, boxDistance) && upBox == null && up==true)
		{
			if(hit.collider.tag == "Box")
			{
				upBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(downRay, out hit, boxDistance) && downBox == null && up==down)
		{
			if(hit.collider.tag == "Box")
			{
				downBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(rightRay, out hit, boxDistance) && rightBox == null && right==true)
		{
			if(hit.collider.tag == "Box")
			{
				rightBox = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(leftRay, out hit, boxDistance) && leftBox == null && left==true)
		{
			if(hit.collider.tag == "Box")
			{
				leftBox = hit.transform.gameObject;
			}
		}

		//Setting all variables
		playerController = GameObject.FindWithTag ("Player").GetComponent<PlayerController_N>();
		//Saving original color
		originalMaterial = transform.GetComponent<Renderer>().material;
	}
	

	void Update ()
	{
	
	}

	void OnMouseEnter()
	{
		transform.GetComponent<Renderer>().material = highlightMaterial;
	}
	
	void OnMouseExit()
	{
		transform.GetComponent<Renderer>().material = originalMaterial;
	}

	void OnMouseUp()
	{
		Debug.Log ("doing something");
		playerController.SetNewBox(gameObject);
	}

	public void UpdatePosition(GameObject enemy)
	{
		switch(enemies.Count)
		{
		case 1:
			if (enemies[0] = enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position;
				if (enemy.GetComponent<Entity>().targetPosition != enemy.GetComponent<Entity>().currentPosition)
				{
					enemy.GetComponent<Entity>().distanceToMove = enemies[0].GetComponent<Entity>().targetPosition - enemies[0].transform.position;
					enemy.GetComponent<Entity>().moving = true;
				}
			}
			break;
		case 2:
			if (enemies[0] = enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position + new Vector3(0, 0, 0.5f);
				if (enemy.GetComponent<Entity>().targetPosition != enemy.GetComponent<Entity>().currentPosition)
				{
					enemy.GetComponent<Entity>().distanceToMove = enemy.GetComponent<Entity>().targetPosition - enemies[0].transform.position;
					enemy.GetComponent<Entity>().moving = true;
				}
			}
			else if (enemies[1] = enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position + new Vector3(0, 0, -0.5f);
				if (enemy.GetComponent<Entity>().targetPosition != enemy.GetComponent<Entity>().currentPosition)
				{
					enemy.GetComponent<Entity>().distanceToMove = enemy.GetComponent<Entity>().targetPosition - enemy.transform.position;
					enemy.GetComponent<Entity>().moving = true;
				}
			}
			break;
		default:
			break;
		}
	}
}

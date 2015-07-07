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
	public float entityDistance;
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
		if (!enemy.GetComponent<Entity>().targetAssigned)
		{
			int groups = Mathf.CeilToInt((float)enemies.Count / 2);
			int sign;
			if (groups % 2 == 1)
			{
				for (int i = 0; i < groups; i++)
				{
					sign = (i % 2 == 0) ?  -1 : 1;

					if ((i * 2) + 1 < enemies.Count)
					{
						enemies[i * 2].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z + entityDistance;
						enemies[(i * 2)+ 1].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z - entityDistance;

						enemies[i * 2].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistance * Mathf.CeilToInt(i / 2) * sign);
						enemies[(i * 2)+ 1].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistance * Mathf.CeilToInt(i / 2) * sign);
					}
					else
					{
						enemies[i * 2].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z;

						float l = Mathf.CeilToInt(i / 2);
						float m = (entityDistance * Mathf.CeilToInt(i / 2) * sign);
						float k = gameObject.transform.position.x + (entityDistance * Mathf.CeilToInt(i / 2) * sign);
						enemies[i * 2].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistance * Mathf.CeilToInt(i / 2) * sign);

					}
				}
			}
			else if (groups % 2 == 0)
			{
				for (int i = 0; i < groups; i++)
				{
					sign = (i % 2 == 0) ?  -1 : 1;
					
					if ((i * 2) + 1 < enemies.Count)
					{
						enemies[i * 2].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z + entityDistance;
						enemies[(i * 2)+ 1].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z - entityDistance;
						
						enemies[i * 2].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistance * (i / 2) * Mathf.CeilToInt(i / 2) * sign);
						enemies[(i * 2)+ 1].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistance * Mathf.CeilToInt(i / 2) * sign);
					}
					else
					{
						enemies[i * 2].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z;
						enemies[i * 2].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistance * Mathf.CeilToInt(i / 2) * sign);
					}
				}
			}
		/*
		switch(enemies.Count)
		{
		case 1:
			if (enemies[0] == enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position;
			}
			break;
		case 2:
			if (enemies[0] == enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position + new Vector3(0, 0, 0.5f);
			}
			else if (enemies[1] == enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position + new Vector3(0, 0, -0.5f);
			}
			break;
		case 3:
			if (enemies[0] == enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position + new Vector3(0, 0, 0.5f);
			}
			else if (enemies[1] == enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position + new Vector3(0, 0, -0.5f);
			}
			else if (enemies[2] == enemy)
			{
				enemy.GetComponent<Entity>().targetPosition = gameObject.transform.position + new Vector3(-0.5f, 0, 0);
			}
			break;
		default:
			break;
		}
		*/
			if (enemy.GetComponent<Entity>().targetPosition != enemy.GetComponent<Entity>().currentPosition)
			{
				enemy.GetComponent<Entity>().distanceToMove = enemy.GetComponent<Entity>().targetPosition - enemy.transform.position;
				enemy.GetComponent<Entity>().moving = true;
			}
		}
	}
}

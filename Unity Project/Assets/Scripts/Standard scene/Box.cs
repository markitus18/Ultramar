using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Box : MonoBehaviour {
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
	public float entityDistanceX;
	public float entityDistanceZ;
	PlayerController playerController;

	/*[HideInInspector]*/ public List<GameObject> enemies;

	[HideInInspector] public Material originalMaterial;

	void Awake ()
	{
		//Assigning linked boxes
# if !UNITY_EDITOR
		gameObject.GetComponent<MeshRenderer>().enabled = false;
#endif
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
		if (Physics.Raycast(downRay, out hit, boxDistance) && downBox == null && down==true)
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
		playerController = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		//Saving original color
		originalMaterial = transform.GetComponent<Renderer>().material;

	}

	public void UpdatePosition(GameObject enemy)
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
					enemies[i * 2].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z + entityDistanceZ;
					enemies[(i * 2)+ 1].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z - entityDistanceZ;

					enemies[i * 2].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistanceX * Mathf.CeilToInt((float)i / 2) * sign);
					enemies[(i * 2)+ 1].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistanceX * Mathf.CeilToInt((float)i / 2) * sign);
				}
				else
				{
					enemies[i * 2].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z;


					enemies[i * 2].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + (entityDistanceX * Mathf.CeilToInt((float)i / 2) * sign);

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
					enemies[i * 2].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z + entityDistanceZ;
					enemies[(i * 2)+ 1].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z - entityDistanceZ;

					enemies[i * 2].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + ((entityDistanceX * 0.5f + entityDistanceX * Mathf.FloorToInt((float)i / 2)) * sign);
					enemies[(i * 2)+ 1].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + ((entityDistanceX * 0.5f + entityDistanceX * Mathf.FloorToInt((float)i / 2)) * sign);
				}
				else
				{
					enemies[i * 2].GetComponent<Entity>().targetPosition.z = gameObject.transform.position.z;
					enemies[i * 2].GetComponent<Entity>().targetPosition.x = gameObject.transform.position.x + ((entityDistanceX * 0.5f + entityDistanceX * Mathf.FloorToInt((float)i / 2)) * sign);
				}
			}
		}
		
		if (enemy.GetComponent<Entity>().targetPosition != enemy.GetComponent<Entity>().currentPosition)
		{
			enemy.GetComponent<Entity>().distanceToMove = enemy.GetComponent<Entity>().targetPosition - enemy.transform.position;
			enemy.GetComponent<Entity>().moving = true;
		}
	}
}

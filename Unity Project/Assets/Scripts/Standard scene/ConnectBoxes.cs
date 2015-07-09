﻿using UnityEngine;
using System.Collections;

public class ConnectBoxes : MonoBehaviour
{
	public enum blockingDirection2
	{
		vertical,
		horizontal
	}
	
	public GameObject box1;
	public GameObject box2;
	
	public blockingDirection2 direction;
	public Box box;
	bool join;
	
	void Start ()
	{
		//gameObject.GetComponent<Collider>().enabled = true;
		//Setting boxes
		RaycastHit hit;
		Ray positiveRay;
		Ray negativeRay;
		if (direction == blockingDirection2.vertical)
		{
			positiveRay = new Ray(transform.position, Vector3.forward);
			negativeRay = new Ray(transform.position, Vector3.back);
		}
		else
		{
			positiveRay = new Ray(transform.position, Vector3.right);
			negativeRay = new Ray(transform.position, Vector3.left);
		}
		
		if (Physics.Raycast(positiveRay, out hit, box.boxDistance))
		{
			if(hit.collider.tag == "Box")
			{
				box1 = hit.transform.gameObject;
			}
		}
		if (Physics.Raycast(negativeRay, out hit, box.boxDistance))
		{
			if(hit.collider.tag == "Box")
			{
				box2 = hit.transform.gameObject;
			}
		}
		
		join = false;		
	}
	
	public void JoinBoxes()
	{
		Debug.Log("Splitting boxes");
		if (direction == blockingDirection2.vertical)
		{
			box1.GetComponent<Box>().downBox = box2;
			box2.GetComponent<Box>().upBox = box1;
		}
		else
		{
			box1.GetComponent<Box>().leftBox = box2;
			box2.GetComponent<Box>().rightBox = box1;
		}
		join = true;
	}
}
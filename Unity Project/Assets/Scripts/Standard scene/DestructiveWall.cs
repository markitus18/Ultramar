using UnityEngine;
using System.Collections;

public class DestructiveWall : MonoBehaviour
{
	public enum blockingDirection
	{
		vertical,
		horizontal
	}

	public GameObject box1;
	public GameObject box2;

	public blockingDirection direction;
	public Box box;
	bool split;

	void Start ()
	{
		gameObject.GetComponent<Collider>().enabled = true;
		//Setting boxes
		RaycastHit hit;
		Ray positiveRay;
		Ray negativeRay;
		if (direction == blockingDirection.vertical)
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

		split = false;		
	}

	void SplitBoxes()
	{
		Debug.Log("Splitting boxes");
		if (direction == blockingDirection.vertical)
		{
			box1.GetComponent<Box>().downBox = null;
			box2.GetComponent<Box>().upBox = null;
		}
		else
		{
			box1.GetComponent<Box>().leftBox = null;
			box2.GetComponent<Box>().rightBox = null;
		}
		split = true;
	}

	void OnMouseUp()
	{
		if (!split)
			SplitBoxes ();
	}
}

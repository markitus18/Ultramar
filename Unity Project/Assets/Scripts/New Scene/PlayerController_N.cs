using UnityEngine;
using System.Collections;

public class PlayerController_N : MonoBehaviour
{
	Entity entity;
	// Use this for initialization
	void Start()
	{
		entity = gameObject.GetComponent<Entity>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void SetNewBox (GameObject newBox)
	{
		Debug.Log ("Checking new box");
		Debug.Log (newBox);
		Debug.Log (entity.currentBox.GetComponent<Box_N>().upBox);
		bool available = false;
		if (entity.currentBox.GetComponent<Box_N>().upBox == newBox)
			available = true;
		else if (entity.currentBox.GetComponent<Box_N>().downBox == newBox)
			available = true;		
		else if (entity.currentBox.GetComponent<Box_N>().rightBox == newBox)
			available = true;
		else if (entity.currentBox.GetComponent<Box_N>().leftBox == newBox)
			available = true;
		if (available)
		{
			Debug.Log ("Assigned");
			entity.targetBox = newBox;
			entity.moving =  true;
		}
	}
}

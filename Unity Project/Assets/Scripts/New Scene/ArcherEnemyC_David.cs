using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherEnemyC_David: MonoBehaviour
{
	Entity playerEntity;
	PlayerController_N playerController;
	Entity entity;

	public bool up;
	public bool right;
	public bool down;
	public bool left;

	int counter;
	
	void Start()
	{
		counter = 0;
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController_N>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box_N>().enemies.Add(gameObject);
	}
	public void CheckPlayer()
	{
		RaycastHit hit;
		Ray ray = new Ray (transform.position, Vector3.up);
		Vector3 searchFromHere = transform.position;
		searchFromHere.y += 0.05f;
		switch (entity.direction) {
		case 1:
			{
			ray = new Ray (searchFromHere, Vector3.forward);
				break; }
		case 2:
			{
				ray = new Ray (searchFromHere, Vector3.right);
				break; }
		case 3:
			{
			ray = new Ray (searchFromHere, Vector3.back);
				break; }
		case 4:
			{
			ray = new Ray (searchFromHere, Vector3.left);
				break; }
		}
		if (Physics.Raycast(ray, out hit, 1))
		{
			if(hit.collider.tag == "Player")
			{
				entity.targetBox = playerEntity.currentBox;
				playerController.Kill ();
			}
		}

	}
}

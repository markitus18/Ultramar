using UnityEngine;
using System.Collections;

public class RunnerEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController_N playerController;
	Entity entity;
	bool boxAsigned;
	
	void Start()
	{
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController_N>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box_N>().enemies.Add(gameObject);
	}
	public void SetNewBox ()
	{
		boxAsigned = false;
		while (!boxAsigned)
		{
			switch(entity.direction)
			{
			case 1:
				entity.targetPosition =  entity.currentBox.GetComponent<Box_N>().upBox.transform.position;
				entity.distanceToMove = entity.targetPosition - entity.currentBox.transform.position;
				entity.currentBox = entity.currentBox.GetComponent<Box_N>().upBox;
				entity.currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
				entity.moving = true;
				boxAsigned = true;
				break;
			case 2:
				entity.targetPosition =  entity.currentBox.GetComponent<Box_N>().rightBox.transform.position;
				entity.distanceToMove = entity.targetPosition - entity.currentBox.transform.position;
				entity.currentBox = entity.currentBox.GetComponent<Box_N>().rightBox;
				entity.currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
				entity.moving = true;
				boxAsigned = true;
				break;
			case 3:
				entity.targetPosition =  entity.currentBox.GetComponent<Box_N>().downBox.transform.position;
				entity.distanceToMove = entity.targetPosition - entity.currentBox.transform.position;
				entity.currentBox = entity.currentBox.GetComponent<Box_N>().downBox;
				entity.currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
				entity.moving = true;
				boxAsigned = true;
				break;
			case 4:
				entity.targetPosition =  entity.currentBox.GetComponent<Box_N>().leftBox.transform.position;
				entity.distanceToMove = entity.targetPosition - entity.currentBox.transform.position;
				entity.currentBox = entity.currentBox.GetComponent<Box_N>().leftBox;
				entity.currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
				entity.moving = true;
				boxAsigned = true;
				break;
			default:
				break;
			}
		}
		transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
	}
	public void SetNewDirection()
	{
		switch(entity.direction)
		{
			case 1:
			if(entity.currentBox.GetComponent<Box_N>().upBox)
			{
				entity.direction = 1;
				boxAsigned = true;
			}
			else if (entity.currentBox.GetComponent<Box_N>().downBox)
			{
				entity.direction = 3;
				boxAsigned = true;
			}
			break;
			case 2:
			if(entity.currentBox.GetComponent<Box_N>().rightBox)
			{
				entity.direction = 2;
				boxAsigned = true;
			}
			else if (entity.currentBox.GetComponent<Box_N>().leftBox)
			{
				entity.direction = 4;
				boxAsigned = true;
			}
			break;
			case 3:
			if(entity.currentBox.GetComponent<Box_N>().downBox)
			{
				entity.direction = 3;
				boxAsigned = true;
			}
			else if (entity.currentBox.GetComponent<Box_N>().upBox)
			{
				entity.direction = 1;
				boxAsigned = true;
			}
			break;
			case 4:
			if(entity.currentBox.GetComponent<Box_N>().leftBox)
			{
				entity.direction = 4;
				boxAsigned = true;
			}
			else if (entity.currentBox.GetComponent<Box_N>().rightBox)
			{
				entity.direction = 2;
				boxAsigned = true;
			}
			break;
			default:
			break;
		}
		transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
	}
public void CheckIfPlayer()
{
	if (entity.currentBox == playerEntity.currentBox)
		playerController.Kill ();
}
}
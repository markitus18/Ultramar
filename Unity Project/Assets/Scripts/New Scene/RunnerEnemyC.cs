using UnityEngine;
using System.Collections;

public class RunnerEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController_N playerController;
	Entity entity;
	bool directionAsigned;
	
	void Start()
	{
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController_N>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box_N>().enemies.Add(gameObject);
	}

	public void SetNewDirection()
	{
		switch(entity.direction)
		{
			case 1:
			if(entity.currentBox.GetComponent<Box_N>().upBox)
			{
				entity.direction = 1;
				directionAsigned = true;
			}
			else if (entity.currentBox.GetComponent<Box_N>().downBox)
			{
				entity.direction = 3;
				directionAsigned = true;
			}
			break;
			case 2:
			if(entity.currentBox.GetComponent<Box_N>().rightBox)
			{
				entity.direction = 2;
				directionAsigned = true;
			}
			else if (entity.currentBox.GetComponent<Box_N>().leftBox)
			{
				entity.direction = 4;
				directionAsigned = true;
			}
			break;
			case 3:
			if(entity.currentBox.GetComponent<Box_N>().downBox)
			{
				entity.direction = 3;
				directionAsigned = true;
			}
			else if (entity.currentBox.GetComponent<Box_N>().upBox)
			{
				entity.direction = 1;
				directionAsigned = true;
			}
			break;
			case 4:
			if(entity.currentBox.GetComponent<Box_N>().leftBox)
			{
				entity.direction = 4;
				directionAsigned = true;
			}
			else if (entity.currentBox.GetComponent<Box_N>().rightBox)
			{
				entity.direction = 2;
				directionAsigned = true;
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
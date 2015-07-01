using UnityEngine;
using System.Collections;

public class RunnerEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController_N playerController;
	Entity entity;
	
	void Start()
	{
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController_N>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box_N>().enemies.Add(gameObject);
	}
	public void SetNewBox ()
	{
		switch(entity.direction)
		{
		case 1:
			if(entity.currentBox.GetComponent<Box_N>().upBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().upBox;
			}
			else if (entity.currentBox.GetComponent<Box_N>().downBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().downBox;
				entity.direction = 3;
			}
			break;
		case 2:
			if(entity.currentBox.GetComponent<Box_N>().rightBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().rightBox;
			}
			else if (entity.currentBox.GetComponent<Box_N>().leftBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().leftBox;
				entity.direction = 4;
			}
			break;
		case 3:
			if(entity.currentBox.GetComponent<Box_N>().downBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().downBox;
			}
			else if (entity.currentBox.GetComponent<Box_N>().upBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().upBox;
				entity.direction = 1;
			}
			break;
		case 4:
			if(entity.currentBox.GetComponent<Box_N>().leftBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().leftBox;
			}
			else if (entity.currentBox.GetComponent<Box_N>().rightBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().rightBox;
				entity.direction = 2;
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
using UnityEngine;
using System.Collections;

public class StaticEnemyC : MonoBehaviour
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
			if(playerEntity.currentBox == entity.currentBox.GetComponent<Box_N>().upBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().upBox;
				entity.targetBox.GetComponent<Box_N>().enemies.Add(gameObject);
			}
			else
				entity.targetBox = entity.currentBox;
			
			break;
			
		case 2:
			if(playerEntity.currentBox == entity.currentBox.GetComponent<Box_N>().rightBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().rightBox;
				entity.targetBox.GetComponent<Box_N>().enemies.Add(gameObject);
			}
			else
				entity.targetBox = entity.currentBox;
			break;
			
		case 3:
			if(playerEntity.currentBox == entity.currentBox.GetComponent<Box_N>().downBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().downBox;
				entity.targetBox.GetComponent<Box_N>().enemies.Add(gameObject);
			}
			else
				entity.targetBox = entity.currentBox;
			break;
			
		case 4:
			if(playerEntity.currentBox == entity.currentBox.GetComponent<Box_N>().leftBox)
			{
				entity.targetBox = entity.currentBox.GetComponent<Box_N>().leftBox;
				entity.targetBox.GetComponent<Box_N>().enemies.Add(gameObject);
			}
			else
				entity.targetBox = entity.currentBox;
			break;
		default:
			break;
		}
	}

	public void CheckIfPlayer()
	{
		if (entity.currentBox == playerEntity.currentBox)
			playerController.Kill ();
	}
}
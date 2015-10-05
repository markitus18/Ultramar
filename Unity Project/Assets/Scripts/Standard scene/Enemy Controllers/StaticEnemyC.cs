using UnityEngine;
using System.Collections;

public class StaticEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController playerController;
	Entity entity;

	void Start()
	{
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		entity = gameObject.GetComponent<Entity>();
	}
	public void SetNewBox ()
	{
		switch(entity.direction)
		{
		case 1:
			if(playerEntity.currentBox == entity.currentBox.GetComponent<Box>().upBox)
			{
				entity.currentBox.GetComponent<Box>().enemies.Remove (gameObject);
				entity.targetBox = entity.currentBox.GetComponent<Box>().upBox;
				entity.targetBox.GetComponent<Box>().enemies.Add(gameObject);
			}
			else
				entity.targetBox = entity.currentBox;
			
			break;
			
		case 2:
			if(playerEntity.currentBox == entity.currentBox.GetComponent<Box>().rightBox)
			{
				entity.currentBox.GetComponent<Box>().enemies.Remove (gameObject);
				entity.targetBox = entity.currentBox.GetComponent<Box>().rightBox;
				entity.targetBox.GetComponent<Box>().enemies.Add(gameObject);
			}
			else
				entity.targetBox = entity.currentBox;
			break;
			
		case 3:
			if(playerEntity.currentBox == entity.currentBox.GetComponent<Box>().downBox)
			{
				entity.currentBox.GetComponent<Box>().enemies.Remove (gameObject);
				entity.targetBox = entity.currentBox.GetComponent<Box>().downBox;
				entity.targetBox.GetComponent<Box>().enemies.Add(gameObject);
			}
			else
				entity.targetBox = entity.currentBox;
			break;
			
		case 4:
			if(playerEntity.currentBox == entity.currentBox.GetComponent<Box>().leftBox)
			{
				entity.currentBox.GetComponent<Box>().enemies.Remove (gameObject);
				entity.targetBox = entity.currentBox.GetComponent<Box>().leftBox;
				entity.targetBox.GetComponent<Box>().enemies.Add(gameObject);
			}
			else
				entity.targetBox = entity.currentBox;
			break;
		default:
			break;
		}
	}

	public bool CheckIfPlayer()
	{
		if (entity.currentBox == playerEntity.currentBox)
		{
			playerController.Kill (enemiesEnum.staticEnemy);
			return true;
		}
		return false;
	}
}
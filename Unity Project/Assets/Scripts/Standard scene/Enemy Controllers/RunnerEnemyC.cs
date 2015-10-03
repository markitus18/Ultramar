using UnityEngine;
using System.Collections;

public class RunnerEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController playerController;
	Entity entity;
	
	void Start()
	{
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box>().enemies.Add(gameObject);
	}

	public void SetNewDirection()
	{
		switch(entity.direction)
		{
			case 1:
			if(entity.currentBox.GetComponent<Box>().upBox)
			{
				entity.direction = 1;
			}
			else if (entity.currentBox.GetComponent<Box>().downBox)
			{
				entity.direction = 3;
			}
			break;
			case 2:
			if(entity.currentBox.GetComponent<Box>().rightBox)
			{
				entity.direction = 2;
			}
			else if (entity.currentBox.GetComponent<Box>().leftBox)
			{
				entity.direction = 4;
			}
			break;
			case 3:
			if(entity.currentBox.GetComponent<Box>().downBox)
			{
				entity.direction = 3;
			}
			else if (entity.currentBox.GetComponent<Box>().upBox)
			{
				entity.direction = 1;
			}
			break;
			case 4:
			if(entity.currentBox.GetComponent<Box>().leftBox)
			{
				entity.direction = 4;
			}
			else if (entity.currentBox.GetComponent<Box>().rightBox)
			{
				entity.direction = 2;
			}
			break;
			default:
			break;
		}
		transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
	}
	public bool CheckIfPlayer()
	{
		if (entity.currentBox == playerEntity.currentBox)
		{
			playerController.Kill (enemiesEnum.runnerEnemy);
			return true;
		}
		return false;
	}
}
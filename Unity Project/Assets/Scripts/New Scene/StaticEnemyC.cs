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
		if (entity.ret == GameStateMachine_N.UpdateStates.UPDATE_KEEP)
		{
			switch(entity.direction)
			{
			case 1:
				if(playerEntity.currentBox == entity.currentBox.GetComponent<Box_N>().upBox)
				{
					entity.targetPosition =  entity.currentBox.GetComponent<Box_N>().upBox.transform.position;
					entity.distanceToMove = playerEntity.currentBox.transform.position - entity.currentBox.transform.position;
					entity.currentBox = playerEntity.currentBox;
					entity.currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
					entity.moving = true;
				}
				
				break;
				
			case 2:
				if(playerEntity.currentBox == entity.currentBox.GetComponent<Box_N>().rightBox)
				{
					entity.targetPosition =  entity.currentBox.GetComponent<Box_N>().rightBox.transform.position;
					entity.distanceToMove = playerEntity.currentBox.transform.position - entity.currentBox.transform.position;
					entity.currentBox = playerEntity.currentBox;
					entity.currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
					entity.moving = true;
				}
				break;
				
			case 3:
				if(playerEntity.currentBox == entity.currentBox.GetComponent<Box_N>().downBox)
				{
					entity.targetPosition =  entity.currentBox.GetComponent<Box_N>().downBox.transform.position;
					entity.distanceToMove = playerEntity.currentBox.transform.position - entity.currentBox.transform.position;
					entity.currentBox = playerEntity.currentBox;
					entity.currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
					entity.moving = true;
				}
				break;
				
			case 4:
				if(playerEntity.currentBox == entity.currentBox.GetComponent<Box_N>().leftBox)
				{
					entity.targetPosition =  entity.currentBox.GetComponent<Box_N>().leftBox.transform.position;
					entity.distanceToMove = playerEntity.currentBox.transform.position - entity.currentBox.transform.position;
					entity.currentBox = playerEntity.currentBox;
					entity.currentBox.GetComponent<Box_N>().enemies.Add (gameObject);
					entity.moving = true;
				}
				break;
			default:
				break;
			}
		}
	}
	public void CheckIfPlayer()
	{
		if (entity.currentBox == playerEntity.currentBox)
			playerController.Kill ();
	}
}
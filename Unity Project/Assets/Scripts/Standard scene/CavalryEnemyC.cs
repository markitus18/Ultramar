using UnityEngine;
using System.Collections;

public class CavalryEnemyC : MonoBehaviour {

	Entity playerEntity;
	PlayerController_N playerController;
	Entity entity;
	public MovementDirection movementDirection;
	bool directionAsigned;
	public enum MovementDirection
	{
		clockwise,
		counterclockwise
	}
	void Start()
	{
		directionAsigned = false;
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController_N>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box_N>().enemies.Add(gameObject);
	}

	public void SetNewDirection()
	{
		directionAsigned = false;
		while (!directionAsigned)
		{
			switch(entity.direction)
			{
			case 1:
				if(entity.currentBox.GetComponent<Box_N>().upBox)
				{
					entity.direction = 1;
					directionAsigned = true;
				}
				else
				{
					if (movementDirection == MovementDirection.clockwise)
					{
						if (entity.currentBox.GetComponent<Box_N>().rightBox)
						{
							entity.direction = 2;
						}
						else
						{
							movementDirection = MovementDirection.counterclockwise;
							entity.direction = 4;
						}
					}
					else if (movementDirection == MovementDirection.counterclockwise)
					{
						if (entity.currentBox.GetComponent<Box_N>().leftBox)
						{
							entity.direction = 4;
						}
						else
							movementDirection = MovementDirection.clockwise;
					}
				}
				break;
			case 2:
				if(entity.currentBox.GetComponent<Box_N>().rightBox)
				{
					entity.direction = 2;
					directionAsigned = true;
				}
				else
				{
					if (movementDirection == MovementDirection.clockwise)
					{
						if (entity.currentBox.GetComponent<Box_N>().downBox)
						{
							entity.direction = 3;
						}
						else
						{
							movementDirection = MovementDirection.counterclockwise;
							entity.direction = 1;
						}
					}
					else if (movementDirection == MovementDirection.counterclockwise)
					{
						if (entity.currentBox.GetComponent<Box_N>().upBox)
						{
							entity.direction = 1;
						}
						else
						{
							movementDirection = MovementDirection.clockwise;
							entity.direction = 3;
						}
					}
				}
				break;
			case 3:
				if(entity.currentBox.GetComponent<Box_N>().downBox)
				{
					entity.direction = 3;
					directionAsigned = true;
				}
				else
				{
					if (movementDirection == MovementDirection.clockwise)
					{
						if (entity.currentBox.GetComponent<Box_N>().leftBox)
						{
							entity.direction = 4;
						}
						else
						{
							movementDirection = MovementDirection.counterclockwise;
							entity.direction = 2;
						}
					}
					else if (movementDirection == MovementDirection.counterclockwise)
					{
						if (entity.currentBox.GetComponent<Box_N>().rightBox)
						{
							entity.direction = 2;
						}
						else
						{
							movementDirection = MovementDirection.clockwise;
							entity.direction = 4;
						}
					}
				}
				break;
			case 4:
				if(entity.currentBox.GetComponent<Box_N>().leftBox)
				{
					entity.direction = 4;
					directionAsigned = true;
				}
				else
				{
					if (movementDirection == MovementDirection.clockwise)
					{
						if (entity.currentBox.GetComponent<Box_N>().upBox)
						{
							entity.direction = 1;
						}
						else
						{
							movementDirection = MovementDirection.counterclockwise;
							entity.direction = 3;
						}
					}
					else if (movementDirection == MovementDirection.counterclockwise)
					{
						if (entity.currentBox.GetComponent<Box_N>().downBox)
						{
							entity.direction = 3;
						}
						else
						{
							movementDirection = MovementDirection.clockwise;
							entity.direction = 1;
						}
					}
				}
				break;
			default:
				break;
			}
		}
		transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
	}
	public void CheckIfPlayer()
	{
		if (entity.currentBox == playerEntity.currentBox)
			playerController.Kill ();
	}
}
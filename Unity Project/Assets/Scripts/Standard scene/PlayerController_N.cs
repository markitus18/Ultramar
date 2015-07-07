﻿using UnityEngine;
using System.Collections;

public class PlayerController_N : MonoBehaviour
{
	Entity entity;
	public GameObject loseText;
	public GameObject winText;public bool paused;
	GameStateMachine_N stateMachine;
	public bool passTurn;
	public bool autoPassTurn;
	// Use this for initialization

	void Awake()
	{
		entity = gameObject.GetComponent<Entity>();
	}
	void Start()
	{
		stateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine_N>();
		paused = false;
	}

	public void SetNewBox (GameObject newBox)
	{
		if (!entity.moving && !paused)
		{
			Debug.Log ("Checking new box");
			bool available = false;
			if (entity.currentBox.GetComponent<Box_N> ().upBox == newBox)
			{
				available = true;
				entity.direction = 1;
			}
			else if (entity.currentBox.GetComponent<Box_N> ().downBox == newBox)
			{
				available = true;
				entity.direction = 3;
			}
			else if (entity.currentBox.GetComponent<Box_N> ().rightBox == newBox)
			{
				available = true;
				entity.direction = 2;
			}
			else if (entity.currentBox.GetComponent<Box_N> ().leftBox == newBox)
			{
				available = true;
				entity.direction = 4;
			}
			if (available)
			{
				transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
				Debug.Log ("Assigned");
				entity.targetPosition = newBox.transform.position;
				entity.distanceToMove = entity.targetPosition - gameObject.transform.position;
				entity.targetBox = newBox;
				entity.targetBox.GetComponent<Box_N>().enemies.Add(gameObject);
				entity.currentBox.GetComponent<Box_N>().enemies.Remove (gameObject);
				entity.moving = true;
			}
		}
	}

	public void Kill()
	{
		Debug.Log("Killing player");
		loseText.GetComponent<Renderer>().enabled = true;
		paused = true;
	}

	public void CheckEnemy()
	{
		if (entity.currentBox.GetComponent<Box_N>().enemies.Count > 0)
		{
			int enemiesMax = entity.currentBox.GetComponent<Box_N>().enemies.Count;
			for (int i = 0; i < enemiesMax; i++)
			{
				if (entity.currentBox.GetComponent<Box_N>().enemies[i].tag != "Player")
				{
					if (entity.currentBox.GetComponent<Box_N>().enemies[i].GetComponent<ArcherEnemyC>())
					{
						entity.currentBox.GetComponent<Box_N>().enemies[i].GetComponent<ArcherEnemyC>().RemoveTargets();
					}
					Debug.Log("Killing enemy");
					entity.currentBox.GetComponent<Box_N>().enemies[i].SetActive(false);
					stateMachine.enemies.Remove(entity.currentBox.GetComponent<Box_N>().enemies[i]);
					entity.currentBox.GetComponent<Box_N>().enemies.Remove (entity.currentBox.GetComponent<Box_N>().enemies[i]);
					i--;
					enemiesMax --;
				}
			}
		}
	}

	public bool CheckEnd()
	{
		if (entity.currentBox.name == ("Box_end"))
		{
			Debug.Log("End of level");
			winText.GetComponent<Renderer>().enabled = true;
			paused = true;
			return true;
		}
		return false;

	}
	void OnMouseUp()
	{
		if (passTurn && stateMachine.state == GameStateMachine_N.GameStates.PLAYER_TURN)
			stateMachine.state = GameStateMachine_N.GameStates.ENEMY_START;
	}

}

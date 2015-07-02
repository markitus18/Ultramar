﻿using UnityEngine;
using System.Collections;

public class PlayerController_N : MonoBehaviour
{
	Entity entity;
	public GameObject loseText;
	public GameObject winText;public bool paused;
	GameStateMachine_N stateMachine;
	public bool passTurn;
	// Use this for initialization

	void Awake()
	{
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox = GameObject.Find ("Box_start");
	}
	void Start()
	{

		stateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine_N>();
		paused = false;

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void SetNewBox (GameObject newBox)
	{
		if (!entity.moving)
		{
			Debug.Log ("Checking new box");
			bool available = false;
			if (entity.currentBox.GetComponent<Box_N> ().upBox == newBox) {
				available = true;
				entity.direction = 1;
			} else if (entity.currentBox.GetComponent<Box_N> ().downBox == newBox) {
				available = true;
				entity.direction = 3;
			} else if (entity.currentBox.GetComponent<Box_N> ().rightBox == newBox) {
				available = true;
				entity.direction = 2;
			} else if (entity.currentBox.GetComponent<Box_N> ().leftBox == newBox) {
				available = true;
				entity.direction = 4;
			}
			if (available)
			{
				transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
				Debug.Log ("Assigned");
				entity.targetBox = newBox;
				entity.moving =  true;
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
		if (entity.targetBox.GetComponent<Box_N>().enemies.Count > 0)
		{
			int enemiesMax = entity.targetBox.GetComponent<Box_N>().enemies.Count;
			for (int i = 0; i < enemiesMax; i++)
			{
				if (entity.targetBox.GetComponent<Box_N>().enemies[i].GetComponent<ArcherEnemyC>())
				{
					entity.targetBox.GetComponent<Box_N>().enemies[i].GetComponent<ArcherEnemyC>().RemoveTargets();
				}
				Debug.Log("Killing enemy");
				entity.targetBox.GetComponent<Box_N>().enemies[i].SetActive(false);
				stateMachine.enemies.Remove(entity.targetBox.GetComponent<Box_N>().enemies[i]);
			}
		}
	}

	public bool CheckEnd()
	{
		if (entity.targetBox.name == ("Box_end"))
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
		if (passTurn)
			stateMachine.state = GameStateMachine_N.GameStates.ENEMY_TURN;
	}

}

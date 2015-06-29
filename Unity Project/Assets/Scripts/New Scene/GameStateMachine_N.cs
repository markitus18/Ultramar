using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateMachine_N : MonoBehaviour
{
	public enum GameStates
	{
		PLAYER_TURN,
		ENEMY_TURN
	}
	
	public enum UpdateStates
	{
		UPDATE_KEEP,
		UPDATE_NEXT,	
	}
	
	public Entity playerController;
	public List<GameObject> enemies;
	GameObject[] go;
	public GameStates state;
	
	void Start ()
	{
		playerController = GameObject.FindWithTag("Player").GetComponent<Entity>();
		state = GameStates.PLAYER_TURN;
		go = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in go)
		{
			enemies.Add(enemy);
		}
		
	}

	void Update ()
	{
		switch(state)
		{
		case GameStates.PLAYER_TURN:
			if (playerController.UpdateEntity() == UpdateStates.UPDATE_NEXT)
			{
				state = GameStates.ENEMY_TURN;
//				ResetEnemiesTurn();
				Debug.Log("Enemies turn");
			}
			break;
		case GameStates.ENEMY_TURN:
			UpdateEnemies ();
			break;
		default:
			break;
		}
	}
	
	void UpdateEnemies()
	{
		Debug.Log("Updating Enemies");
		int enemiesMax = enemies.Count;
		int enemiesUpdated = 0;
		for (int i = 0; i < enemiesMax; i++)
		{
			//Update enemy
			/*
			if (enemies[i].GetComponent<StaticEnemyController>())
				if (enemies[i].GetComponent<StaticEnemyController>().UpdateEnemy() == UpdateStates.UPDATE_NEXT)
					enemiesUpdated ++;
			if (enemies[i].GetComponent<RunnerEnemyController>())
				if (enemies[i].GetComponent<RunnerEnemyController>().UpdateEnemy() == UpdateStates.UPDATE_NEXT)
					enemiesUpdated ++;
			Debug.Log("Updated: " + enemiesUpdated);
			Debug.Log("Max: " + enemiesMax);
			*/
		}
		if (enemiesUpdated == enemiesMax)
		{
			Debug.Log("Enemies max: " + enemiesMax);
			state = GameStates.PLAYER_TURN;
			Debug.Log("Players turn");
		}
	}
	void ResetEnemiesTurn()
	{
		int enemiesMax = enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			/*
			if (enemies[i].GetComponent<StaticEnemyController>())
				enemies[i].GetComponent<StaticEnemyController>().ret = UpdateStates.UPDATE_KEEP;
			if (enemies[i].GetComponent<RunnerEnemyController>())
				enemies[i].GetComponent<RunnerEnemyController>().ret = UpdateStates.UPDATE_KEEP;
			*/
		}
	}
}
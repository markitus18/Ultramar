using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateMachine_N : MonoBehaviour
{
	public enum GameStates
	{
		PLAYER_TURN,
		ENEMY_TURN,
		END,
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
	bool start = true;

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
			UpdatePlayer();
			break;
		case GameStates.ENEMY_TURN:
			UpdateEnemies ();
			break;
		default:
			break;
		}
	}

	void UpdatePlayer()
	{
		if (!playerController.gameObject.GetComponent<PlayerController_N>().paused)
		{
			if (playerController.UpdateEntity() == UpdateStates.UPDATE_NEXT)
			{
				state = GameStates.ENEMY_TURN;
				playerController.gameObject.GetComponent<PlayerController_N>().CheckEnemy();
				if (playerController.gameObject.GetComponent<PlayerController_N>().CheckEnd())
					state = GameStates.END;
				Debug.Log("Enemies turn");
			}
		}
	}
	
	void UpdateEnemies()
	{
		if (start)
		{
			ResetEnemiesTurn();
			SetEnemiesBox ();
			start = false;
		}
		int enemiesMax = enemies.Count;
		int enemiesUpdated = 0;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<StaticEnemyC>())
				enemies[i].GetComponent<StaticEnemyC>().UpdateMoving();
			if (enemies[i].GetComponent<Entity>().ret == UpdateStates.UPDATE_KEEP)
			{
				enemies[i].GetComponent<Entity>().Move();
			}
			else
				enemiesUpdated ++;

		}
		if (enemiesUpdated == enemiesMax)
		{
			EndEnemiesTurn();

			Debug.Log("Enemies max: " + enemiesMax);
			state = GameStates.PLAYER_TURN;
			start = true;
			Debug.Log("Players turn");
		}
	}
	void ResetEnemiesTurn()
	{
		int enemiesMax = enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			enemies[i].GetComponent<Entity>().ret = UpdateStates.UPDATE_KEEP;
		}
	}

	void EndEnemiesTurn()
	{
		int enemiesMax = gameObject.GetComponent<GameStateMachine_N>().enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<StaticEnemyC>())
				enemies[i].GetComponent<StaticEnemyC>().CheckIfPlayer();
			if (enemies[i].GetComponent<RunnerEnemyC>())
				enemies[i].GetComponent<RunnerEnemyC>().CheckIfPlayer ();
			if (enemies[i].GetComponent<ArcherEnemyC>())
			    enemies[i].GetComponent<ArcherEnemyC>().CheckPlayer();
			if (enemies[i].GetComponent<CavalryEnemyC>())
				enemies[i].GetComponent<CavalryEnemyC>().CheckIfPlayer();
		}
		SetDirection();
	}
	void SetEnemiesBox()
	{
		Debug.Log("Setting enemies boxes");
		int enemiesMax = gameObject.GetComponent<GameStateMachine_N>().enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<StaticEnemyC>())
				enemies[i].GetComponent<StaticEnemyC>().SetNewBox();
			if (enemies[i].GetComponent<RunnerEnemyC>() || enemies[i].GetComponent<CavalryEnemyC>())
			    enemies[i].GetComponent<Entity>().SetNewBox();
				
		}
	}

	void SetDirection()
	{
		int enemiesMax = gameObject.GetComponent<GameStateMachine_N>().enemies.Count;
		for (int i = 0; i < enemiesMax; i ++)
		{
			if (enemies[i].GetComponent<RunnerEnemyC>())
				enemies[i].GetComponent<RunnerEnemyC>().SetNewDirection();
			if (enemies[i].GetComponent<CavalryEnemyC>())
				enemies[i].GetComponent<CavalryEnemyC>().SetNewDirection();
		}
	}
}
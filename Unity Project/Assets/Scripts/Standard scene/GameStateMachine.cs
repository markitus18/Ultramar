using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateMachine : MonoBehaviour
{
	public enum GameStates
	{
		PLAYER_TURN,
		ENEMY_START,
		ENEMY_MOVE,
		ENEMY_END,
		END,
	}
	
	public enum UpdateStates
	{
		UPDATE_KEEP,
		UPDATEEXT,
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
			UpdatePlayer();
			break;
		case GameStates.ENEMY_START:
			StartEnemiesTurn ();
			break;
		case GameStates.ENEMY_MOVE:
			MoveEnemies ();
			break;
		case GameStates.ENEMY_END:
			EndEnemiesTurn ();
			break;
		default:
			break;
		}
	}

	void UpdatePlayer()
	{
		if (playerController.gameObject.GetComponent<PlayerController>().autoPassTurn)
		{
			state = GameStates.ENEMY_START;
		}
		else if (!playerController.gameObject.GetComponent<PlayerController>().paused)
		{
			if (playerController.UpdateEntity() == UpdateStates.UPDATEEXT)
			{
				state = GameStates.ENEMY_START;
				playerController.gameObject.GetComponent<PlayerController>().CheckEnemy();
				if (playerController.gameObject.GetComponent<PlayerController>().CheckEnd())
					state = GameStates.END;
				Debug.Log("Enemies turn");
			}
		}
	}

	void StartEnemiesTurn()
	{
		ResetEnemiesTurn ();
		SetEnemiesBox ();
		SetEnemiesPositions();
		state = GameStates.ENEMY_MOVE;
	}

	void ResetEnemiesTurn()
	{
		int enemiesMax = enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			enemies[i].GetComponent<Entity>().ret = UpdateStates.UPDATE_KEEP;
			enemies[i].GetComponent<Entity>().targetAssigned = false;
		}
	}

	void SetEnemiesBox()
	{
		Debug.Log("Setting enemies boxes");
		int enemiesMax = gameObject.GetComponent<GameStateMachine>().enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<StaticEnemyC>())
				enemies[i].GetComponent<StaticEnemyC>().SetNewBox();
			if (enemies[i].GetComponent<RunnerEnemyC>() || enemies[i].GetComponent<CavalryEnemyC>())
				enemies[i].GetComponent<Entity>().SetNewBox();	
		}
	}

	void SetEnemiesPositions()
	{
		int enemiesMax = gameObject.GetComponent<GameStateMachine>().enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			enemies[i].GetComponent<Entity>().SetNewPosition ();
		}
	}

	void MoveEnemies()
	{
		int enemiesMax = enemies.Count;
		int enemiesUpdated = 0;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<Entity>().ret == UpdateStates.UPDATE_KEEP)
			{
				enemies[i].GetComponent<Entity>().Move();
			}
			else
				enemiesUpdated ++;

		}
		if (enemiesUpdated == enemiesMax)
		{
			state = GameStates.ENEMY_END;
			Debug.Log("Enemies max: " + enemiesMax);
		}
	}


	void EndEnemiesTurn()
	{
		int enemiesMax = gameObject.GetComponent<GameStateMachine>().enemies.Count;
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
		state = GameStates.PLAYER_TURN;
	}
	
	void SetDirection()
	{
		int enemiesMax = gameObject.GetComponent<GameStateMachine>().enemies.Count;
		for (int i = 0; i < enemiesMax; i ++)
		{
			if (enemies[i].GetComponent<RunnerEnemyC>())
				enemies[i].GetComponent<RunnerEnemyC>().SetNewDirection();
			if (enemies[i].GetComponent<CavalryEnemyC>())
				enemies[i].GetComponent<CavalryEnemyC>().SetNewDirection();
		}
	}
}
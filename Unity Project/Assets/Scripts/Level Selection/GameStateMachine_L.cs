using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateMachine_L : MonoBehaviour
{
	public enum GameStates_L
	{
		PLAYER_TURN,
		ENEMY_START,
		ENEMY_MOVE,
		ENEMY_END,
		END,
	}
	
	public enum UpdateStates_L
	{
		UPDATE_KEEP,
		UPDATE_NEXT,
	}
	
	public Entity playerController;
	public List<GameObject> enemies;
	public GameStates_L state; 
	public bool unlockLevels;
	public List<GameObject> planes;
	int currentLevel;

	void Start ()
	{
		playerController = GameObject.FindWithTag("Player").GetComponent<Entity>();
		state = GameStates_L.PLAYER_TURN;
	}

	void Update ()
	{
		switch(state)
		{
		case GameStates_L.PLAYER_TURN:
			UpdatePlayer();
			break;
		case GameStates_L.ENEMY_START:
			StartEnemiesTurn ();
			break;
		default:
			break;
		}
		if (unlockLevels)
			GameControl.control.unlockedLevel = 5;
	}

	void UpdatePlayer()
	{
		if (playerController.gameObject.GetComponent<PlayerController>().autoPassTurn)
		{
			state = GameStates_L.ENEMY_START;
		}
		else if (!playerController.gameObject.GetComponent<PlayerController>().paused)
		{
			if (playerController.UpdateEntity() == GameStateMachine.UpdateStates.UPDATENEXT)
			{
				state = GameStates_L.ENEMY_START;
				playerController.gameObject.GetComponent<PlayerController>().CheckEnemy();
				if (playerController.gameObject.GetComponent<PlayerController>().CheckEnd())
					state = GameStates_L.END;
				Debug.Log("Enemies turn");
			}
		}
	}

	void StartEnemiesTurn()
	{

		state = GameStates_L.PLAYER_TURN;
	}
	/*
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
		int enemiesMax = gameObject.GetComponent<GameStateMachine_L_L>().enemies.Count;
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
		int enemiesMax = gameObject.GetComponent<GameStateMachine_L>().enemies.Count;
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
			if (CheckPlayerKill ())
				state = GameStates.END;
			else
				state = GameStates.ENEMY_END;
		}
	}

	bool CheckPlayerKill()
	{
		bool ret = false;
		int enemiesMax = gameObject.GetComponent<GameStateMachine_L>().enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<StaticEnemyC>())
				if (enemies[i].GetComponent<StaticEnemyC>().CheckIfPlayer()) ret = true;
			if (enemies[i].GetComponent<RunnerEnemyC>())
				if (enemies[i].GetComponent<RunnerEnemyC>().CheckIfPlayer ()) ret = true;
			if (enemies[i].GetComponent<ArcherEnemyC>())
				if (enemies[i].GetComponent<ArcherEnemyC>().CheckPlayer()) ret = true;
			if (enemies[i].GetComponent<CavalryEnemyC>())
				if (enemies[i].GetComponent<CavalryEnemyC>().CheckIfPlayer()) ret = true;
		}
		return ret;
	}
	void EndEnemiesTurn()
	{

		SetDirection();
		state = GameStates.PLAYER_TURN;
	}
	
	void SetDirection()
	{
		int enemiesMax = gameObject.GetComponent<GameStateMachine_L>().enemies.Count;
		for (int i = 0; i < enemiesMax; i ++)
		{
			if (enemies[i].GetComponent<RunnerEnemyC>())
				enemies[i].GetComponent<RunnerEnemyC>().SetNewDirection();
			if (enemies[i].GetComponent<CavalryEnemyC>())
				enemies[i].GetComponent<CavalryEnemyC>().SetNewDirection();
		}
	}
	*/
}
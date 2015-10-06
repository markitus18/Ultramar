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
		MENU_WIN,
		MENU_LOOSE,
        RESET	
	}
	
	public enum UpdateStates
	{
		UPDATE_KEEP,
		UPDATENEXT,
	}
	
	public Entity playerController;
    public PlayerController playerScript;
	public List<GameObject> enemies;
	public PanelManager panelManager;
	GameObject[] go;
	public GameStates state;
	float delayTime;
	public bool FullGame = false;
	public int level;
	void Start ()
	{
		playerController = GameObject.FindWithTag("Player").GetComponent<Entity>();
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //state = GameStates.PLAYER_TURN;
        state = GameStates.RESET;
        go = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in go)
		{
			enemies.Add(enemy);
		}	
	}

	void Update ()
	{
        switch (state)
        {
            case GameStates.PLAYER_TURN:
                UpdatePlayer();
                break;
            case GameStates.ENEMY_START:
                StartEnemiesTurn();
                break;
            case GameStates.ENEMY_MOVE:
                MoveEnemies();
                break;
            case GameStates.ENEMY_END:
                EndEnemiesTurn();
                break;
            case GameStates.MENU_WIN:
		{
			Debug.Log("Menu Win stage");
                LoadWinMenu();
		}
			break;
            case GameStates.MENU_LOOSE:
		{
			Debug.Log("Menu Loose stage");
               	LoadLooseMenu();
		}
                break;
            case GameStates.RESET:
				ResetGame();
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
			if (playerController.UpdateEntity() == UpdateStates.UPDATENEXT)
			{
				state = GameStates.ENEMY_START;
				playerController.gameObject.GetComponent<PlayerController>().CheckEnemy();
                playerController.gameObject.GetComponent<PlayerController>().CheckEnd();
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
			if (enemies[i].GetComponent<Entity>().active)
			{
				enemies[i].GetComponent<Entity>().ret = UpdateStates.UPDATE_KEEP;
				enemies[i].GetComponent<Entity>().targetAssigned = false;
			}
		}
	}

	void SetEnemiesBox()
	{
		Debug.Log("Setting enemies boxes");
		int enemiesMax = gameObject.GetComponent<GameStateMachine>().enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<Entity>().active)
			{
				if (enemies[i].GetComponent<StaticEnemyC>())
					enemies[i].GetComponent<StaticEnemyC>().SetNewBox();
				if (enemies[i].GetComponent<RunnerEnemyC>() || enemies[i].GetComponent<CavalryEnemyC>())
					enemies[i].GetComponent<Entity>().SetNewBox();	
			}
		}
	}

	void SetEnemiesPositions()
	{
		int enemiesMax = gameObject.GetComponent<GameStateMachine>().enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<Entity>().active)
				enemies[i].GetComponent<Entity>().SetNewPosition ();
		}
	}

	void MoveEnemies()
	{
		int enemiesMax = enemies.Count;
		int enemiesUpdated = 0;
		for (int i = 0; i < enemiesMax; i++)
		{
			if ((enemies[i].GetComponent<Entity>().ret == UpdateStates.UPDATE_KEEP) && (enemies[i].GetComponent<Entity>().active == true))
			{
				enemies[i].GetComponent<Entity>().Move();
			}
			else
				enemiesUpdated ++;

		}
		if (enemiesUpdated == enemiesMax)
		{
			if (CheckPlayerKill ())
			{
				delayTime = Time.time;
			}
			else
				state = GameStates.ENEMY_END;
		}
	}

	bool CheckPlayerKill()
	{
		bool ret = false;
		int enemiesMax = gameObject.GetComponent<GameStateMachine>().enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<Entity>().active)
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
		int enemiesMax = enemies.Count;
		for (int i = 0; i < enemiesMax; i ++)
		{
			if (enemies[i].GetComponent<Entity>().active)
			{
				if (enemies[i].GetComponent<RunnerEnemyC>())
					enemies[i].GetComponent<RunnerEnemyC>().SetNewDirection();
				if (enemies[i].GetComponent<CavalryEnemyC>())
					enemies[i].GetComponent<CavalryEnemyC>().SetNewDirection();
			}
		}
	}

	void LoadWinMenu()
	{
		panelManager.OpenWinPanel();
    }

	void LoadLooseMenu()
	{
		panelManager.OpenLoosePanel();
	}

	void ResetGame()
	{
		Debug.Log("Reseting Game");
		if (Time.time >= delayTime + 1)
		{
			ResetBoxes ();
			ResetEnemies();
			ResetPlayer ();
			state = GameStates.PLAYER_TURN;
		}
	}

	void ResetBoxes()
	{
		go = GameObject.FindGameObjectsWithTag("Box");
		foreach (GameObject box in go)
		{
			box.GetComponent<Box>().enemies.Clear();
		}	
	}

	void ResetPlayer()
	{
		playerScript.ended = false;
		playerScript.dead = false;
		playerScript.looseChanged = false;
		playerScript.winChanged = false;
		playerScript.paused = false;
		playerController.GetComponent<Entity>().Reset();
	}

	void ResetEnemies()
	{
		int enemiesMax = enemies.Count;
		for (int i = 0; i < enemiesMax; i++)
		{
			enemies[i].SetActive(true);
			enemies[i].GetComponent<Entity>().Reset();
		}
		for (int i = 0; i < enemiesMax; i++)
		{
			if (enemies[i].GetComponent<ArcherEnemyC>())
			{
				enemies[i].GetComponent<ArcherEnemyC>().CheckPlayer();
			}
		}
	}
		
	}
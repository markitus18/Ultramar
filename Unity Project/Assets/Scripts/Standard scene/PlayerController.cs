using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	Entity entity;
	public GameObject loseText;
	public GameObject winText;public bool paused;
	GameStateMachine stateMachine;
	public bool passTurn;
	public bool autoPassTurn;
	Vector2 touchStartPos;
	bool touching;
	// Use this for initialization

	void Awake()
	{
		entity = gameObject.GetComponent<Entity>();
	}
	void Start()
	{
		stateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine>();
		paused = false;
	}

	public void OnTouchDown ()
	{
		if (Input.touchCount == 1) 
		{
			touching = true;
			touchStartPos.x = Input.touches [0].position.x;
			touchStartPos.y = Input.touches [0].position.y;
		}
		#if UNITY_EDITOR
		
		touching = true;
		touchStartPos.x = Input.mousePosition.x;
		touchStartPos.y = Input.mousePosition.y;
		
		# endif
	}

	public void OnTouchUp ()
	{
		touching = false;
	}

	public void OnTouchExit()
	{
		if (touching == true && Input.touchCount == 1)
		{
			touching = false;
			float deltaY = Input.touches[0].position.y - touchStartPos.y;
			float deltaX = Input.touches[0].position.x - touchStartPos.x;
			if (Mathf.Abs (deltaX) > 3 || Mathf.Abs (deltaY) > 3)
			{
				if (Mathf.Abs (deltaX) > Mathf.Abs(deltaY))
				{
					if (deltaX < 0)
					{
						SetNewBox(entity.currentBox.GetComponent<Box>().leftBox);
					}
					else
					{
						SetNewBox(entity.currentBox.GetComponent<Box>().rightBox);
					}
				}
				else
				{
					if (deltaY < 0)
					{
						SetNewBox(entity.currentBox.GetComponent<Box>().downBox);
					}
					else
					{
						SetNewBox(entity.currentBox.GetComponent<Box>().upBox);
					}
				}
			}
		}
#if UNITY_EDITOR
		if (touching == true)
		{
			touching = false;
			float deltaY = Input.mousePosition.y - touchStartPos.y;
			float deltaX = Input.mousePosition.x - touchStartPos.x;
			if (Mathf.Abs (deltaX) > 3 || Mathf.Abs (deltaY) > 3)
			{
				if (Mathf.Abs (deltaX) > Mathf.Abs(deltaY))
				{
					if (deltaX < 0)
					{
						SetNewBox(entity.currentBox.GetComponent<Box>().leftBox);
					}
					else
					{
						SetNewBox(entity.currentBox.GetComponent<Box>().rightBox);
					}
				}
				else
				{
					if (deltaY < 0)
					{
						SetNewBox(entity.currentBox.GetComponent<Box>().downBox);
					}
					else
					{
						SetNewBox(entity.currentBox.GetComponent<Box>().upBox);
					}
				}
			}
		}


# endif
	}

	public void SetNewBox (GameObject newBox)
	{
		if (!entity.moving && !paused)
		{
			Debug.Log ("Checking new box");
			bool available = false;
			if (entity.currentBox.GetComponent<Box> ().upBox == newBox)
			{
				available = true;
				entity.direction = 1;
			}
			else if (entity.currentBox.GetComponent<Box> ().downBox == newBox)
			{
				available = true;
				entity.direction = 3;
			}
			else if (entity.currentBox.GetComponent<Box> ().rightBox == newBox)
			{
				available = true;
				entity.direction = 2;
			}
			else if (entity.currentBox.GetComponent<Box> ().leftBox == newBox)
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
				entity.targetBox.GetComponent<Box>().enemies.Add(gameObject);
				entity.currentBox.GetComponent<Box>().enemies.Remove (gameObject);
				entity.moving = true;
			}
		}
	}

	public void Kill()
	{
		Debug.Log("Killing player");
		loseText.GetComponent<Renderer>().enabled = true;
		paused = true;
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void CheckEnemy()
	{
		if (entity.currentBox.GetComponent<Box>().enemies.Count > 0)
		{
			int enemiesMax = entity.currentBox.GetComponent<Box>().enemies.Count;
			for (int i = 0; i < enemiesMax; i++)
			{
				if (entity.currentBox.GetComponent<Box>().enemies[i].tag != "Player")
				{
					if (entity.currentBox.GetComponent<Box>().enemies[i].GetComponent<ArcherEnemyC>())
					{
						entity.currentBox.GetComponent<Box>().enemies[i].GetComponent<ArcherEnemyC>().RemoveTargets();
					}
					Debug.Log("Killing enemy");
					entity.currentBox.GetComponent<Box>().enemies[i].SetActive(false);
					stateMachine.enemies.Remove(entity.currentBox.GetComponent<Box>().enemies[i]);
					entity.currentBox.GetComponent<Box>().enemies.Remove (entity.currentBox.GetComponent<Box>().enemies[i]);
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
			stateMachine.state = GameStateMachine.GameStates.END;
			return true;
		}
		return false;

	}
	/*void OnTouchUp()
	{
		if (passTurn && stateMachine.state == GameStateMachine.GameStates.PLAYER_TURN)
			stateMachine.state = GameStateMachine.GameStates.ENEMY_START;
	}*/

}

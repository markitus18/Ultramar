using UnityEngine;
using System.Collections;

public enum enemiesEnum
{
    staticEnemy,
    runnerEnemy,
    archerEnemy,
    cavalryEnemy
}

public class PlayerController : MonoBehaviour
{
	Entity entity;
    public bool paused;
	GameStateMachine stateMachine;
    public CameraOrbit CameraScript;
	public bool passTurn;
	public bool autoPassTurn;
	Vector2 touchStartPos;
	public bool touching;
    public bool lockCam;
    public bool ended;
    public bool dead;
	public bool winChanged = false;
	public bool looseChanged = false;
	public int currentLevel;
	public LevelSelectionManager levelSelectionManager;
    int direction;
    int directionVariation;
    // Use this for initialization

    public AudioSource swordKill;
    public AudioSource arrowKill;
    public AudioSource KillSound;
	

    void Awake()
	{
		entity = gameObject.GetComponent<Entity>();

    }
	void Start()
	{
        dead = false;
        ended = false;
		stateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine>();
		if (!CameraScript)
     	   CameraScript = GameObject.Find("Main Camera").GetComponent<CameraOrbit>();
		if (Application.loadedLevel == 12 || Application.loadedLevel == 14)
			StartLevelSelection ();
        paused = false;
	}

	void StartLevelSelection()
	{
		currentLevel = GameControl.control.currentSelectionLevel;
		int levelToStart = GameControl.control.currentSelectionLevel;
		entity.currentBox = entity.startingBox = levelSelectionManager.GetBox (levelToStart);
		entity.currentPosition = entity.currentBox.transform.position;
		if (GameControl.control.levelSelectionMovement)
		{
			entity.targetBox = levelSelectionManager.GetBox (levelToStart + 1);
			entity.moving = true;
			entity.targetPosition = entity.targetBox.transform.position;
			entity.distanceToMove = entity.targetPosition - gameObject.transform.position;

		}

	}

	void Update ()
	{
        //Setting the variation amount for the player controller drag
        if (CameraScript.startingY > 45 && CameraScript.startingY < 135) { directionVariation = 1; }
        else if (CameraScript.startingY > 135 && CameraScript.startingY < 225) { directionVariation = 2; }
        else if (CameraScript.startingY > 225 && CameraScript.startingY < 315) { directionVariation = 3; }
        else { directionVariation = 0; }


        if (dead == true)
        {
            if (!arrowKill.isPlaying && !swordKill.isPlaying && !looseChanged)
            {
                stateMachine.state = GameStateMachine.GameStates.MENU_LOOSE;
				looseChanged = true;
            }
        }
        if (Input.touchCount== 0 && Input.touchSupported)
		{
			touching = false;
            lockCam = false;
        }

        if(ended == true && !winChanged)
        {
            stateMachine.state = GameStateMachine.GameStates.MENU_WIN; 
			winChanged = true;
        }

#if UNITY_EDITOR

		if (Input.GetMouseButtonUp(0))
		{
			touching = false;
            lockCam = false;
        }
        if (CameraScript.movingCamera == false)
        {
            if (Input.GetKeyUp("up") || Input.GetKeyUp("down") || Input.GetKeyUp("left") || Input.GetKeyUp("right"))
            {
                if (Input.GetKeyUp("up"))
                {
                    direction = 1 + directionVariation;
                }
                if (Input.GetKeyUp("down"))
                {
                    direction = 3 + directionVariation;
                }
                if (Input.GetKeyUp("left"))
                {
                    direction = 4 + directionVariation;
                }

                if (Input.GetKeyUp("right"))
                {
                    direction = 2 + directionVariation;
                }

                if (direction > 4)
                { direction -= 4; }
                if (direction == 1)
                    SetNewBox(entity.currentBox.GetComponent<Box>().upBox);
                if (direction == 2)
                    SetNewBox(entity.currentBox.GetComponent<Box>().rightBox);
                if (direction == 3)
                    SetNewBox(entity.currentBox.GetComponent<Box>().downBox);
                if (direction == 4)
                    SetNewBox(entity.currentBox.GetComponent<Box>().leftBox);
            }
        }
#endif
    }
	
	public void OnTouchDown ()
	{
        if (CameraScript.startingY > 45 && CameraScript.startingY < 135) { directionVariation = -1; }
        else if (CameraScript.startingY < 225) { directionVariation = -2; }
        else if (CameraScript.startingY < 315) { directionVariation = -3; }
        else { directionVariation = 0; }
		if (Input.touchCount == 1) 
		{
			touching = true;
            lockCam = true;
            touchStartPos.x = Input.touches [0].position.x;
			touchStartPos.y = Input.touches [0].position.y;
		}
#if UNITY_EDITOR
        lockCam = true;
        touching = true;
		touchStartPos.x = Input.mousePosition.x;
		touchStartPos.y = Input.mousePosition.y;
		
# endif
	}

	public void OnTouchUp ()
	{
        lockCam = false;
        touching = false;
#if UNITY_EDITOR
		float deltaY = Input.mousePosition.y - touchStartPos.y;
		float deltaX = Input.mousePosition.x - touchStartPos.x;
		if (deltaX < 5 && deltaY < 5 && deltaX > -5 && deltaY > -5)
		{
			if (passTurn && stateMachine.state == GameStateMachine.GameStates.PLAYER_TURN)
				stateMachine.state = GameStateMachine.GameStates.ENEMY_START;
		}
#endif
	}

	public void OnTouchExit()
	{
		if (touching == true && Input.touchCount == 1 && CameraScript.movingCamera == false)
		{
			float deltaY = Input.touches[0].position.y - touchStartPos.y;
			float deltaX = Input.touches[0].position.x - touchStartPos.x;
			if (Mathf.Abs (deltaX) > 3 || Mathf.Abs (deltaY) > 3)
			{
                if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
                {
                    if (deltaX < 0)
                    {
                        direction = 4;
                    }
                    else
                    {
                        direction = 2;
                    }
                }
                else
                {
                    if (deltaY < 0)
                    {
                        direction = 3;
                    }
                    else
                    {
                        direction = 1;
                    }
                }
            }
		}
#if UNITY_EDITOR
		if (touching == true && CameraScript.movingCamera == false)
		{
			float deltaY = Input.mousePosition.y - touchStartPos.y;
			float deltaX = Input.mousePosition.x - touchStartPos.x;
			if (Mathf.Abs (deltaX) > 3 || Mathf.Abs (deltaY) > 3)
			{
				if (Mathf.Abs (deltaX) > Mathf.Abs(deltaY))
				{
					if (deltaX < 0)
					{
                        direction = 4;
					}
					else
					{
                        direction = 2;
					}
				}
				else
				{
					if (deltaY < 0)
					{
						direction = 3;
					}
					else
					{
						direction = 1;
					}
				}
			}
		}


#endif
        if (CameraScript.movingCamera == false && touching == true)
        {
            direction += directionVariation;
            if (direction > 4)
            { direction -= 4; }
            if (direction == 1)
                SetNewBox(entity.currentBox.GetComponent<Box>().upBox);
            if (direction == 2)
                SetNewBox(entity.currentBox.GetComponent<Box>().rightBox);
            if (direction == 3)
                SetNewBox(entity.currentBox.GetComponent<Box>().downBox);
            if (direction == 4)
                SetNewBox(entity.currentBox.GetComponent<Box>().leftBox);
            touching = false;
        }
    }

	public void SetNewBox (GameObject newBox)
	{
		bool available = false;
		//Outside the level selection scenes
		if (Application.loadedLevel != 12 && Application.loadedLevel != 14)
		{
	        if (!entity.moving && !paused && newBox)
			{
				Debug.Log ("Checking new box");
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
			}
		}
		//In level selection scenes
		else
		{
			if (!entity.moving && !paused && newBox)
			{
				Debug.Log ("Checking new box -- Level Selection");
				if (entity.currentBox.GetComponent<Box> ().upBox == newBox && newBox.GetComponent<Box>().LevelToLoad <= GameControl.control.unlockedLevel)
				{
					available = true;
					entity.direction = 1;
				}
				else if (entity.currentBox.GetComponent<Box> ().downBox == newBox && newBox.GetComponent<Box>().LevelToLoad <= GameControl.control.unlockedLevel)
				{
					available = true;
					entity.direction = 3;
				}
				else if (entity.currentBox.GetComponent<Box> ().rightBox == newBox && newBox.GetComponent<Box>().LevelToLoad <= GameControl.control.unlockedLevel)
				{
					available = true;
					entity.direction = 2;
				}
				else if (entity.currentBox.GetComponent<Box> ().leftBox == newBox && newBox.GetComponent<Box>().LevelToLoad <= GameControl.control.unlockedLevel)
				{
					available = true;
					entity.direction = 4;
				}
			}
		}
		if (available)
		{
			if (Application.loadedLevel == 12 || Application.loadedLevel == 14)
			{
				currentLevel = newBox.GetComponent<Box>().LevelToLoad;
			}
			//transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
			Debug.Log ("Assigned");
			entity.targetPosition = newBox.transform.position;
			entity.distanceToMove = entity.targetPosition - gameObject.transform.position;
			entity.targetBox = newBox;
			entity.targetBox.GetComponent<Box>().enemies.Add(gameObject);
			entity.currentBox.GetComponent<Box>().enemies.Remove (gameObject);
			entity.moving = true;
		}
	}



	public void Kill(enemiesEnum enemyWhoKilled)
	{
        if (dead == false)
        {
            Debug.Log("Killing player");
            if (enemyWhoKilled == enemiesEnum.archerEnemy)
            {
                arrowKill.Play();
            }
            else
            {
                swordKill.Play();
            }
            dead = true;
            paused = true;
        }
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
					Debug.Log("Killing enemy");
                    KillSound.Play();
                    entity.currentBox.GetComponent<Box>().enemies[i].GetComponent<Entity>().active = false;
					entity.currentBox.GetComponent<Box>().enemies[i].SetActive(false);
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
			GetComponent<AudioSource>().Play();
			paused = true;
            ended = true;
			return true;
		}
		return false;

	}

}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	Entity entity;
	public GameObject loseText;
	public GameObject winText;
    public bool paused;
	GameStateMachine stateMachine;
    CameraOrbit CameraScript;
	public bool passTurn;
	public bool autoPassTurn;
	Vector2 touchStartPos;
	public bool touching;
    public bool lockCam;
    public bool ended;

    int direction;
    int directionVariation;
	// Use this for initialization

	void Awake()
	{
		entity = gameObject.GetComponent<Entity>();
	}
	void Start()
	{
        ended = false;
		stateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine>();
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraOrbit>();

        paused = false;
       
    }

	void Update ()
	{
        //Setting the variation amount for the player controller drag
        if (CameraScript.startingY > 45 && CameraScript.startingY < 135) { directionVariation = 1; }
        else if (CameraScript.startingY > 135 && CameraScript.startingY < 225) { directionVariation = 2; }
        else if (CameraScript.startingY > 225 && CameraScript.startingY < 315) { directionVariation = 3; }
        else { directionVariation = 0; }

        if (Input.touchCount== 0 && Input.touchSupported)
		{
			touching = false;
            lockCam = false;
        }

        if(ended == true && GetComponent<AudioSource>().isPlaying == false)
        {
            stateMachine.state = GameStateMachine.GameStates.END_WIN; 
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
    
        if (!entity.moving && !paused && newBox)
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
		//loseText.GetComponent<Renderer>().enabled = true;
		stateMachine.state = GameStateMachine.GameStates.END_LOOSE;
		paused = true;
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
			GetComponent<AudioSource>().Play();
			paused = true;
            ended = true;
			return true;
		}
		return false;

	}

}

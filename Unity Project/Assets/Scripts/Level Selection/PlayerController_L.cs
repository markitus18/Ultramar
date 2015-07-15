using UnityEngine;
using System.Collections;

public class PlayerController_L : MonoBehaviour
{
	Entity_L entity;
	public GameObject loseText;
	public GameObject winText;
	public bool paused;
	GameStateMachine stateMachine;
	public bool passTurn;
	public bool autoPassTurn;
	// Use this for initialization

	void Awake()
	{
		entity = gameObject.GetComponent<Entity_L>();
	}
	void Start()
	{
		stateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine>();
		paused = false;
	}

	public void SetNewBox (GameObject newBox)
	{
		if (!entity.moving && !paused)
		{
			Debug.Log ("Checking new box");
			bool available = false;
			if (entity.currentBox.GetComponent<Box_L> ().upBox == newBox)
			{
				available = true;
				entity.direction = 1;
			}
			else if (entity.currentBox.GetComponent<Box_L> ().downBox == newBox)
			{
				available = true;
				entity.direction = 3;
			}
			else if (entity.currentBox.GetComponent<Box_L> ().rightBox == newBox)
			{
				available = true;
				entity.direction = 2;
			}
			else if (entity.currentBox.GetComponent<Box_L> ().leftBox == newBox)
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
				entity.targetBox.GetComponent<Box_L>().enemies.Add(gameObject);
				entity.targetBox.GetComponent<Box_L>().OnBoxEnter();
				entity.currentBox.GetComponent<Box_L>().enemies.Remove (gameObject);
				entity.currentBox.GetComponent<Box_L>().OnBoxExit();
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
}

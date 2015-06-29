using UnityEngine;
using System.Collections;

public class PlayerController_N : MonoBehaviour
{
	Entity entity;
	public GameObject loseText;
	public bool paused;
	GameStateMachine_N stateMachine;
	// Use this for initialization
	void Start()
	{
		entity = gameObject.GetComponent<Entity>();
		stateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine_N>();
		paused = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void SetNewBox (GameObject newBox)
	{
		Debug.Log ("Checking new box");
		Debug.Log (newBox);
		Debug.Log (entity.currentBox.GetComponent<Box_N>().upBox);
		bool available = false;
		if (entity.currentBox.GetComponent<Box_N>().upBox == newBox)
			available = true;
		else if (entity.currentBox.GetComponent<Box_N>().downBox == newBox)
			available = true;
		else if (entity.currentBox.GetComponent<Box_N>().rightBox == newBox)
			available = true;
		else if (entity.currentBox.GetComponent<Box_N>().leftBox == newBox)
			available = true;
		if (available)
		{
			Debug.Log ("Assigned");
			entity.targetBox = newBox;
			entity.moving =  true;
			CheckEnemy();
		}
	}

	public void Kill()
	{
		Debug.Log("Killing player");
		loseText.GetComponent<Renderer>().enabled = true;
		paused = true;
	}

	void CheckEnemy()
	{
		if (entity.targetBox.GetComponent<Box_N>().enemies.Count > 0)
		{
			int enemiesMax = entity.targetBox.GetComponent<Box_N>().enemies.Count;
			for (int i = 0; i < enemiesMax; i++)
			{
				Debug.Log("Killing enemy");
				stateMachine.enemies[i].SetActive(false);
				stateMachine.enemies.Remove(stateMachine.enemies[i]);
			}
		}
	}

}

using UnityEngine;
using System.Collections;

public class PlayerController_N : MonoBehaviour
{
	Entity entity;
	public GameObject loseText;
	public GameObject winText;public bool paused;
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
		bool available = false;
		if (entity.currentBox.GetComponent<Box_N> ().upBox == newBox) {
			available = true;
			entity.direction = 1;
		} else if (entity.currentBox.GetComponent<Box_N> ().downBox == newBox) {
			available = true;
			entity.direction = 3;
		} else if (entity.currentBox.GetComponent<Box_N> ().rightBox == newBox) {
			available = true;
			entity.direction = 2;
		} else if (entity.currentBox.GetComponent<Box_N> ().leftBox == newBox) {
			available = true;
			entity.direction = 4;
		}
		if (available)
		{
			transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
			Debug.Log ("Assigned");
			entity.targetBox = newBox;
			entity.moving =  true;
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
		if (entity.targetBox.GetComponent<Box_N>().enemies.Count > 0)
		{
			int enemiesMax = entity.targetBox.GetComponent<Box_N>().enemies.Count;
			for (int i = 0; i < enemiesMax; i++)
			{
				Debug.Log("Killing enemy");
				entity.targetBox.GetComponent<Box_N>().enemies[i].SetActive(false);
				stateMachine.enemies.Remove(entity.targetBox.GetComponent<Box_N>().enemies[i]);
			}
		}
	}

	public bool CheckEnd()
	{
		if (entity.targetBox.name == ("Box_end"))
		{
			Debug.Log("End of level");
			winText.GetComponent<Renderer>().enabled = true;
			paused = true;
			return true;
		}
		return false;

	}

}

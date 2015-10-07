using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour
{

	public PlayerController playerController;
	public int levelToLoad;

	void Start ()
	{
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		gameObject.GetComponent<Renderer>().enabled = false;
		if (playerController.currentLevel == levelToLoad)
			gameObject.GetComponent<Renderer>().enabled = true;
	}
	
	void Update ()
	{
		if (levelToLoad == 11)
		{
			if (playerController.currentLevel == 1)
			{
				gameObject.GetComponent<Renderer>().enabled = true;
				gameObject.GetComponent<Collider>().enabled = true;
			}
			else
			{
				gameObject.GetComponent<Renderer>().enabled = false;
				gameObject.GetComponent<Collider>().enabled = false;
			}
		}
		else if (levelToLoad == 13)
		{
			if (playerController.currentLevel == 6)
			{
				gameObject.GetComponent<Renderer>().enabled = true;
				gameObject.GetComponent<Collider>().enabled = true;
			}
			else
			{
				gameObject.GetComponent<Renderer>().enabled = false;
				gameObject.GetComponent<Collider>().enabled = false;
			}
		}
		else if (playerController.currentLevel == levelToLoad)
		{
			gameObject.GetComponent<Renderer>().enabled = true;
			gameObject.GetComponent<Collider>().enabled = true;
		}
		else
		{
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<Collider>().enabled = false;
		}
	}

	void OnTouchDown()
	{
		Application.LoadLevel (levelToLoad);
	}
}

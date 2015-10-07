using UnityEngine;
using System.Collections;

public class StandartIntro : MonoBehaviour
{
	public int levelToLoad;

	public Material locked;
	public Material unlocked;
	public Material current;

	void Update()
	{
		if (GameControl.control.gameFinished)
		{
			gameObject.GetComponent<Renderer>().material = unlocked;
		}
		else if(GameControl.control.SecondMissionUnlocked)
		{
			if (levelToLoad == 11)
				gameObject.GetComponent<Renderer>().material = unlocked;
			else
				gameObject.GetComponent<Renderer>().material = current;
		}

		else
		{
			if (levelToLoad == 11)
				gameObject.GetComponent<Renderer>().material = current;
			else
				gameObject.GetComponent<Renderer>().material = locked;
		}
	}

	void OnTouchDown()
	{
		if (levelToLoad == 11)
			Application.LoadLevel (11);
		else if (GameControl.control.SecondMissionUnlocked)
			Application.LoadLevel (13);
	}
}

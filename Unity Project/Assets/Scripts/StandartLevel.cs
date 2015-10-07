using UnityEngine;
using System.Collections;

public class StandartLevel : MonoBehaviour
{
	public int levelToLoad;

	public Material locked;
	public Material unlocked;
	public Material current;

	void Update()
	{
		if(levelToLoad == GameControl.control.unlockedLevel)
		{
			gameObject.GetComponent<Renderer>().material = current;
		}

		else if(levelToLoad < GameControl.control.unlockedLevel)
		{
			gameObject.GetComponent<Renderer>().material = unlocked;
		}

		else if(levelToLoad > GameControl.control.unlockedLevel)
		{
			gameObject.GetComponent<Renderer>().material = locked;
		}
	}
}

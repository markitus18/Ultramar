using UnityEngine;
using System.Collections;

public class LevelNumber : MonoBehaviour
{
	public int levelToLoad;

	void OnTouchUp()
	{
		Application.LoadLevel (levelToLoad);
	}
}

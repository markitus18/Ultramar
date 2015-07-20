using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour
{
	public int levelToLoad;
	
	void OnTouchUp()
	{
		Application.LoadLevel (levelToLoad);
	}
}

using UnityEngine;
using System.Collections;

public class StandartLevel : MonoBehaviour
{
	public PanelManager panelManager;
	public int levelToLoad;

	void OnTouchUp()
	{
		if (!panelManager.playButton.activeSelf)
			Application.LoadLevel (levelToLoad);
	}
}

using UnityEngine;
using System.Collections;

public class StandartLevel : MonoBehaviour
{
	public PanelManager panelManager;
	public int levelToLoad;

	void OnTouchUp()
	{
		if (panelManager.playButton && !panelManager.playButton.activeSelf)
			Application.LoadLevel (levelToLoad);
        else if (!panelManager.playButton)
            Application.LoadLevel(levelToLoad);
	}
}

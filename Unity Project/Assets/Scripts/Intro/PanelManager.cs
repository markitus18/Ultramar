using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {
	
	public GameObject panel;
	public GameObject musicButton;
	bool panelOpened = false;

	public void OpenPanel()
	{
		musicButton.SetActive (true);
		panelOpened = true;
	}

	public void ClosePanel()
	{
		musicButton.SetActive (false);
		panelOpened = false;
	}

	public void DeployedClicked()
	{
		if (!panelOpened)
			OpenPanel();
		else
			ClosePanel ();
	}
	public void UpdateVolume()
	{

		if (AudioListener.volume == 1)
		{
			AudioListener.volume = 0;
		}
		else
		{
			AudioListener.volume = 1;
		}
	}

	public void Play()
	{
		if (GameControl.control.firstTime)
			Application.LoadLevel (1);
		else
			Application.LoadLevel (2);

	}

}

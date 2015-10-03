using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

	public GameObject musicButton;
	public GameObject replayButton;
	GameStateMachine gameStateMachine;
	bool panelOpened = false;

	void Start()
	{
		gameStateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine>();
	}
	public void OpenPanel()
	{
		musicButton.SetActive (true);
		if (replayButton)
			replayButton.SetActive (true);
		panelOpened = true;
	}

	public void ClosePanel()
	{
		musicButton.SetActive (false);
		if (replayButton)
			replayButton.SetActive (false);
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

	public void Reset()
	{
		gameStateMachine.state = GameStateMachine.GameStates.RESET;
	}

}

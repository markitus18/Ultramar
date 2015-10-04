using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

	public GameObject musicButton;
	public GameObject replayButton;
	public GameObject backButton;
	GameStateMachine gameStateMachine;
	bool panelOpened = false;

	void Start()
	{
		if (GameObject.Find("Game Manager"))
		gameStateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine>();
	}
	public void OpenPanel()
	{
		musicButton.SetActive (true);
		if (replayButton)
			replayButton.SetActive (true);
		if (backButton)
			backButton.SetActive (true);
		panelOpened = true;
	}

	public void ClosePanel()
	{
		musicButton.SetActive (false);
		if (replayButton)
			replayButton.SetActive (false);
		if (backButton)
			backButton.SetActive (false);
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
			Application.LoadLevel (2);
		else
			Application.LoadLevel (1);

	}

	public void Reset()
	{
		gameStateMachine.state = GameStateMachine.GameStates.RESET;
	}

	public void Back()
	{
		Application.LoadLevel ("Level Selection");
	}

}

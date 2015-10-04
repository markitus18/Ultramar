using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

	public GameObject musicButton;
	public GameObject replayButton;
	public GameObject backButton;
	public GameObject winPanel;
	public GameObject loosePanel;
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
		CloseLoosePanel();
		CloseWinPanel();
		gameStateMachine.state = GameStateMachine.GameStates.RESET;
	}

	public void Back()
	{
		Application.LoadLevel ("Level Selection");
	}

	public void OpenWinPanel()
	{
		if (!winPanel.activeSelf)
		{
			winPanel.SetActive(true);
		}
	}

	public void OpenLoosePanel()
	{
		if (!loosePanel.activeSelf)
		{
			loosePanel.SetActive(true);
		}
	}

	public void CloseWinPanel()
	{
		if (winPanel.activeSelf)
		{
			winPanel.SetActive(false);
		}
	}

	public void CloseLoosePanel()
	{
		if (loosePanel.activeSelf)
		{
			loosePanel.SetActive(false);
		}
	}

	public void NextLevel()
	{
		Application.LoadLevel(LevelToLoad ()); //here is where level selection will go;
	}

	public int LevelToLoad()
	{
		int currentLevel = Application.loadedLevel;
		if (currentLevel > 2 && currentLevel < 7)
			return 1;
		if (currentLevel > 10)
			return 8;
	}

}

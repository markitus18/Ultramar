using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

    public GameObject deployButton;
	public GameObject playButton;
	public GameObject musicButton;
	public GameObject musicButton2;
	public GameObject replayButton;
	public GameObject backButton;
	public GameObject winPanel;
	public GameObject loosePanel;
	public GameObject title;
	public GameStateMachine gameStateMachine;
	bool panelOpened = false;

	void Start()
	{
		if (GameObject.Find("Game Manager"))
		gameStateMachine = GameObject.Find("Game Manager").GetComponent<GameStateMachine>();
        Vector3 pos;
        pos.x = Screen.width - 75;
        pos.y = Screen.height - 75;
        pos.z = 0;
        deployButton.transform.position = pos;
        pos.x -= 100;
        if (musicButton)
        {
            musicButton.transform.position = pos;
        }
        if (musicButton2)
        {
            musicButton2.transform.position = pos;
            pos.x -= 100;
        }
        if (replayButton)
        {
            replayButton.transform.position = pos;
            pos.x -= 100;
        }
        if (backButton)
        {
            backButton.transform.position = pos;
        }

    }
	public void OpenPanel()
	{
		if (AudioListener.volume == 1)
			musicButton.SetActive (true);
		if (AudioListener.volume == 2)
			musicButton2.SetActive (true);
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
			musicButton2.SetActive (true);
			musicButton.SetActive (false);
		}
		else
		{
			AudioListener.volume = 1;
			musicButton.SetActive (true);
			musicButton2.SetActive (false);
		}

	}

	public void Play()
	{
		if (GameControl.control.firstKinematic)
			Application.LoadLevel (11);
		else
		{
			title.SetActive (false);
			playButton.SetActive (false);
			/*
			if (GameControl.control.unlockedLevel < 6)
				Application.LoadLevel (1);
			else
				Application.LoadLevel(8);
			*/
		}

	}

	public void Reset()
	{
		gameStateMachine.state = GameStateMachine.GameStates.RESET;
		Debug.Log (gameStateMachine.state);
		Debug.Log("Reset button pressed");
		CloseLoosePanel();
		CloseWinPanel();
        ClosePanel();
		Debug.Log("Changing state to reset");
	}

	public void Back()
	{
		if (Application.loadedLevel == 12 || Application.loadedLevel == 14)
			Application.LoadLevel (0);
		else if (Application.loadedLevel < 6)
			Application.LoadLevel ("Level Selection");
		else if (Application.loadedLevel >= 6)
			Application.LoadLevel ("Level Selection2");
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
		int levelsUnlocked = GameControl.control.unlockedLevel;
		int currentLevel = Application.loadedLevel;

		if (currentLevel == 10)
		{
			return 15;
		}
		if (currentLevel >= levelsUnlocked)
		{
			GameControl.control.unlockedLevel = levelsUnlocked = currentLevel + 1;
			if (currentLevel != 5)
			{
				GameControl.control.levelSelectionMovement = true;
				GameControl.control.currentSelectionLevel = currentLevel;
			}
			else
			{
				GameControl.control.currentSelectionLevel = currentLevel++;
				return 0;
			}
		}
		else
			GameControl.control.currentSelectionLevel = currentLevel;
		if (currentLevel <= 5)
			return 12;
		if (currentLevel > 5)
		    return 14;
		return 0;

	}



}

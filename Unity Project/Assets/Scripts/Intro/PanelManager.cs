using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

    public GameObject deployButton;
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
        Vector3 pos;
        pos.x = Screen.width - 75;
        pos.y = Screen.height - 75;
        pos.z = 0;
        deployButton.transform.position = pos;
        pos.x -= 100;
		if (musicButton)
      	  musicButton.transform.position = pos;
        pos.x -= 100;
		if (replayButton)
        	replayButton.transform.position = pos;
        pos.x -= 100;
		if (backButton)
        	backButton.transform.position = pos;

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
		if (GameControl.control.firstKinematic)
			Application.LoadLevel (2);
		else
		{
			if (GameControl.control.unlockedLevel < 6)
				Application.LoadLevel (1);
			else
				Application.LoadLevel(8);
		}

	}

	public void Reset()
	{
		CloseLoosePanel();
		CloseWinPanel();
        ClosePanel();
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
		int levelsUnlocked = GameControl.control.unlockedLevel;
		int currentLevel = Application.loadedLevel;

		if (currentLevel == 10)
			return 15;

		if (currentLevel == levelsUnlocked)
		{
			GameControl.control.unlockedLevel = ++levelsUnlocked;
			if (currentLevel == 5 && GameControl.control.secondKinematic)
					return 13;
			else
				GameControl.control.levelSelectionMovement = true;
		}
			
			GameControl.control.currentSelectionLevel = currentLevel;
			if (currentLevel != 5)
			{
				if (currentLevel < 5)
					return 12;
				else
					return 14;
			}
			else
			{
				if (GameControl.control.secondKinematic)
					return 13;
				else
					return 12;
			}
		return 0;
	}



}

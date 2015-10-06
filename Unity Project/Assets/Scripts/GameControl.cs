using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{
	public static GameControl control;

	public int unlockedLevel = 1;
	public bool firstKinematic = true;
	public bool secondKinematic = true;
	public int currentSelectionLevel;
	public bool levelSelectionMovement = false;
	public bool gameFinished = false;
	public bool introZoom = true;

	void Awake ()
	{
		if (control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this)
		{
			Destroy(gameObject);
		}
		Load ();
	}

	void OnGUI()
	{
		if(GUI.Button (new Rect(10, 40, 100, 30), "Reset Game"))
			ResetGame ();
		GUI.Label (new Rect(10, 80, 120, 50), "Unlocked levels: " + unlockedLevel.ToString ());
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/gameData.dat");

		GameData data = new GameData();
		data.unlockedLevel = unlockedLevel;
		data.firstKinematic = firstKinematic;
		data.secondKinematic = secondKinematic;
		data.gameFinished = gameFinished;
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/gameData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/gameData.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();

			unlockedLevel = data.unlockedLevel;
			firstKinematic = data.firstKinematic;
			secondKinematic = data.secondKinematic;
			gameFinished = data.gameFinished;
		}
	}

	public void ResetGame()
	{
		if (File.Exists (Application.persistentDataPath + "/gameData.dat"))
			File.Delete (Application.persistentDataPath + "/gameData.dat");
		firstKinematic = true;
		secondKinematic = true;
		unlockedLevel = 1;
		gameFinished = false;
        if (Application.loadedLevel != 0)
		    Application.LoadLevel (0);
	}

	[Serializable]
	class GameData
	{
		public int unlockedLevel;
		public bool firstKinematic;
		public bool secondKinematic;
		public bool gameFinished;
	}
}

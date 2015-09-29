using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{
	public static GameControl control;

	public int unlockedLevel = 1;

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
	/*
	void OnGUI()
	{
		if(GUI.Button (new Rect(10, 10, 100, 30), "Save Game"))
			Save ();
		if(GUI.Button (new Rect(10, 40, 100, 30), "Reset Game"))
			Reset ();
		GUI.Label (new Rect(10, 80, 120, 50), "Unlocked levels: " + unlockedLevel.ToString ());
	}
	*/
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/gameData.dat");

		GameData data = new GameData();
		data.unlockedLevel = unlockedLevel;

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
		}
	}

	public void Reset()
	{
		if (File.Exists (Application.persistentDataPath + "/gameData.dat"))
			File.Delete (Application.persistentDataPath + "/gameData.dat");
		unlockedLevel = 1;
		Application.LoadLevel (0);
	}

	[Serializable]
	class GameData
	{
		public int unlockedLevel;
	}
}

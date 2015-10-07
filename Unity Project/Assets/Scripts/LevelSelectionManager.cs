using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectionManager : MonoBehaviour {

	// Use this for initialization
	public List<GameObject> boxes;

	void Start ()
	{
		GameObject[] go;
		go = GameObject.FindGameObjectsWithTag("Box");
		foreach (GameObject box in go)
		{
			boxes.Add(box);
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject GetBox(int level)
	{
		for(int i = 0; i < boxes.Count; i++)
		{
			if (boxes[i].GetComponent<Box>().LevelToLoad == level)
				return boxes[i];
		}
		return null;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectionManager : MonoBehaviour {

	// Use this for initialization
	public List<GameObject> boxes;
	void Awake()
	{
		GameObject[] go;
		go = GameObject.FindGameObjectsWithTag("Box");
		foreach (GameObject box in go)
		{
			boxes.Add(box);
		}	
	}
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject GetBox(int level)
	{
		int c = boxes.Count;
		Debug.Log (c);
		for(int i = 0; i < c; i++)
		{
			if (boxes[i].GetComponent<Box>().LevelToLoad == level)
				return boxes[i];
		}
		return null;
	}
}

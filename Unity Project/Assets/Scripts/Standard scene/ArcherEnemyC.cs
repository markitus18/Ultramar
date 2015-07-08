using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController playerController;
	Entity entity;

	public Material targetMaterial;
	public List<GameObject> targetBoxes;
	void Awake()
	{
		for (int i = 0; i < targetBoxes.Count; i++)
		{
			targetBoxes[i].GetComponent<Renderer>().material = targetMaterial;
		}
	}
	void Start()
	{
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box>().enemies.Add(gameObject);
	}
	public void CheckPlayer()
	{
		for (int i = 0; i < targetBoxes.Count; i++)
		{
			if (targetBoxes[i] == playerEntity.currentBox)
			{
				playerController.Kill();
				Vector3 vector = targetBoxes[i].transform.position - entity.currentBox.transform.position;
				if (vector.z != 0)
				{
					if (vector.z < 0)
						entity.direction = 3;
					else
						entity.direction = 1;
				}
				else if (vector.x != 0)
				{
					if (vector.x < 0)
						entity.direction = 4;
					else
						entity.direction = 2;
				}
			Debug.Log ("moving direction");
			entity.transform.eulerAngles = new Vector3(0, 90 * (entity.direction - 1), 0);
			}
		}
	}

	public void RemoveTargets()
	{
		for (int i = 0; i < targetBoxes.Count; i++)
		{
			targetBoxes[i].GetComponent<Renderer>().material = targetBoxes[i].GetComponent<Box>().standardMaterial;
			targetBoxes[i].GetComponent<Box>().originalMaterial = targetBoxes[i].GetComponent<Box>().standardMaterial;
		}
	}
}

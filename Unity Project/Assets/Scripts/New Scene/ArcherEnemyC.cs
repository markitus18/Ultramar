using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController_N playerController;
	Entity entity;

	public List<GameObject> targetBoxes;
	void Start()
	{
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController_N>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box_N>().enemies.Add(gameObject);
	}
	public void CheckPlayer()
	{
		for (int i = 0; i < targetBoxes.Count; i++)
		{
			if (targetBoxes[i] == playerEntity.currentBox)
				playerController.Kill();
		}
	}
}

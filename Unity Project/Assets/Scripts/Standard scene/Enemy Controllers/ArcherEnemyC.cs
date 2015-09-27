﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController playerController;
	Entity entity;

	public int archerRange;

	private Ray shootRay;

	void Awake()
	{
		entity = gameObject.GetComponent<Entity>();
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity> ();
		playerController = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
	}
	void Start()
	{
		shootRay = new Ray(transform.position, transform.forward * archerRange);
		/*
		switch (entity.startingDirection)
		{
		case(Entity.enumDirections.up):
			shootRay = new Ray(transform.position, Vector3.forward * archerRange);
			break;
		case(Entity.enumDirections.down):
			shootRay = new Ray(transform.position, Vector3.back * archerRange);
			break;
		case(Entity.enumDirections.right):
			shootRay = new Ray(transform.position, new Vector3(1, 0, 0) * archerRange);
			break;
		case(Entity.enumDirections.left):
			shootRay = new Ray(transform.position, Vector3.left * archerRange);
			break;
		}
		*/
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box>().enemies.Add(gameObject);

	}
	public bool CheckPlayer()
	{
		bool ret = false;
		RaycastHit hit;
		Debug.DrawRay(transform.position, transform.forward * archerRange, Color.red, 1.0f);
		if (Physics.Raycast(shootRay, out hit, archerRange))
		{

			if(hit.collider.tag == "Player")
			{
				Debug.Log ("Player Hit");
		//		playerController.Kill();
		//		ret = true;
			}
		}
		else
			Debug.DrawRay(transform.position, transform.forward * archerRange, Color.green, 1.0f);
		
		return ret;
	}
}


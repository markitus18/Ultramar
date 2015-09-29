using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherEnemyC : MonoBehaviour
{
	Entity playerEntity;
	PlayerController playerController;
	Entity entity;
	public LineRenderer redLineRenderer;
	public LineRenderer greenLineRenderer;
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
		playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity>();
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box>().enemies.Add(gameObject);
		CheckPlayer();
	}
	public bool CheckPlayer()
	{
		bool ret = false;
		RaycastHit hit;
		Debug.DrawRay(transform.position, transform.forward * archerRange, Color.red, 1.0f);
		if (Physics.Raycast(shootRay, out hit, archerRange))
		{
			redLineRenderer.SetPosition(0, gameObject.transform.position + new Vector3 (0, 1.8f, 0));
			redLineRenderer.SetPosition(1, hit.point + new Vector3(0, 1.8f, 0));
			if(hit.collider.tag == "Player")
			{
				Debug.Log ("Player Hit");
				playerController.Kill();
				ret = true;
			}
			else if(hit.collider.tag == "Enemy")
			{
				Debug.Log ("Enemy Hit");
				greenLineRenderer.SetPosition(0, hit.point + new Vector3 (0, 1.8f, 0));
				greenLineRenderer.SetPosition(1, hit.point + transform.forward * (archerRange - (hit.point.x - transform.position.x)) + new Vector3(0, 1.8f, 0));
			}
		}
		else
		{
			redLineRenderer.SetPosition(0, gameObject.transform.position + new Vector3 (0, 1.8f, 0));
			redLineRenderer.SetPosition(1, gameObject.transform.position + transform.forward * archerRange + new Vector3 (0, 1.8f, 0));
		}


		
		return ret;
	}
}


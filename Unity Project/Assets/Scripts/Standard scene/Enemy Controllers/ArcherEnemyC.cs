using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherEnemyC : MonoBehaviour
{
	//Entity playerEntity;
	PlayerController playerController;
	Entity entity;
	Vector3 vectorHeight = new Vector3 (0, 1.8f, 0);
	public LineRenderer redLineRenderer;
	//public LineRenderer greenLineRenderer;
	public int archerRange;

	private Ray shootRay;

	void Awake()
	{
		entity = gameObject.GetComponent<Entity>();
		//playerEntity = GameObject.FindWithTag ("Player").GetComponent<Entity> ();
		playerController = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
	}
	void Start()
	{
		shootRay = new Ray(transform.position, transform.forward * archerRange + vectorHeight);
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		entity = gameObject.GetComponent<Entity>();
		entity.currentBox.GetComponent<Box>().enemies.Add(gameObject);
		CheckPlayer();
	}
	public bool CheckPlayer()
	{
		bool ret = false;
		RaycastHit hit;
		if (Physics.Raycast(shootRay, out hit, archerRange))
		{
			redLineRenderer.SetPosition(0, gameObject.transform.position + vectorHeight);
			redLineRenderer.SetPosition(1, hit.point + vectorHeight - new Vector3(0, hit.point.y, 0));
			if(hit.collider.tag == "Player")
			{
				Debug.Log ("Player Hit");
				playerController.Kill(enemiesEnum.archerEnemy);
				ret = true;
			}
			else if(hit.collider.tag == "Enemy")
			{
				Debug.Log ("Enemy Hit");
				//greenLineRenderer.SetPosition(0, hit.point + vectorHeight - new Vector3 (0, hit.point.y, 0));
				//greenLineRenderer.SetPosition(1, new Vector3(0, -hit.point.y, 0) + hit.point + transform.forward * (archerRange - ((hit.point.x - transform.position.x) + (hit.point.z - transform.position.z))) + vectorHeight);
			}
		}
		else
		{
			redLineRenderer.SetPosition(0, gameObject.transform.position + vectorHeight);
			redLineRenderer.SetPosition(1, gameObject.transform.position + transform.forward * archerRange + vectorHeight);
		}


		
		return ret;
	}
}


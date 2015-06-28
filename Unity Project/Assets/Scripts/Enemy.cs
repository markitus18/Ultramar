using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public PlayerController playerController;
	public GameObject currentBox;


	[HideInInspector]public Vector3 currentPosition;
	[HideInInspector]public Vector3 targetPosition;

	[HideInInspector]public bool isMoving;
	public int direction;

	public GameStateMachine.UpdateStates ret;
	// Use this for initialization
	/*
	void Start ()
	{
	
	}
	*/
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void Start()
	{
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		targetPosition = currentPosition = currentBox.transform.position;
		transform.position = currentPosition;
		transform.eulerAngles = new Vector3(0, 90 * (direction - 1), 0);
	}

	public virtual GameStateMachine.UpdateStates UpdateEnemy()
	{
		return GameStateMachine.UpdateStates.UPDATE_KEEP;
	}
}

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour 
{

	public LayerMask touchInputMask;

	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;

	// Update is called once per frame
	void Update ()
	{
#if UNITY_EDITOR
		if (Input.touchCount < 1)
        { 
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) )
		{
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				RaycastHit hit;
				
				if (Physics.Raycast (ray, out hit, touchInputMask))
				{
					GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);
					
				if (Input.GetMouseButtonDown(0))
					{
						recipient.SendMessage ("OnTouchDown", SendMessageOptions.DontRequireReceiver);
					}
				if (Input.GetMouseButtonUp(0))
					{
						recipient.SendMessage ("OnTouchUp", SendMessageOptions.DontRequireReceiver);
					}
				if (Input.GetMouseButton(0))
					{
						recipient.SendMessage ("OnTouchStay", SendMessageOptions.DontRequireReceiver);
					}
					
				}
			
			foreach(GameObject g in touchesOld)
			{
				if (!touchList.Contains(g))
				  {
					g.SendMessage ("OnTouchExit", SendMessageOptions.DontRequireReceiver);
				  }
			
			}
		}
		}
#endif
		if (Input.touchCount > 0)
		{
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

			foreach (Touch touch in Input.touches)
			{

				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, touchInputMask))
				{
					GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);

					if (touch.phase == TouchPhase.Began)
					{
						recipient.SendMessage ("OnTouchDown", SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Ended)
					{
						recipient.SendMessage ("OnTouchUp", SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
					{
						recipient.SendMessage ("OnTouchStay", SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Canceled)
					{
						recipient.SendMessage ("OnTouchExit", SendMessageOptions.DontRequireReceiver);
					}
				}
			}

			foreach(GameObject g in touchesOld)
			{
				if (!touchList.Contains(g))
				    {
						g.SendMessage ("OnTouchExit", SendMessageOptions.DontRequireReceiver);
					}
			}
		}
	}
}

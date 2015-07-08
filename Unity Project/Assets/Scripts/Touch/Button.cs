using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

	public Color defaultColor;
	public Color selectedColor;
	private Material mat;

	void Start ()
	{
		mat = transform.GetComponent<Renderer>().material;
	}

	void OnTouchDown ()
	{
		mat.color = selectedColor;
	}
	void OnTouchUp ()
	{
		mat.color = defaultColor;	
	}
	void OnTouchStay ()
	{
	}
	void OnTouchExit ()
	{
		mat.color = defaultColor;	
	}

}

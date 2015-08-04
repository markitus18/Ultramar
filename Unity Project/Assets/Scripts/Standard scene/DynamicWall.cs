using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicWall : MonoBehaviour {

	public enum DirectionEnum
	{
		up = 1,
		right,
		down,
		left,
	}
	public enum Type
	{
		Destroyable = 1,
		Blockable
	}

	public Type wallType;
	public List<GameObject> BoxList;
	public List<DirectionEnum> DirectionList;
	public bool activable;

	void Start ()
	{
		activable = true;
	}

	void Update ()
	{

	}

	public bool UpHit(GameObject boxToAdd)
	{
		if (!DirectionList.Contains(DirectionEnum.up))
		{
			BoxList.Add (boxToAdd);
			DirectionList.Add (DirectionEnum.up);
			return true;
		}
		return false;
	}

	public bool DownHit(GameObject boxToAdd)
	{
		if (!DirectionList.Contains (DirectionEnum.down))
		{
			BoxList.Add (boxToAdd);
			DirectionList.Add (DirectionEnum.down);
			return true;
		}
		return false;
	}

	public bool RightHit(GameObject boxToAdd)
	{
		if (!DirectionList.Contains (DirectionEnum.right))
		{
			BoxList.Add (boxToAdd);
			DirectionList.Add (DirectionEnum.right);
			return true;
		}
		return false;
	}

	public bool LeftHit(GameObject boxToAdd)
	{
		if (!DirectionList.Contains (DirectionEnum.left))
		{
			BoxList.Add (boxToAdd);
			DirectionList.Add (DirectionEnum.left);
			return true;
		}
		return false;
	}

	public bool Change ()
	{
		if (wallType == Type.Destroyable)
		{
			if (DirectionList.Contains(DirectionEnum.left) && DirectionList.Contains(DirectionEnum.right))
			{
				int rightIndex = DirectionList.IndexOf(DirectionEnum.right);
				int leftIndex = DirectionList.IndexOf(DirectionEnum.right);

			}
			return true;
		}
		return false;
	}

}

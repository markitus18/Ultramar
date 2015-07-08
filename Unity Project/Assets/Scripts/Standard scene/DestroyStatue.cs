using UnityEngine;
using System.Collections;

public class DestroyStatue : MonoBehaviour
{
	public GameObject finalBox;
	public DestructiveWall linkerBox;

	void OnMouseUp()
	{
		finalBox.SetActive(true);
		linkerBox.SplitBoxes();
		gameObject.SetActive(false);
	}
}

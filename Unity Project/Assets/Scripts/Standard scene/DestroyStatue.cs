using UnityEngine;
using System.Collections;

public class DestroyStatue : MonoBehaviour
{
	public GameObject finalBox;
	public DisconectBoxes linkerBox;

	void OnTouchUp()
	{
		finalBox.SetActive(true);
		linkerBox.SplitBoxes();
		gameObject.SetActive(false);
	}
}

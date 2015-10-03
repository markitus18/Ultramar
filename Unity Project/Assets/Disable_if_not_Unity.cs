using UnityEngine;
using System.Collections;

public class Disable_if_not_Unity : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

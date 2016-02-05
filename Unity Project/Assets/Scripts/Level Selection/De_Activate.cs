using UnityEngine;
using System.Collections;

public class De_Activate : MonoBehaviour {

	public void ChangeState(GameObject toChange)
    {
        toChange.SetActive(!toChange.activeSelf);
    }

    public void Deactivate(GameObject toChange)
    {
        toChange.SetActive(false);
    }

    public void Activate(GameObject toChange)
    {
        toChange.SetActive(true);
    }
}

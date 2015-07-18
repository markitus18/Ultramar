using UnityEngine;
using System.Collections;

public class MeshEditor : MonoBehaviour {

	// Use this for initialization

	bool up = true;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		int i = 0;

		while (i < vertices.Length)
		{
			if (up)
			{
				if (i % 2 == 0)
					vertices[i] += Vector3.up * Time.deltaTime;
				else
					vertices[i] += Vector3.down * Time.deltaTime;
				i++;
			}
			else
			{
				if (i % 2 == 0)
					vertices[i] += Vector3.down * Time.deltaTime;
				else
					vertices[i] += Vector3.up * Time.deltaTime;
			i++;
			}
		}
		up = !up;
		mesh.vertices = vertices;
	}
}

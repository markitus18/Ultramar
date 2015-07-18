using UnityEngine;
using System.Collections;

public class MeshEditor : MonoBehaviour {

	// Use this for initialization

	bool up = true;
	int change = 0;
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
					vertices[i] += (Vector3.up / 6) * Time.deltaTime;
				else
					vertices[i] += (Vector3.down / 6)* Time.deltaTime;
				i++;
			}
			else
			{
				if (i % 2 == 0)
					vertices[i] += (Vector3.down / 6) * Time.deltaTime;
				else
					vertices[i] += (Vector3.up / 6) * Time.deltaTime;
			i++;
			}
		}
		change++;
		if (change > 60)
		{
			up = !up;
			change = 0;
		}
		mesh.vertices = vertices;
	}
}

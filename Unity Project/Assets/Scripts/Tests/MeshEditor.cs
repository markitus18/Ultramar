using UnityEngine;
using System.Collections;

public class MeshEditor : MonoBehaviour {

	// Use this for initialization

	bool up = true;
	int state = 1;
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
		if (state == 1 || state == 4)
			{
				if (i % 2 == 0)
					vertices[i] += (Vector3.up / 6) * Time.deltaTime;
				else
					vertices[i] += (Vector3.down / 6)* Time.deltaTime;
				normals[i] = Vector3.up;
				i++;
			}

		else if (state == 2 || state == 3)
			{
				if (i % 2 == 0)
					vertices[i] += (Vector3.down / 6) * Time.deltaTime;
				else
					vertices[i] += (Vector3.up / 6) * Time.deltaTime;
				normals[i] = Vector3.up;
			i++;
			}
		}
		change ++;
		if (change == 60)
		{
			change = 0;
			if (++state == 5)
				state = 1;
		}
		mesh.vertices = vertices;
	}
}

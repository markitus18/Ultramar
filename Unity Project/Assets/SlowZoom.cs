using UnityEngine;
using System.Collections;

public class SlowZoom : MonoBehaviour {

    // The target marker.
    public Vector3 destination;
	
	public float speed = 0.5f;
    public float maxSpeed = 0.5f;
	
    void Start()
    {
        speed = 0;
    }
	
	void Update()
    {
        // The step size is equal to speed times frame time.
        if (speed < maxSpeed)
        {
            speed += maxSpeed / 500;
        }


        var step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }
}

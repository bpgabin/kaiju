using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float speed = 5.0f;
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.position = newPosition;
	}
}

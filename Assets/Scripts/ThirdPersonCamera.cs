using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	public GameObject target;
	public float rotateSpeed = 300.0f;
	public float lookAtHeight = 5.0f;
	
	Vector3 offset;
	Quaternion rotation = Quaternion.Euler(0, 0, 0);
	
	// Use this for initialization
	void Start() {
		offset = target.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate() {
		if(Time.timeScale != 0){
			float horizontal = 0;
			if(Input.GetMouseButton(0)){
				horizontal = Time.deltaTime * Input.GetAxis("Mouse X") * rotateSpeed * 1.7f;
			}
			else if(Input.GetKey(KeyCode.LeftArrow)){
				horizontal = Time.deltaTime * rotateSpeed;
			}
			else if(Input.GetKey (KeyCode.RightArrow)){
				horizontal = Time.deltaTime * -rotateSpeed;	
			}
			rotation = Quaternion.Euler(0, rotation.eulerAngles.y + horizontal, 0);
		}
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);
		transform.Rotate(new Vector3(lookAtHeight, 0, 0));
	}
}

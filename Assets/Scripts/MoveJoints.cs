using UnityEngine;
using System.Collections;

public class MoveJoints : MonoBehaviour {
	// Bone Joints of the Puppet.
	public ConfigurableJoint leftThigh;
	public ConfigurableJoint leftKnee;
	public ConfigurableJoint leftFoot;
	
	public ConfigurableJoint rightThigh;
	public ConfigurableJoint rightKnee;
	public ConfigurableJoint rightFoot;
	
	public ConfigurableJoint leftUpperArm;
	public ConfigurableJoint leftLowerArm;
	
	public ConfigurableJoint rightUpperArm;
	public ConfigurableJoint rightLowerArm;
	
	public ConfigurableJoint hip;
	public ConfigurableJoint head;
	
	
	// Movement Configuration
	public float speed = 5.0f;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)){
			changeAngle(leftThigh, speed * Time.deltaTime, 0, 0);
			changeAngle(leftKnee, -speed * Time.deltaTime, 0, 0);
			changeAngle(hip, speed * Time.deltaTime, 0, 0);
		}
	}
	
	// Changes ConfigurableJoint targetRotation by given amount.
	void changeAngle(ConfigurableJoint cJoint, float dX, float dY, float dZ){
		Quaternion newRotation = cJoint.targetRotation;
		newRotation.x += dX;
		newRotation.y += dY;
		newRotation.z += dZ;
		cJoint.targetRotation = newRotation;
	}
}

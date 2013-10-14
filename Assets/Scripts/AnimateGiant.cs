using UnityEngine;
using System.Collections;

public class AnimateGiant : MonoBehaviour {
	public Transform hip;
	Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.speed = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		// Get the current animation state.
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);
		
		if(	stateInfo.nameHash == Animator.StringToHash("Legs.Idle") || 
			stateInfo.nameHash == Animator.StringToHash("Legs.RobotLeftStepStill") ||
		   	stateInfo.nameHash == Animator.StringToHash("Legs.RobotRightStepStill")){
			if(Input.GetKeyDown(KeyCode.Q)){
				anim.SetBool("LeftStep", true);
			}
			if(Input.GetKeyDown(KeyCode.W)){
				anim.SetBool("RightStep", true);
			}
		}
		else{
			anim.SetBool("LeftStep", false);
			anim.SetBool("RightStep", false);
		}
	}
}

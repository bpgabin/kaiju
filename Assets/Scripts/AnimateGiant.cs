using UnityEngine;
using System.Collections;

public class AnimateGiant : MonoBehaviour {
	Animator anim;
	
	public AudioSource[] aSources;
	AudioSource audio1;
	AudioSource audio2;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.speed = 0.25f;
		
		audio1 = aSources[0];
		audio2 = aSources[1];
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
				audio1.Play();
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

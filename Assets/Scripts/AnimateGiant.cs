using UnityEngine;
using System.Collections;

public class AnimateGiant : MonoBehaviour {
	public AudioSource[] audioSources;
	
	Animator anim;
	AudioSource soundHydraulic;
	AudioSource soundFootstep;
	
	int lastState;
	bool stepping = false;
	bool running = false;
	int lastRunButton = -1;
	
	float lastStepTime = -1f;
	float cadenceTime = 1.0f;
	float startRunTime = 3.0f;
	
	RagdollHelper helper;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
		soundHydraulic = audioSources[0];
		soundFootstep = audioSources[1];
		
		helper = GetComponent<RagdollHelper>();
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);
		
		if(helper.ragdolled == true && Input.GetKeyDown(KeyCode.S)){
			helper.ragdolled = false;
		}
		
		if(	stateInfo.nameHash == Animator.StringToHash("Legs.RobotLeftStepStill") ||
			stateInfo.nameHash == Animator.StringToHash("Legs.RobotRightStepStill") ||
			stateInfo.nameHash == Animator.StringToHash("Legs.Idle")){
			if(stateInfo.nameHash == Animator.StringToHash("Legs.Idle")){
				if(anim.GetBool("LeftStep") || anim.GetBool("RightStep")){
					if(lastState == Animator.StringToHash("Legs.RobotLeftStepStill") || lastState == Animator.StringToHash("Legs.RobotRightStepStill")){
						if(stepping){
							stepping = false;
							soundFootstep.Play();
						}
						
						anim.SetBool("LeftStep", false);
						anim.SetBool("RightStep", false);
					}
				}
			}
			
			if(Input.GetKeyDown(KeyCode.Q) && !anim.GetBool("LeftStep")){
				if(Input.GetKey(KeyCode.Space)){
					if(!running && lastStepTime != -1 && (Time.time - lastStepTime) <= startRunTime){
						running = true;
						anim.SetBool("Running", true);
						lastRunButton = 1;
					}
					else if(running && lastRunButton == 2 && (Time.time - lastStepTime <= cadenceTime)){
						lastRunButton = 1;	
					}
					else if(running && (lastRunButton == 1 || (Time.time - lastStepTime > cadenceTime))){
						running = false;
						anim.SetBool("Running", false);
						helper.ragdolled = true;
					}
				}
				else if(running){
					running = false;
					anim.SetBool("Running", false);
					helper.ragdolled = true;
				}
				else if(Input.GetKey(KeyCode.W))
					anim.SetFloat("Turning", -1.0f);
				else if(Input.GetKey(KeyCode.E))
					anim.SetFloat("Turning", 1.0f);
				else
					anim.SetFloat("Turning", 0.0f);
				
				if(!running){
					anim.SetBool("LeftStep", true);
					soundHydraulic.Play();
					stepping = true;
					lastState = stateInfo.nameHash;
				}
					
				lastStepTime = Time.time;
			}
			else if(Input.GetKeyDown(KeyCode.R) && !anim.GetBool("RightStep")){
				if(Input.GetKey(KeyCode.Space)){
					if(!running && lastStepTime != -1 && (Time.time - lastStepTime) <= startRunTime){
						running = true;
						anim.SetBool("Running", true);
						lastRunButton = 2;
					}
					else if(running && lastRunButton == 1 && (Time.time - lastStepTime <= cadenceTime)){
						lastRunButton = 2;	
					}
					else if(running && (lastRunButton == 2 || (Time.time - lastStepTime > cadenceTime))){
						running = false;
						anim.SetBool("Running", false);
						helper.ragdolled = true;
					}
				}
				else if(Input.GetKey(KeyCode.W))
					anim.SetFloat("Turning", -1.0f);
				else if(Input.GetKey(KeyCode.E))
					anim.SetFloat("Turning", 1.0f);
				else
					anim.SetFloat("Turning", 0.0f);
				
				if(!running){
					anim.SetBool("RightStep", true);
					soundHydraulic.Play();
					stepping = true;
					lastState = stateInfo.nameHash;
				}
					
				lastStepTime = Time.time;
			}
			else if(running && (Time.time - lastStepTime) > cadenceTime){
				running = false;
				anim.SetBool("Running", false);
				helper.ragdolled = true;
			}
		}
		else{
			if(stepping && stateInfo.normalizedTime >= 0.8f){
				stepping = false;
				soundFootstep.Play();
			}
			anim.SetBool("LeftStep", false);
			anim.SetBool("RightStep", false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class AnimateMonster : MonoBehaviour {
	public AudioSource[] audioSources;
	
	GameStats stats;
	Animator anim;
	AudioSource soundFootstep;
	
	int lastState;
	bool stepping = false;
	bool running = false;
	int lastRunButton = -1;
	
	float lastStepTime = -1f;
	float cadenceTime = 0.25f;
	float startRunTime = 3.0f;
	float timeWhenRunStarted = 0;
	float timeFell;
	
	RagdollHelper helper;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
		soundFootstep = audioSources[0];
		
		helper = GetComponent<RagdollHelper>();
		
		stats = GameObject.Find("GameStats").GetComponent<GameStats>();
		
		//Now cast a ray from the computed position downwards and find the highest hit
		Vector3 newPosition = transform.position;
		RaycastHit[] hits = Physics.RaycastAll(new Ray(newPosition, Vector3.down)); 
		newPosition.y = -100f;
		foreach(RaycastHit hit in hits){
			if (!hit.transform.IsChildOf(transform)){
				newPosition.y = Mathf.Max(newPosition.y, hit.point.y);
			}
		}
		
		transform.position = newPosition;
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);

		// player slips if they go up too fast
		if (this.rigidbody.velocity.y > 1f)
			helper.ragdolled = true;
		
		if(Time.timeSinceLevelLoad > 3.0f){
			if(stats.getStartTime() != -1.0f){
		
		if(helper.ragdolled == true && Input.GetKeyDown(KeyCode.S) && Time.time - timeFell > 3.0f){
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
						timeWhenRunStarted = Time.time;
					}
					else if(running && lastRunButton == 2 && (Time.time - lastStepTime <= cadenceTime)){
						lastRunButton = 1;	
					}
					else if(running && (lastRunButton == 1 || (Time.time - lastStepTime > cadenceTime))){
						running = false;
						helper.ragdolled = true;
						anim.SetBool("Running", false);
						timeFell = Time.time;
						stats.increaseTimesFallen();
						stats.checkRunTime(Time.time - timeWhenRunStarted);
					}
				}
				else if(running){
					if(Time.time - lastStepTime > cadenceTime){
						helper.ragdolled = true;
						timeFell = Time.time;
					}
					running = false;
					anim.SetBool("Running", false);
					stats.checkRunTime(Time.time - timeWhenRunStarted);
				}
				else if(Input.GetKey(KeyCode.W))
					anim.SetFloat("Turning", -1.0f);
				else if(Input.GetKey(KeyCode.E))
					anim.SetFloat("Turning", 1.0f);
				else
					anim.SetFloat("Turning", 0.0f);
				
				if(!running){
					anim.SetBool("LeftStep", true);
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
						timeWhenRunStarted = Time.time;
					}
					else if(running && lastRunButton == 1 && (Time.time - lastStepTime <= cadenceTime)){
						lastRunButton = 2;	
					}
					else if(running && (lastRunButton == 2 || (Time.time - lastStepTime > cadenceTime))){
						running = false;
						helper.ragdolled = true;
						anim.SetBool("Running", false);
						timeFell = Time.time;
						stats.increaseTimesFallen();
						stats.checkRunTime(Time.time - timeWhenRunStarted);
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
					stepping = true;
					lastState = stateInfo.nameHash;
				}
					
				lastStepTime = Time.time;
			}
			else if(running && (Time.time - lastStepTime) > cadenceTime){
				running = false;
				helper.ragdolled = true;
				anim.SetBool("Running", false);
				timeFell = Time.time;
				stats.increaseTimesFallen();
				stats.checkRunTime(Time.time - timeWhenRunStarted);
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
	}
}


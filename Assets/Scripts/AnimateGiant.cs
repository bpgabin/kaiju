using UnityEngine;
using System.Collections;

public class AnimateGiant : MonoBehaviour {
	public AudioSource[] audioSources;
	
	Animator anim;
	AudioSource soundHydraulic;
	AudioSource soundFootstep;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
		soundHydraulic = audioSources[0];
		soundFootstep = audioSources[1];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			anim.SetBool("LeftStep", true);
			soundHydraulic.Play();
			soundFootstep.PlayDelayed(0.9f);
		}
		else if(Input.GetKeyDown(KeyCode.W)){
			anim.SetBool("RightStep", true);
			soundHydraulic.Play();
			soundFootstep.PlayDelayed(0.9f);
		}
		else{
			anim.SetBool("LeftStep", false);
			anim.SetBool("RightStep", false);
		}
	}
}

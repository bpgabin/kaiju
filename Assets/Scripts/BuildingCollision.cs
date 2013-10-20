using UnityEngine;
using System.Collections;

public class BuildingCollision : MonoBehaviour {
	public float minMult = 0.75f;
	public float maxMult = 1.75f;
	
	void Start(){
		float heightMult = Random.Range(minMult, maxMult);
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * heightMult, transform.localScale.z);
	}
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.name == "Player"){
			collider.enabled = false;
			renderer.enabled = false;
			particleSystem.Play();
			audio.Play();
		}
	}
}

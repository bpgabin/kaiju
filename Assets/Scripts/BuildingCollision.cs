using UnityEngine;
using System.Collections;

public class BuildingCollision : MonoBehaviour {

	void Start(){
		float heightMult = Random.Range(0.75f, 1.75f);
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

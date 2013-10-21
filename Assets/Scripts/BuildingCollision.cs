using UnityEngine;
using System.Collections;

public class BuildingCollision : MonoBehaviour {
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			collider.enabled = false;
			renderer.enabled = false;
			particleSystem.Play();
			audio.Play();
		}
	}
}

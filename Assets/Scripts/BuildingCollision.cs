using UnityEngine;
using System.Collections;

public class BuildingCollision : MonoBehaviour {
	
	GameStats stats;
	
	void Start(){
		GameObject statsObject = GameObject.Find("GameStats");
		if(statsObject != null){
			stats = statsObject.GetComponent<GameStats>();
		}
		else {
			Debug.Log("GameStats object could not be found.");
		}
	}
	
	void OnCollisionEnter(Collision other){	
		if(other.gameObject.tag == "Player"){
			stats.increaseBuildingsDestroyed();
			collider.enabled = false;
			renderer.enabled = false;
			particleSystem.Play();
			audio.Play();
		}
		else if(other.gameObject.tag == "Monster"){
			collider.enabled = false;
			renderer.enabled = false;
			particleSystem.Play();
			audio.Play();
		}
	}
}

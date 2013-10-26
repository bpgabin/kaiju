using UnityEngine;
using System.Collections;

public class CarCollision : MonoBehaviour {
	public AudioSource soundSiren;
	public AudioSource soundExplosion;
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
			stats.increaseCarsDestroyed();
			AudioSource.PlayClipAtPoint(soundExplosion.clip, transform.position);
			Destroy(gameObject);
		}
		else if(other.gameObject.tag == "Monster"){
			stats.increaseCarsDestroyed();
			AudioSource.PlayClipAtPoint(soundExplosion.clip, transform.position);
			Destroy(gameObject);
		}
	}
}
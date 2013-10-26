using UnityEngine;
using System.Collections;

public class BuildingCollision : MonoBehaviour {
	
	GameStats stats;
	public AudioClip soundCrash;
	public GameObject buildingDestruct;
	
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
			AudioSource.PlayClipAtPoint(soundCrash, transform.position);
			GameObject newDestruct = (GameObject)Instantiate(buildingDestruct, transform.position, transform.rotation);
			newDestruct.transform.localScale = transform.localScale;
			Destroy(gameObject);
		}
		else if(other.gameObject.tag == "Monster"){
			stats.inscreaseMonsterBuildingsDestroyed();
			AudioSource.PlayClipAtPoint(soundCrash, transform.position);
			GameObject newDestruct = (GameObject)Instantiate(buildingDestruct, transform.position, transform.rotation);
			newDestruct.transform.localScale = transform.localScale;
			Destroy(gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {
	ThirdPersonCamera cameraScript;
	GameStats stats;
	bool triggered = false;
	
	// Use this for initialization
	void Start () {
		cameraScript = GetComponent<ThirdPersonCamera>();
		GameObject statsObject = GameObject.Find("GameStats");
		stats = statsObject.GetComponent<GameStats>();
	}
	
	// Update is called once per frame
	void Update () {
		if(stats == null){
			GameObject statsObject = GameObject.Find("GameStats");
			if(statsObject != null){
				stats = statsObject.GetComponent<GameStats>();
			}
		}
		if(cameraScript.enabled && !triggered){
			stats.startGame();
			triggered = true;
			audio.Play();
		}
	}
}

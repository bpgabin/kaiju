using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {
	ThirdPersonCamera cameraScript;
	GameStats stats;
	bool triggered = false;
	public AudioSource[] audioSources;
	AudioSource soundRobot;
	AudioSource soundMonster;
	
	// Use this for initialization
	void Start () {
		soundRobot = audioSources[0];
		soundMonster = audioSources[1];
		
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
			GameObject menuObject = GameObject.FindGameObjectWithTag("Menu");
			MainMenu menu = null;
			if(menuObject != null)
				menu = menuObject.GetComponent<MainMenu>();
			stats.startGame();
			triggered = true;
			if(menu != null){
				if(menu.gameScene == MainMenu.gameScenes.robotMode)
					soundRobot.Play();
				else if(menu.gameScene == MainMenu.gameScenes.monsterMode)
					soundMonster.Play();
			}
		}
	}
}

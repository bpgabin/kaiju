using UnityEngine;
using System;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GUISkin mySkin;
	
	public enum gameScenes {none, robotMode, monsterMode};
	public gameScenes gameScene = gameScenes.none;
	
	public AudioSource[] audioSources;
	
	enum menuState {mainMenu, optionsMenu, credits, pauseMenu, pauseOptions, game, score, blank};
	menuState currentState = menuState.mainMenu;
	string lastTooltip = "";
	AudioSource soundSelect;
	AudioSource soundHighlight;
	GameStats stats;
	float endTime;
	
	void Start(){
		DontDestroyOnLoad(gameObject);
		soundSelect = audioSources[0];
		soundHighlight = audioSources[1];
		
		if(gameScene != gameScenes.none){
			currentState = menuState.game;
			gameObject.AddComponent("GameStats");
		}
	}
	
	void Update(){
		if(stats == null && currentState == menuState.game){
			GameObject statsObject = GameObject.Find("GameStats");
			if(statsObject != null)
				stats = statsObject.GetComponent<GameStats>();	
		}
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(currentState == menuState.game){
				Time.timeScale = 0;
				currentState = menuState.pauseMenu;
			}
			else if(currentState == menuState.pauseMenu || currentState == menuState.pauseOptions){
				Time.timeScale = 1;
				currentState = menuState.game;
			}
		}
		if(Input.GetKeyDown(KeyCode.A)){
			Time.timeScale = 0;
			currentState = menuState.score;
			endTime = Time.timeSinceLevelLoad;
		}
	}
	
	void OnGUI(){
		GUI.skin = mySkin;
		switch(currentState){
		case menuState.mainMenu:
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 400));
				GUILayout.BeginVertical();
					GUILayout.Box("KAIJU");
					GUILayout.BeginHorizontal();
					if(GUILayout.Button(new GUIContent("Robot", "Button"))){
						Application.LoadLevel("Neo_Tokyo");
						soundSelect.Play();
						gameScene = gameScenes.robotMode;
						currentState = menuState.game;
					}
					if(GUILayout.Button(new GUIContent("Monster", "Button"))){
						soundSelect.Play();
					}
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					if(GUILayout.Button(new GUIContent("Options", "Button"), GUILayout.Width(100))){
						currentState = menuState.optionsMenu;
						soundSelect.Play();
					}
				GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if(GUILayout.Button(new GUIContent("Credits", "Button"), GUILayout.Width(100))){
					soundSelect.Play();	
					currentState = menuState.credits;
				}
				GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUILayout.EndArea();
			break;
		case menuState.optionsMenu:
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
			GUILayout.BeginVertical();
			
			GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.Box("Options", GUILayout.Width(100));
				GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.Box("Global Volume: " + string.Format("{0:0.00}", AudioListener.volume));
			AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0.0f, 1.0f);
			GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if(GUILayout.Button(new GUIContent("Main Menu", "Button"))){
					currentState = menuState.mainMenu;
					soundSelect.Play();
					currentState = menuState.mainMenu;
				}
				GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUILayout.EndArea();
			break;
		case menuState.credits:
			break;
		case menuState.game:
			GUILayout.BeginArea(new Rect(Screen.width - 55, Screen.height - 25, 50, 20));
				if(GUILayout.Button(new GUIContent("Menu", "Button"))){
					Time.timeScale = 0;
					currentState = menuState.pauseMenu;
					soundSelect.Play();
				}
			GUILayout.EndArea();
			
			if(gameScene == gameScenes.robotMode){
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 0, 100, 100));
					GUILayout.BeginVertical();
						GUILayout.Box("Time: " + string.Format(Math.Floor(Time.timeSinceLevelLoad / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(Time.timeSinceLevelLoad / 60), Time.timeSinceLevelLoad - Math.Floor(Time.timeSinceLevelLoad / 60) * 60));
					GUILayout.EndVertical();
				GUILayout.EndArea();
			}
			break;
		case menuState.pauseMenu:
			if(gameScene == gameScenes.robotMode){
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 0, 100, 100));
					GUILayout.BeginVertical();
						GUILayout.Box("Time: " + string.Format(Math.Floor(Time.timeSinceLevelLoad / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(Time.timeSinceLevelLoad / 60), Time.timeSinceLevelLoad - Math.Floor(Time.timeSinceLevelLoad / 60) * 60));
					GUILayout.EndVertical();
				GUILayout.EndArea();	
			}
			
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 400));
				GUILayout.BeginVertical();	
					GUILayout.Box("Paused");
					if(GUILayout.Button(new GUIContent("Resume", "Button"))){
						Time.timeScale = 1;
						currentState= menuState.game;
						soundSelect.Play();
					}
					if(GUILayout.Button(new GUIContent("Options", "Button"))){
						currentState = menuState.pauseOptions;
						soundSelect.Play();
					}
					if(GUILayout.Button(new GUIContent("Main Menu", "Button"))){
						Time.timeScale = 1;
						Application.LoadLevel("MainMenu");
						stats = null;
						soundSelect.Play();
						gameScene = gameScenes.none;
						currentState = menuState.mainMenu;
					}
				GUILayout.EndVertical();
			GUILayout.EndArea();
			break;
		case menuState.pauseOptions:
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height - 200, 200, 400));
				GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						GUILayout.Box("Options", GUILayout.Width(100));
						GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.Box("Global Volume: " + string.Format("{0:0.00}", AudioListener.volume));
					AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0.0f, 1.0f);
					GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						if(GUILayout.Button(new GUIContent("Go Back", "Button"))){
							currentState = menuState.pauseMenu;
							soundSelect.Play();
						}
						GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				GUILayout.EndVertical();
			GUILayout.EndArea();
			break;
		case menuState.score:
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 400));
				GUILayout.BeginVertical();
					GUILayout.Box("Game Over");
					GUILayout.Box("Time Elapsed: " + string.Format(Math.Floor(endTime / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(endTime / 60), endTime - Math.Floor(endTime / 60) * 60));
					GUILayout.Box("Buildings Destroyed: " + stats.getBuildingsDestroyed());
					GUILayout.Box("Times Fallen: " + stats.getTimesFallen());
					if(stats.getLongestRun() > -1)
						GUILayout.Box("Longest Run Time: " + string.Format("{0:0.00}", stats.getLongestRun()));
					if(GUILayout.Button(new GUIContent("Restart", "Button"))){
						Application.LoadLevel("Neo_Tokyo");
						currentState = menuState.game;
						soundSelect.Play();
						stats = null;
					}
					if(GUILayout.Button(new GUIContent("Main Menu", "Button"))){
						Application.LoadLevel("MainMenu");
						gameScene = gameScenes.none;
						currentState = menuState.mainMenu;
						soundSelect.Play();
						stats = null;
						Time.timeScale = 1;
					}
				GUILayout.EndVertical();
			GUILayout.EndArea();
			break;
		}
		
		 if(Event.current.type == EventType.Repaint && GUI.tooltip != lastTooltip) {
            if (lastTooltip != "")
                SendMessage(lastTooltip + "OnMouseOut", SendMessageOptions.DontRequireReceiver);
            
            if (GUI.tooltip != "")
                SendMessage(GUI.tooltip + "OnMouseOver", SendMessageOptions.DontRequireReceiver);
            
            lastTooltip = GUI.tooltip;
        }
		GUI.skin = null;
	}
	
	void ButtonOnMouseOver(){
		soundHighlight.Play();
	}
}

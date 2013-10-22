using UnityEngine;
using System;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GUISkin mySkin;
	
	public enum gameScenes {none, robotMode, monsterMode};
	public gameScenes gameScene = gameScenes.none;
	
	enum menuState {mainMenu, optionsMenu, credits, pauseMenu, pauseOptions, game};
	menuState currentState = menuState.mainMenu;
	string lastTooltip = "";
	
	void Start(){
		if(gameScene != gameScenes.none){
			currentState = menuState.game;
			gameObject.AddComponent("GameStats");
		}
	}
	
	void Update(){
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
					}
					if(GUILayout.Button(new GUIContent("Monster", "Button"))){
				
					}
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					if(GUILayout.Button(new GUIContent("Options", "Button"), GUILayout.Width(100))){
						currentState = menuState.optionsMenu;
					}
				GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if(GUILayout.Button(new GUIContent("Credits", "Button"), GUILayout.Width(100))){
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
				if(GUILayout.Button(new GUIContent("Main Menu", "Button")))
					currentState = menuState.mainMenu;
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
				}
			GUILayout.EndArea();
			
			if(gameScene == gameScenes.robotMode){
				//GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 0, 100, 100));
					GUILayout.BeginVertical();
						GUILayout.Box("Time: " + string.Format(Math.Floor(Time.timeSinceLevelLoad / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(Time.timeSinceLevelLoad / 60), Time.timeSinceLevelLoad - Math.Floor(Time.timeSinceLevelLoad / 60) * 60));
					GUILayout.EndVertical();
				//GUILayout.EndArea();
			}
			break;
		case menuState.pauseMenu:
			if(gameScene == gameScenes.robotMode){
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 0, 100, 100));
					GUILayout.BeginVertical();
						GUILayout.Box("Time: " + string.Format("{0:0.00}", Time.timeSinceLevelLoad));
					GUILayout.EndVertical();
				GUILayout.EndArea();	
			}
			
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height - 200, 200, 400));
				GUILayout.BeginVertical();	
					GUILayout.Box("Paused");
					if(GUILayout.Button(new GUIContent("Resume", "Button"))){
						Time.timeScale = 1;
						currentState= menuState.game;
					}
					if(GUILayout.Button("Options")){
						currentState = menuState.pauseOptions;
					}
					if(GUILayout.Button(new GUIContent("Main Menu", "Button"))){
						Time.timeScale = 1;
						Application.LoadLevel("MainMenu");
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
						if(GUILayout.Button(new GUIContent("Go Back", "Button")))
							currentState = menuState.pauseMenu;
						GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
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
	}
	
	void ButtonOnMouseOver(){
		Debug.Log ("Button1 got focus");
	}
}

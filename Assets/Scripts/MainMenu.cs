using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
	public GUISkin mySkin;
	
	enum menuState {mainMenu, optionsMenu, credits, pauseMenu, pauseOptions};
	menuState currentState = menuState.mainMenu;
	
	void OnGUI(){
		GUI.skin = mySkin;
		switch(currentState){
		case menuState.mainMenu:
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 400));
	
			GUILayout.BeginVertical();
			GUILayout.Box("KAIJU");
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Robot")){
				Application.LoadLevel("Neo_Tokyo");	
			}
			if(GUILayout.Button("Monster")){
				
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if(GUILayout.Button("Options", GUILayout.Width(100))){
					currentState = menuState.optionsMenu;
				}
				GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if(GUILayout.Button("Credits", GUILayout.Width(100))){
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
				if(GUILayout.Button("Main Menu"))
					currentState = menuState.mainMenu;
				GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUILayout.EndArea();
			break;
		case menuState.credits:
			break;
		}
	}
}

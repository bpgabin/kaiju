using UnityEngine;
using System;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GUISkin mySkin;
	
	public enum gameScenes {none, robotMode, monsterMode};
	public gameScenes gameScene = gameScenes.none;
	public float monsterTimeAllowed = 120.0f;
	public AudioSource[] audioSources;
	
	enum menuState {mainMenu, optionsMenu, credits, instructions, pauseMenu, pauseOptions, game, score, blank};
	menuState currentState = menuState.mainMenu;
	string lastTooltip = "";
	AudioSource soundSelect;
	AudioSource soundHighlight;
	GameStats stats;
	float endTime;
	bool showTutorial = true;
	public KongregateAPI kongAPI;
	
	void Start(){
		DontDestroyOnLoad(gameObject);
		soundSelect = audioSources[0];
		soundHighlight = audioSources[1];
	}
	
	void Update(){
		if(currentState == menuState.game && gameScene == gameScenes.monsterMode){
			float tempTime = monsterTimeAllowed - (Time.time - stats.getStartTime());
			if(tempTime <= 0){
				EndGame();	
			}
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
		
		if(currentState == menuState.blank && Time.timeSinceLevelLoad > 3.0f){
			if(stats == null && currentState == menuState.blank){
			GameObject statsObject = GameObject.Find("GameStats");
			if(statsObject != null)
				stats = statsObject.GetComponent<GameStats>();	
			}
			if(stats.getStartTime() != -1.0f){
				currentState = menuState.game;
			}
		}
	}
	
	// Ends play of the current game.
	public void EndGame(){
		// Pause the game and show the scoreboard.
		Time.timeScale = 0;
		currentState = menuState.score;
		stats.endGameTime();
		endTime = stats.getFinalTime();
		
		// Report Game Statistics to Kongregate
		if(kongAPI.isKongregate){
			if(gameScene == gameScenes.robotMode){
				kongAPI.SubmitStats("completedRobotMode", 1);
				kongAPI.SubmitStats("robotScore", stats.getScore("robot"));
				kongAPI.SubmitStats("leastBuildingsDestroyedRobot", stats.getBuildingsDestroyed());
			}
			else if(gameScene == gameScenes.monsterMode){
				kongAPI.SubmitStats("completedMonsterMode", 1);
				kongAPI.SubmitStats("monsterScore", stats.getScore("monster"));
				kongAPI.SubmitStats("mostBuildingsDestroyedMonster", stats.getBuildingsDestroyed());
			}
		}
	}
	
	void OnGUI(){
		GUI.skin = mySkin;
		float tempTime = 0;
		switch(currentState){
		case menuState.mainMenu:
			// If on Kongregate display welcome message to user.
			if(kongAPI.isKongregate)
				GUILayout.Box("Welcome " + kongAPI.username + "!");
			else
				GUILayout.Box("Connecting to Kongregate..");
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 120, 100, 400));
				GUILayout.BeginVertical();
					if(GUILayout.Button(new GUIContent("Robot", "Button"))){
						Application.LoadLevel("Neo_Tokyo");
						soundSelect.Play();
						gameScene = gameScenes.robotMode;
						currentState = menuState.blank;
					}
					if(GUILayout.Button(new GUIContent("Monster", "Button"))){
						Application.LoadLevel("TokyoMonster");
						soundSelect.Play();
						gameScene = gameScenes.monsterMode;
						currentState = menuState.blank;
					}
					if(GUILayout.Button (new GUIContent("Tutorial", "Button"))){
						soundSelect.Play();
						currentState = menuState.instructions;
					}
					if(GUILayout.Button(new GUIContent("Options", "Button"))){
						currentState = menuState.optionsMenu;
						soundSelect.Play();
					}
					if(GUILayout.Button(new GUIContent("Credits", "Button"))){
						soundSelect.Play();	
						currentState = menuState.credits;
					}
				GUILayout.EndVertical();
			GUILayout.EndArea();
			break;
		case menuState.instructions:
			// If on Kongregate display welcome message to user.
			if(kongAPI.isKongregate)
				GUILayout.Box("Welcome " + kongAPI.username + "!");
			else
				GUILayout.Box("Connecting to Kongregate..");
			// robot Instructions
			GUILayout.BeginArea(new Rect(0, Screen.height - 100, 300, 400));
			GUILayout.Box("Goal: Catch the Monster without destroying Tokyo.\nPoints: Low times and less destruction.");
			GUILayout.EndArea();
			
			// monster Instructions
			GUILayout.BeginArea(new Rect(Screen.width - 300, Screen.height - 100, 300, 200));
			GUILayout.Box("Goal: Destroy Tokyo before the robot gets you.\nPoints: Higher times and mass destruction.");
			GUILayout.EndArea();
			
			
			// return button
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 120, 300, 200));
			GUILayout.Box("Walk: Alternate Q and R.\nTurn: Hold either W or E while walking.\nSprint: Hold Spacebar while rapidly walking.\nGet-up: Press S.");
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if(GUILayout.Button(new GUIContent("Main Menu", "Button"), GUILayout.Width(100))){
				soundSelect.Play();
				currentState = menuState.mainMenu;
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
			break;
		case menuState.optionsMenu:
			// If on Kongregate display welcome message to user.
			if(kongAPI.isKongregate)
				GUILayout.Box("Welcome " + kongAPI.username + "!");
			else
				GUILayout.Box("Connecting to Kongregate..");
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 120, 200, 200));
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
			// If on Kongregate display welcome message to user.
			if(kongAPI.isKongregate)
				GUILayout.Box("Welcome " + kongAPI.username + "!");
			else
				GUILayout.Box("Connecting to Kongregate..");
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 200, 400));
				GUILayout.BeginVertical();
					GUILayout.Box ("Brian Gabin");
					GUILayout.Box ("Ruben Telles");
					GUILayout.Box ("Victor Nguyen");
					GUILayout.Box ("Chase Khamashta");
					GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						if(GUILayout.Button(new GUIContent("Return", "Button"), GUILayout.Width(100))){
							currentState = menuState.mainMenu;
							soundSelect.Play();
						}
						GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				GUILayout.EndVertical();
			GUILayout.EndArea();
			break;
		// During Gameplay Menu
		case menuState.game:
			// Menu button that accesses the pause menu.
			GUILayout.BeginArea(new Rect(Screen.width - 55, Screen.height - 30, 50, 25));
				if(GUILayout.Button(new GUIContent("Menu", "Button"))){
					Time.timeScale = 0;
					currentState = menuState.pauseMenu;
					soundSelect.Play();
				}
			GUILayout.EndArea();
			
			// For robot mode there's simply a timer which counts upwards
			if(gameScene == gameScenes.robotMode){
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 0, 100, 100));
					GUILayout.BeginVertical();
						tempTime = stats.getGameTime();
						GUILayout.Box("Time: " + string.Format(Math.Floor(tempTime / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(tempTime / 60), tempTime - Math.Floor(tempTime / 60) * 60));
					GUILayout.EndVertical();
				GUILayout.EndArea();
			}
			
			// For monster mode the timer counts down
			if(gameScene == gameScenes.monsterMode){
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 0, 100, 100));
					GUILayout.BeginVertical();
						tempTime = monsterTimeAllowed - stats.getGameTime();
						GUILayout.Box("Time: " + string.Format(Math.Floor(tempTime / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(tempTime / 60), tempTime - Math.Floor(tempTime / 60) * 60));
					GUILayout.EndVertical();
				GUILayout.EndArea();
			}
			
			// Tutorial Tooltip
			if(showTutorial){
				GUILayout.BeginArea(new Rect(5, Screen.height - 110, 200, 120));
					GUILayout.BeginVertical();
						GUILayout.Box("Stepping: Q and R\nTurning: Hold W and E\nSprinting: Hold Space and step\nRecover: Press S");
						if(GUILayout.Button(new GUIContent("Dismiss", "Button"))){
							soundSelect.Play();
							showTutorial = false;
						}
					GUILayout.EndVertical();
				GUILayout.EndArea();
			}
			break;
			
		case menuState.pauseMenu:
			// Pause timer for robot
			if(gameScene == gameScenes.robotMode){
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 0, 100, 100));
					GUILayout.BeginVertical();
						tempTime = stats.getGameTime();
						GUILayout.Box("Time: " + string.Format(Math.Floor(tempTime / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(tempTime / 60), tempTime - Math.Floor(tempTime / 60) * 60));
					GUILayout.EndVertical();
				GUILayout.EndArea();	
			}
			
			// Pause timer for monster
			if(gameScene == gameScenes.monsterMode){
				GUILayout.BeginArea(new Rect(Screen.width / 2 - 50, 0, 100, 100));
					GUILayout.BeginVertical();
						tempTime = monsterTimeAllowed - stats.getGameTime();
						GUILayout.Box("Time: " + string.Format(Math.Floor(tempTime / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(tempTime / 60), tempTime - Math.Floor(tempTime / 60) * 60));
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
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 400));
				GUILayout.BeginVertical();
					GUILayout.Box("Game Over");
					if(gameScene == gameScenes.robotMode)		
						GUILayout.Box("Time Elapsed: " + string.Format(Math.Floor(endTime / 60) > 0 ? "{0:0}:{1:00.00}" : "{1:0.00}", Math.Floor(endTime / 60), endTime - Math.Floor(endTime / 60) * 60));
					GUILayout.Box("Buildings Destroyed: " + stats.getBuildingsDestroyed());
					GUILayout.Box("Cars and People Squished: " + stats.getCarsDestroyed());
					GUILayout.Box("Times Fallen: " + stats.getTimesFallen());
					if(stats.getLongestRun() > -1)
						GUILayout.Box("Longest Run Time: " + string.Format("{0:0.00}", stats.getLongestRun()));
					if(gameScene == gameScenes.robotMode)
						GUILayout.Box("Score: " + stats.getScore("robot"));
					else if(gameScene == gameScenes.monsterMode)
						GUILayout.Box("Score: " + stats.getScore("monster"));
					if(GUILayout.Button(new GUIContent("Restart", "Button"))){		
						if(gameScene == gameScenes.robotMode){		
							Application.LoadLevel("Neo_Tokyo");
							soundSelect.Play();
							stats.resetGame();
							gameScene = gameScenes.robotMode;
							currentState = menuState.blank;
							Time.timeScale = 1;
						}
						else if(gameScene == gameScenes.monsterMode){
							Application.LoadLevel("TokyoMonster");
							soundSelect.Play();
							stats.resetGame();
							gameScene = gameScenes.monsterMode;
							currentState = menuState.blank;
							Time.timeScale = 1;
						}
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

using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
	// Game stats being tracked
	int buildingsDestroyed = 0;
	int timesFallen = 0;
	int carsDestroyed = 0;
	float longestRun = -1.0f;
	float startTime = -1.0f;
	
	// Accessors
	public int getBuildingsDestroyed(){
		return buildingsDestroyed;	
	}
	
	public int getTimesFallen(){
		return timesFallen;
	}
	
	public float getLongestRun(){
		return longestRun;
	}
	
	public float getStartTime(){
		return startTime;	
	}
	
	public int getCarsDestroyed(){
		return carsDestroyed;
	}
	
	// Modifiers
	public void increaseCarsDestroyed(){
		carsDestroyed++;
	}
	
	public void increaseBuildingsDestroyed(){
		buildingsDestroyed++;	
	}
	
	public void increaseTimesFallen(){
		timesFallen++;	
	}
	
	public void checkRunTime(float runTime){
		if(runTime > longestRun){
			longestRun = runTime;	
		}
	}
	
	public void startGame(){
		startTime = Time.time;
	}
	
	public void resetGame(){
		startTime = -1.0f;
		buildingsDestroyed = 0;
		carsDestroyed = 0;
		timesFallen = 0;
		longestRun = -1.0f;
	}
}

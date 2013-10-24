using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
	// Game stats being tracked
	int buildingsDestroyed = 0;
	int timesFallen = 0;
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
	
	// Modifiers
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
	}
}

using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
	// Game stats being tracked
	int buildingsDestroyed = 0;
	int timesFallen = 0;
	float longestRun = -1.0f;
	
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
}

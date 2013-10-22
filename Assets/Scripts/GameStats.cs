using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
	// Game stats being tracked
	int buildingsDestroyed = 0;
	int timesFallen = 0;
	float longestRun = -1.0f;
	
	// Accessors
	int getBuildingsDestroyed(){
		return buildingsDestroyed;	
	}
	
	int getTimesFallen(){
		return timesFallen;
	}
	
	float getLongestRun(){
		return longestRun;	
	}
	
	// Modifiers
	void increaseBuildingsDestroyed(){
		buildingsDestroyed++;	
	}
	
	void increaseTimesFallen(){
		timesFallen++;	
	}
	
	void checkRunTime(float runTime){
		if(runTime > longestRun){
			longestRun = runTime;	
		}
	}
}

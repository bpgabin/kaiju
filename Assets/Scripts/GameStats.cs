using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
	// Game stats being tracked
	int buildingsDestroyed = 0;
	int timesFallen = 0;
	int carsDestroyed = 0;
	float longestRun = -1.0f;
	float startTime = -1.0f;
	float endTime = -1.0f;
	
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
	
	public float getGameTime(){
		return Time.time - startTime;	
	}
	
	public float getFinalTime(){
		if(endTime != -1.0f)
			return endTime - startTime;
		else
			return -1.0f;
	}
	
	public int getScore(string mode){
		float score = 0;
		if(mode == "robot"){
			// Score Weights
			float timeScore = 1;
			float buildingScore = -3;
			float carScore = -5;
			float fallScore = -2;
			
			// Calculate Score
			float time = endTime - startTime;
			score += time * timeScore;
			score += buildingsDestroyed * buildingScore;
			score += carsDestroyed * carScore;
			score += timesFallen * fallScore;
			
			// Limit the lowest score to zero.
			if(score < 0)
				score = 0;
			
			return (int)System.Math.Floor(score);
		}
		else if(mode == "monster"){
			// Score Weights
			float timeScore = -1;
			float buildingScore = 3;
			float carScore = 5;
			float fallScore = 2;
			
			// Calculate Score;
			float time = endTime - startTime;
			score += time * timeScore;
			score += buildingsDestroyed * buildingScore;
			score += carsDestroyed * carScore;
			score += timesFallen * fallScore;
			
			// Limit the lowest score to zero.
			if(score < 0)
				score = 0;
			
			return (int)System.Math.Floor(score);
		}
		else
			return -1;
	}
	
	// Modifiers
	public void endGameTime(){
		endTime = Time.time;
	}
	
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

using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
	// Game stats being tracked
	int buildingsDestroyedValue = 0;
	int buildingsDestroyed = 0;
	int monsterBuildingsDestroyed = 0;
	int timesFallen = 0;
	int carsDestroyed = 0;
	float longestRun = -1.0f;
	float startTime = -1.0f;
	float endTime = -1.0f;
	
	// Accessors
	public int getBuildingsDestroyed(){
		return buildingsDestroyed;	
	}
	
	public int getMonsterBuildingsDestroyed(){
		return monsterBuildingsDestroyed;
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
			float buildingScore = 3;
			float carScore = 5;
			float fallScore = 2;
			
			// Calculate Score
			float time = endTime - startTime;
			float destructionScore = + (buildingScore * buildingsDestroyed) + (carScore * carsDestroyed) + (fallScore * timesFallen);
			score = 10000;
			score *= 30/(time + 30);
			score *= 100/(destructionScore + 100);
			
			// Limit the lowest score to zero.
			if(score < 0)
				score = 0;
			
			return (int)System.Math.Floor(score);
		}
		else if(mode == "monster"){
			// Score Weights
			float timeScore = 1;
			float buildingScore = 3;
			float carScore = 5;
			float fallScore = -2;
			
			// Calculate Score;
			float time = endTime - startTime;
			float destructionScore = + (buildingScore * buildingsDestroyed) + (carScore * carsDestroyed) + (fallScore * timesFallen);
			
			score = time * 10;
			score += destructionScore * 20;
			
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
	
	public void inscreaseMonsterBuildingsDestroyed(){
		monsterBuildingsDestroyed++;
	}
		
	public void increaseBuildingsDestroyedValue(int value){
		buildingsDestroyedValue += value;	
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
		monsterBuildingsDestroyed = 0;
		carsDestroyed = 0;
		timesFallen = 0;
		longestRun = -1.0f;
	}
}

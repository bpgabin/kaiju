using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class GenerateBuildings : MonoBehaviour {
	//public Transform playerPosition;
	public Object building;
	public int rows = 5;
	public int cols = 5;
	public float buildingDistance = 0.5f;
	public float mininmumBuildingHeight = 1.5f;
	public float maximumBuildingHeight = 3.5f;
	
	//public int buildingAmount = 5;
	//public float minimumDistance = 2.0f;
	
	
	//private List<GameObject> buildings = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		// Get buildingSpawner position values.
		float cubeWidth = transform.localScale.x * transform.parent.localScale.x;
		float cubeHeight = transform.localScale.z * transform.parent.localScale.z;
		float xMin = transform.position.x - cubeWidth / 2.0f;
		//float xMax = transform.position.x + cubeWidth / 2.0f;
		float zMin = transform.position.z - cubeHeight / 2.0f;
		//float zMax = transform.position.z + cubeHeight / 2.0f;
		
		// Determine building size
		float buildingWidth = (cubeWidth / rows) - buildingDistance;
		float buildingHeight = (cubeHeight / cols) - buildingDistance;
		
		// Create all the buildings
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < cols; j++){
				// Determine building position and transform
				float buildingX = xMin + (buildingDistance / 2.0f) + (buildingWidth / 2.0f) + (cubeWidth / rows) * i ;
				float buildingZ = zMin + (buildingDistance / 2.0f) + (buildingHeight / 2.0f) + (cubeHeight / cols) * j;
				float buildingYScale = Random.Range(mininmumBuildingHeight, maximumBuildingHeight);
				Vector3 position = new Vector3(buildingX, transform.position.y + buildingYScale, buildingZ);
				
				//Now cast a ray from the computed position downwards and find the highest hit
				RaycastHit[] hits = Physics.RaycastAll(new Ray(position, Vector3.down)); 
				position.y = -100f;
				foreach(RaycastHit hit in hits){
					if (!hit.transform.IsChildOf(transform)){
						position.y = Mathf.Max(position.y, hit.point.y);
					}
				}
				
				position.y += buildingYScale / 2.0f;
				
				// Create the building and assign the values
				GameObject newBuilding = (GameObject)Instantiate(building, position, Quaternion.identity);
				newBuilding.transform.localScale = new Vector3(buildingWidth, buildingYScale, buildingHeight);
			}
		}
		
		/*
		for(int i = 0; i < buildingAmount; i++){
			bool randDone = false;
			Vector3 position = Vector3.zero;
			int attempts = 0;
			while(!randDone && attempts <= 100){
				attempts++;
				float xMin = transform.position.x - transform.localScale.x / 2.0f;
				float xMax = transform.position.x + transform.localScale.x / 2.0f;
				float xPos = Random.Range(xMin, xMax);
				float zMin = transform.position.z - transform.localScale.z / 2.0f;
				float zMax = transform.position.z + transform.localScale.z / 2.0f;
				float zPos = Random.Range(zMin, zMax);
				position = new Vector3(xPos, transform.position.y + 3.6f, zPos);
				Vector3 playerDistance = playerPosition.position - position;
				if(playerDistance.magnitude >= minimumDistance * 2.0f){
					randDone = true;
					if(buildings.Count != 0){
						foreach(GameObject neighbor in buildings){
							Vector3 distance = neighbor.transform.position - position;
							if(distance.magnitude < minimumDistance)
								randDone = false;
						}
					}
				}
			}
			buildings.Add((GameObject)Instantiate(building, position, Quaternion.identity));
		}
		*/
	}
}

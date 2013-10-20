using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateBuildings : MonoBehaviour {
	public Transform playerPosition;
	public Object building;
	public int buildingAmount = 5;
	public float minimumDistance = 2.0f;
	
	
	private List<GameObject> buildings = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
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
	}
}

using UnityEngine;
using System.Collections;

public class BuildingDestruction : MonoBehaviour {
	
	float randX = 0f;
	float randZ = 0f;
	
	// Use this for initialization
	void Start () {
		Invoke("end", 4.0f);
		
		int rand = Random.Range(1, 5);
		
		if(rand == 1){
			randX = 0f;
			randZ = 6.0f;
		}
		else if(rand == 2){
			randX = 0f;
			randZ = -6.0f;
		}
		else if(rand == 3){
			randX = 6.0f;
			randZ = 0f;
		}
		else{
			randX = -6.0f;
			randZ = 0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.y -= 0.5f * transform.localScale.y * Time.deltaTime;
		transform.position = newPosition;
		
		
		transform.Rotate(new Vector3(randX * Time.deltaTime, 0f, randZ * Time.deltaTime));
	}
	
	void end(){
		Destroy(gameObject);	
	}
}

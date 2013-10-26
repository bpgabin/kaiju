using UnityEngine;
using System.Collections;

public class pagodaDestruct : MonoBehaviour {
	
	float randX = 0f;
	float randZ = 0f;
	
	// Use this for initialization
	void Start () {
		Invoke("end", 6.0f);
		
		int rand = Random.Range(1, 3);
		
		if(rand == 1){
			randX = 12.0f;
		}
		else{
			randX = -12.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.y += 1.5f * transform.localScale.y * Time.deltaTime;
		transform.position = newPosition;
		
		
		transform.Rotate(new Vector3(randX * Time.deltaTime, 0f, 0f));
	}
	
	void end(){
		Destroy(gameObject);	
	}
}

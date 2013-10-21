using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "Intersection"){
			Intersection.intersectionTypes type = other.GetComponent<Intersection>().intersectionType;
			int dir;
			switch(type){
			case Intersection.intersectionTypes.fourWay:
				dir = Random.Range(1, 4);
				if(dir == 1)
					transform.Rotate(new Vector3(0, 90, 0));
				else if(dir == 2)
					transform.Rotate (new Vector3(0, -90, 0));
				break;
				
				
			case Intersection.intersectionTypes.threeWay:
				float intersectionRotation = other.transform.rotation.y;
				dir = Random.Range(1, 3);
				if(dir == 1){
					if(transform.rotation.y == intersectionRotation)
						
					transform.Rotate(new Vector3(0, 90, 0));
				}
				else{
					transform.Rotate(new Vector3(0, -90, 0));
				}
				break;
				
				
			case Intersection.intersectionTypes.turnRight:
				transform.Rotate(new Vector3(0, 90, 0));
				break;
			case Intersection.intersectionTypes.turnLeft:
				transform.Rotate(new Vector3(0, -90, 0));
				break;
			}
		}
	}
	
	void LateUpdate(){
		rigidbody.velocity = transform.forward * 2.0f;	
	}
}

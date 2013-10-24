using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {
	public float speed = 2.0f;
	
	float east = 90;
	float west = 270;
	float south = 180;
	float north = 0;
	bool turned = false;
	float closest = 100.0f;
	
	void OnTriggerStay(Collider other){
		float distance = Vector3.Distance(transform.position, other.transform.position);
		if(distance < closest)
			closest = distance;
		else if(!turned && distance > closest){
			turned = true;
			MakeDecision(other);
		}
	}
	
	void OnTriggerExit(Collider other){
		turned = false;
		closest = 100.0f;
	}

	
	void MakeDecision(Collider other){
		if(other.gameObject.name == "Intersection"){
			Intersection.intersectionTypes intersectionType = other.GetComponent<Intersection>().intersectionType;
			if(intersectionType == Intersection.intersectionTypes.fourWay){
				int dir = Random.Range(1, 4);
				if(dir == 1)
					transform.Rotate(0, 90, 0);
				else if(dir == 2)
					transform.Rotate(0, -90, 0);
			}
			else if(System.Math.Round(transform.eulerAngles.y) == north){
				if(System.Math.Round(other.transform.eulerAngles.y) == north){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, 90, 0);	
						else
							transform.Rotate(0, -90, 0);
					}
					else if(intersectionType == Intersection.intersectionTypes.turn)
						transform.Rotate(0, -90, 0);
				}
				else if(System.Math.Round(other.transform.eulerAngles.y) == east){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, -90, 0);
					}
				}
				else if(System.Math.Round(other.transform.eulerAngles.y) == west){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, 90, 0);
					}
					else if(intersectionType == Intersection.intersectionTypes.turn)
						transform.Rotate(0, 90, 0);
				}
			}
			else if(System.Math.Round(transform.eulerAngles.y) == east){
				if(System.Math.Round(other.transform.eulerAngles.y) == north){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, 90, 0);
					}
					else if(intersectionType == Intersection.intersectionTypes.turn)
						transform.Rotate(0, 90, 0);
				}
				else if(System.Math.Round(other.transform.eulerAngles.y) == east){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, 90, 0);
						else
							transform.Rotate(0, -90, 0);
					}
					else if(intersectionType == Intersection.intersectionTypes.turn)
						transform.Rotate(0, -90, 0);
				}
				else if(System.Math.Round(other.transform.eulerAngles.y) == south){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, -90, 0);
					}
				}
			}
			else if(System.Math.Round(transform.eulerAngles.y) == west){
				if(System.Math.Round(other.transform.eulerAngles.y) == north){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, -90, 0);
					}
				}
				else if(System.Math.Round(other.transform.eulerAngles.y) == south){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, 90, 0);
					}
					else if(intersectionType == Intersection.intersectionTypes.turn)
						transform.Rotate(0, 90, 0);
				}
				else if(System.Math.Round(other.transform.eulerAngles.y) == west){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, 90, 0);
						else
							transform.Rotate(0, -90, 0);
					}
					else if(intersectionType == Intersection.intersectionTypes.turn)
						transform.Rotate(0, -90, 0);
				}
			}
			else{
				if(System.Math.Round(other.transform.eulerAngles.y) == south){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, 90, 0);
						else
							transform.Rotate(0, -90, 0);
					}
					else if(intersectionType == Intersection.intersectionTypes.turn)
						transform.Rotate(0, -90, 0);
				}
				else if(System.Math.Round(other.transform.eulerAngles.y) == east){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, 90, 0);
					}
					else if(intersectionType == Intersection.intersectionTypes.turn)
						transform.Rotate(0, 90, 0);
				}
				else if(System.Math.Round(other.transform.eulerAngles.y) == west){
					if(intersectionType == Intersection.intersectionTypes.threeWay){
						int dir = Random.Range(1, 3);
						if(dir == 1)
							transform.Rotate(0, -90, 0);
					}
				}
			}
		}
	}
	
	void LateUpdate(){
		rigidbody.velocity = transform.forward * speed;
	}
}

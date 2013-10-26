using UnityEngine;
using System.Collections;

public class AttackingMonster : MonoBehaviour {
	Animator anim;
	GameObject building;
	public float rotationSpeed = 2.0f;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		//Now cast a ray from the computed position downwards and find the highest hit
		Vector3 newPosition = transform.position;
		RaycastHit[] hits = Physics.RaycastAll(new Ray(newPosition, Vector3.down)); 
		newPosition.y = -100f;
		foreach(RaycastHit hit in hits){
			if (!hit.transform.IsChildOf(transform)){
				newPosition.y = Mathf.Max(newPosition.y, hit.point.y);
			}
		}
		transform.position = newPosition;
		pickNewBuilding();
		anim.SetBool("Walk", true);
	}
	
	void Update() {
		if(building == null)
			pickNewBuilding();
		
		Vector3 direction = (building.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);	
	}
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			GameObject menuObject = GameObject.FindGameObjectWithTag("Menu");
			MainMenu menu = menuObject.GetComponent<MainMenu>();
			menu.EndGame();
		}
		else if(other.gameObject.tag == "Building"){
			if(other.gameObject == building){
				pickNewBuilding();
			}
		}
	}
	
	void pickNewBuilding(){
		GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
		if(buildings.Length != 0){
			int bNum = Random.Range(0, buildings.Length);
			GameObject newBuilding = buildings[bNum];
			building = newBuilding;
		}
	}
}

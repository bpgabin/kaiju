using UnityEngine;
using System.Collections;

public class AttackingRobot : MonoBehaviour {
	
	public Transform monster;
	public float rotationSpeed = 2.0f;
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = (monster.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			GameObject menuObject = GameObject.FindGameObjectWithTag("Menu");
			MainMenu menu = menuObject.GetComponent<MainMenu>();
			menu.EndGame();
		}
	}
}

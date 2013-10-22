using UnityEngine;
using System.Collections;

public class AttackingMonster : MonoBehaviour {
	Animator anim;
	
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
		
		anim.SetBool("Walk", true);
	}
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			GameObject menuObject = GameObject.FindGameObjectWithTag("Menu");
			MainMenu menu = menuObject.GetComponent<MainMenu>();
			menu.EndGame();
		}
		else if(other.gameObject.name == "Building"){
			anim.SetBool("Walk", false);	
		}
	}
}

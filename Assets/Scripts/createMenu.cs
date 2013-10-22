using UnityEngine;
using System.Collections;

public class createMenu : MonoBehaviour {
	public Object menuPrefab;
	
	// Use this for initialization
	void Start(){
		GameObject menu = GameObject.FindGameObjectWithTag("Menu");
		if(menu == null){
			Instantiate(menuPrefab, new Vector3(0, 0, 0), Quaternion.identity);	
		}
	}
}

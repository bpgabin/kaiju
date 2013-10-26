using UnityEngine;
using System.Collections;

public class createMenu : MonoBehaviour {
	public Object menuPrefab;
	public Object kongAPIObject;
	
	// Use this for initialization
	void Start(){
		GameObject menu = GameObject.FindGameObjectWithTag("Menu");
		if(menu == null){
			GameObject newMenu = (GameObject)Instantiate(menuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			newMenu.name = "Menu";
			
			GameObject newAPI = (GameObject)Instantiate(kongAPIObject, new Vector3(0, 0, 0), Quaternion.identity);
			newAPI.name = "KongregateAPI";
			
			newMenu.GetComponent<MainMenu>().kongAPI = newAPI.GetComponent<KongregateAPI>();
		}
	}
}

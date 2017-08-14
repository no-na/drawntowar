using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour {
	
	public GameObject unit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = new Vector3(Input.mousePosition.x,
						Input.mousePosition.y,
						transform.position.z - Camera.main.transform.position.z);
		Vector3 mousePoint = Camera.main.ScreenToWorldPoint(temp);
		this.transform.position = mousePoint;
		
		if(Input.GetMouseButtonDown(0)){
			Spawn(unit,mousePoint);
		}
	}
	
	public void ResetPosition(){
		Vector3 temp = new Vector3(Input.mousePosition.x,
						Input.mousePosition.y,
						transform.position.z - Camera.main.transform.position.z);
		Vector3 mousePoint = Camera.main.ScreenToWorldPoint(temp);
		this.transform.position = mousePoint;
	}
	
	void Spawn(GameObject unit,Vector3 position){
		Instantiate(unit,position,Quaternion.identity);
		gameObject.SetActive(false);
	}
}

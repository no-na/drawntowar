using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour {
	
	public GameObject reticule;
	public GameObject unit;
	
	public void Spawn(){
		reticule.SetActive(true);
		reticule.GetComponent<Reticule>().unit = unit;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

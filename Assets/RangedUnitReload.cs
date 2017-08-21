using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnitReload : MonoBehaviour {
	
	public GameObject projectile;
	
	void Awake(){
		projectile = transform.GetChild(1).gameObject;
	}

	public void Reload(){
		print("RELOAD");
		projectile.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		projectile.transform.localPosition = Vector3.zero;
	}
}

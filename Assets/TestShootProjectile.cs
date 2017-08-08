using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShootProjectile : MonoBehaviour {
	
	public Animator fireAnimator;
	public Rigidbody2D projectile;

	// Use this for initialization
	void Start () {
		fireAnimator = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
		projectile = transform.GetChild(2).GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			fireAnimator.SetTrigger("fire");
			projectile.velocity = Vector2.up * 8f;
		}
	}
}

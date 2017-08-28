using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDistance : MonoBehaviour {
	
	private GameObject target;
	private int damageAmount;
	
	public void SetTarget(GameObject t,int dA){
		target = t;
		damageAmount = dA;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target && Mathf.Abs(Vector2.Distance(transform.position,target.transform.position)) < 0.1f){
			gameObject.SetActive(false);
			if (target){
				if (target.GetComponent<Unit>() != null)
				{
					target.GetComponent<Unit>().GetHit(damageAmount);
				}

				if(target.GetComponent<Health>() != null)
				{
					target.GetComponent<Health>().TakeDamage(damageAmount);
				}
			}
		}
	}
}

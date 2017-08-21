using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDistance : MonoBehaviour {
	
	private Vector2 targetPos;
	private float targetDistance;
	private GameObject target;
	private int damageAmount;
	
	public void SetTarget(GameObject t,int dA){
		targetPos = t.transform.position;
		target = t;
		damageAmount = dA;
	}
	
	public void UpdateTargetDistance(){
		targetDistance = Vector2.Distance(transform.position,targetPos);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(Vector2.Distance(transform.position,targetPos)) < 0.1f){
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

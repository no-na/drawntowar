using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAttack : MonoBehaviour {
	
	public Unit unitController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void AttackFinish()
	{
		unitController.AttackOver();
	}
}

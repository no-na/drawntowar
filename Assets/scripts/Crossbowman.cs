using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbowman : Unit
{
	private GameObject projectile;
	private FireDistance fireDistance;


    private Animator weaponAnimator;
	
	void Awake()
	{
		weaponAnimator = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
		projectile = transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject;
		fireDistance = projectile.GetComponent<FireDistance>();
	}
	
	public override void DoAttack()
    {
        print("Doing attack");
		weaponAnimator.SetTrigger("fire");
		
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, strikeDistance, willAttackUnit.value);
        if (hit.collider != null)
        {
			Vector2 projectilePos = projectile.transform.position;
			Vector2 targetPos = hit.collider.gameObject.transform.position;
			if(hit.collider.gameObject != null){
				fireDistance.SetTarget(hit.collider.gameObject,damageAmount);
			
				Debug.DrawLine(projectilePos,targetPos,Color.red);
		
				//projectile.transform.LookAt(hit.collider.gameObject.transform);
				Vector2 vel = new Vector2((targetPos.x - projectilePos.x),(targetPos.y-projectilePos.y));
				vel.Normalize();
				projectile.GetComponent<Rigidbody2D>().velocity = vel * 10f;
				//projectile.GetComponent<Rigidbody2D>().velocity = Vector2.MoveTowards(transform.position,hit.collider.gameObject.transform.localPosition,3f);// * 3f;
			}
			
            
        }
    }
	

}

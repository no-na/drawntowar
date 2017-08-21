using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbowman : Unit
{
	public GameObject projectile;


    private Animator weaponAnimator;
	
	void Awake()
	{
		weaponAnimator = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
		projectile = transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject;
	}
	
	public override void DoAttack()
    {
        print("Doing attack");
		weaponAnimator.SetTrigger("fire");
		
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, strikeDistance, willAttackUnit.value);
        if (hit.collider != null)
        {
			projectile.GetComponent<Rigidbody2D>().velocity = Vector2.MoveTowards(transform.position,hit.collider.gameObject.transform.position,3f) * 3f;
			
            if (hit.collider.gameObject.GetComponent<Unit>() != null)
            {
                hit.collider.gameObject.GetComponent<Unit>().GetHit(damageAmount);
            }

            if(hit.collider.gameObject.GetComponent<Health>() != null)
            {
                hit.collider.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
            }
        }
    }
	

}

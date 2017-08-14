using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
	private Animator spearAnimator;
	
	void Awake()
	{
		spearAnimator = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
	}
	
	public override void DoAttack()
    {
        print("Doing attack");
		spearAnimator.Play("spear-strike",1,0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, strikeDistance, willAttackUnit.value);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Unit>() != null)
            {
                hit.collider.gameObject.GetComponent<Unit>().GetHit();
            }
        }
    }
}

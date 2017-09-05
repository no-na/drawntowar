using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Unit
{
    public float aoeRange;

    private Animator spearAnimator;
	public GameObject explosion;

    void Awake()
    {
        spearAnimator = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
    }

    public override void DoAttack()
    {
        print("Doing attack");
        spearAnimator.Play("wand-strike", 1, 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, strikeDistance, willAttackUnit.value);
        if (hit.collider != null)
        {	
			GameObject boom = Instantiate(explosion,hit.collider.gameObject.transform.position,Quaternion.identity);
			Destroy(boom,1f);
            Collider2D[] targets = Physics2D.OverlapCircleAll(hit.collider.gameObject.transform.position, aoeRange, willAttackUnit.value);
            foreach (Collider2D cols in targets)
            {
                print(cols.gameObject.tag);
                if (cols != null)
                {
                    if (cols.gameObject.GetComponent<Unit>() != null)
                    {
                        cols.gameObject.GetComponent<Unit>().GetHit(damageAmount);
                    }

                    if (cols.gameObject.GetComponent<Health>() != null)
                    {
                        attCastle = true;
                        cols.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
                    }
                }
            }
        }
    }
}
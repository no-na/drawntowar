using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEUnit : Unit
{
    public float aoeRange;

    private Animator spearAnimator;

    void Awake()
    {
        spearAnimator = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
    }

    public override void DoAttack()
    {
        print("Doing attack");
        spearAnimator.Play("spear-strike", 1, 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, strikeDistance, willAttackUnit.value);
        if (hit.collider != null)
        {
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
                        cols.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
                    }
                }
            }
        }
    }
}
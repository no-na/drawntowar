﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, Unit_I
{
    private enum STATE { WANDER, CHASE, ATTACK, DIE };

    public float wanderSpeed;
    public float chaseSpeed;
    public float minWanderTime;
    public float maxWanderTime;
    public LayerMask willAttackUnit;
    public float sightDistance;
    public float strikeDistance;
    public float rotationSpeed;
    public string targetTag;
    public int health;

    private STATE currentState;

    public Animator myAnim;

    private float currWanderTime;
    private Vector2 currDirection;
    private GameObject currTarget;
    private Rigidbody2D myRB;

    // Use this for initialization
    void Start ()
    {
        myRB = GetComponent<Rigidbody2D>();
        currTarget = GameObject.FindWithTag(targetTag);
        EnterStateWander();

    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (currentState)
        {
            case STATE.WANDER:
                UpdateWander();
                break;

            case STATE.CHASE:
                UpdateChase();
                break;

            case STATE.ATTACK:
                UpdateAttack();
                break;

            case STATE.DIE:
                UpdateDie();
                break;
        }

    }

    private void EnterStateWander()
    {

        currentState = STATE.WANDER;
        //Vector2 targetDirection = (pcm.transform.position + transform.forward - transform.position).normalized;//test
        //currDirection = Vector3.RotateTowards(currDirection.normalized, targetDirection, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;//test
        currDirection = (currTarget.transform.position  - transform.position).normalized;//test
        //currDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        currWanderTime = Random.Range(minWanderTime, maxWanderTime);
    }

    private void UpdateWander()
    {
        myRB.velocity = currDirection * wanderSpeed;
        currWanderTime -= Time.deltaTime;
        //myAnim.transform.localEulerAngles = Vector3.forward * Mathf.Atan2(currDirection.y, currDirection.x);//test
        //myAnim.transform.localEulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2(currDirection.y, currDirection.x)) - 90f);
        if (currWanderTime <= 0f)
        {
            EnterStateWander();
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, sightDistance, willAttackUnit.value);
        if (hit.collider != null)
        {
            currTarget = hit.collider.gameObject;
            EnterStateChase(hit.collider.gameObject);
        }
    }

    private void EnterStateChase(GameObject target)
    {
        currentState = STATE.CHASE;
        //currTarget = GameObject.FindWithTag("Player");
        //currTarget = target;
        currDirection = (target.transform.position + target.transform.forward - transform.position).normalized;
    }

    private void UpdateChase()
    {
        print("Found");
        Vector2 targetDirection = (currTarget.transform.position - transform.position).normalized;
        currDirection = Vector3.RotateTowards(currDirection.normalized, targetDirection, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
        myRB.velocity = currDirection * chaseSpeed;
        //myAnim.transform.localEulerAngles = Vector3.forward * Mathf.Atan2(currDirection.y, currDirection.x);
        //myAnim.transform.localEulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2(currDirection.y, currDirection.x)) - 90f);
        float targetDistance = Vector3.Distance(currTarget.transform.position, transform.position);
        if (targetDistance <= strikeDistance)
        {
            print("Entering Attack");
            EnterStateAttack();
        }
        else if (targetDistance > sightDistance || Vector2.Angle(currDirection, targetDirection) > 30f)
        {
            currentState = STATE.WANDER;
        }
    }

    private void EnterStateAttack()
    {
        currentState = STATE.ATTACK;
        myRB.velocity = Vector2.zero;
        print("In attack");
        DoAttack();
        //myAnim.SetTrigger("attack");
    }

    private void UpdateAttack()
    {

    }


    public void DoAttack()
    {
        print("Doing attack");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, strikeDistance, willAttackUnit.value);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Unit>() != null)
            {
                hit.collider.gameObject.GetComponent<Unit>().GetHit();
            }
        }
    }

    public void GetHit()
    {
        print("HIT");
        health--;
        if (health <= 0)
        {
            myRB.velocity = Vector2.zero;
            Die();
        }
    }

    public void AttackOver()
    {
        EnterStateWander();
    }

    public void Die()
    {
        EnterStateDie();
    }

    private void EnterStateDie()
    {
        print("Entered Death");
        currentState = STATE.DIE;
        //myAnim.SetTrigger("die");
        myRB.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    private void UpdateDie()
    {
        //if (myAnim.GetComponent<SpriteRenderer>().sprite == null)
        //{
            Destroy(gameObject);
        //}
    }
}

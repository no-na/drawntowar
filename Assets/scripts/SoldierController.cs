using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour
{

    private enum STATE { GUARD, ASSAULT, ATTACK, DIE };

    // New vars added
    public int health;
    public int weaponDamage;
    public float assaultSpeed;
    public float detectionRadius;
    public int defense;
    //Unit type variable


    public float chaseSpeed;
    
    public Animator myAnimator;
    public LayerMask willAttackLayer;
    public float strikeDistance;
    public float rotationSpeed;


    private STATE _currentState;
    private Vector2 _currentDirection;
    private GameObject _currentTarget;
    private Rigidbody2D _myRigidbody2D;


    // Use this for initialization
    void Start()
    {
        //Create soldier, cause it to assault the castle
        //Need to create secondary start condition, for when guarding soldiers get spawned.  Maybe do it based off of layer?
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        EnterStateAssault();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            /*
            case STATE.WANDER:
                UpdateWander();
                break;
            case STATE.CHASE:
                UpdateChase();
                break;
        */
            //Defending soldiers will use this state.  Constant checking within detection radius to spot enemies, if enemy found, move to assault.
            case STATE.GUARD:
                UpdateGuard();
                break;
            case STATE.ASSAULT:
                UpdateAssault();
                break;
            case STATE.ATTACK:
                UpdateAttack();
                break;
            case STATE.DIE:
                UpdateDie();
                break;
        }
    }

    private void EnterStateAssault()
    {
        _currentState = STATE.ASSAULT;
        _currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    //Leave for now, update 
    private void UpdateWander()
    {
        myAnimator.transform.localEulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2(_currentDirection.y, _currentDirection.x)) - 90f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _currentDirection, detectionRadius, willAttackLayer.value);
      /*  if (hit.collider != null)
        {
            EnterStateChase(hit.collider.gameObject);
        }*/
    }
    
    private void UpdateGuard()
    {

    }

    private void UpdateAssault()
    {

    }
/*
    private void EnterStateChase(GameObject target)
    {
        _currentState = STATE.CHASE;
        _currentTarget = target;
        _currentDirection = (target.transform.position - transform.position).normalized;
    }
    */
    private void UpdateChase()
    {
        Vector2 targetDirection = (_currentTarget.transform.position - transform.position).normalized;
        _currentDirection = Vector3.RotateTowards(_currentDirection.normalized, targetDirection, rotationSpeed = Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
        _myRigidbody2D.velocity = _currentDirection * chaseSpeed;
        myAnimator.transform.localEulerAngles = Vector3.forward * Mathf.Atan2(_currentDirection.y, _currentDirection.x);
        myAnimator.transform.localEulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2(_currentDirection.y, _currentDirection.x)) - 90f);
        float targetDistance = Vector3.Distance(_currentTarget.transform.position, transform.position);
        if (targetDistance <= strikeDistance)
        {
            EnterStateAttack();
        }
        else if (targetDistance > detectionRadius || Vector2.Angle(_currentDirection, targetDirection) > 30f)
        {
            _currentState = STATE.ASSAULT;
        }
    }

    private void EnterStateAttack()
    {
        _currentState = STATE.ATTACK;
        _myRigidbody2D.velocity = Vector2.zero;
        myAnimator.SetTrigger("attack");
    }

    private void UpdateAttack()
    {

    }

    /// <summary>
    /// Rewrite to do an attack against a soldier, not a player.
    /// </summary>
    public void DoAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _currentDirection, strikeDistance, willAttackLayer.value);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Health>() != null)
                hit.collider.gameObject.GetComponent<Health>().TakeDamage(weaponDamage);
        }
    }

    public void AttackOver()
    {
        EnterStateAssault();
    }

    public void Die()
    {
        EnterStateDie();
    }

    private void EnterStateDie()
    {
        _currentState = STATE.DIE;
        myAnimator.SetTrigger("die");
        _myRigidbody2D.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    private void UpdateDie()
    {
        if (myAnimator.GetComponent<SpriteRenderer>().sprite == null) Destroy(gameObject);
    }
}

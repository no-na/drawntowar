using UnityEngine;
using System.Collections;

public class ScorpionController1 : MonoBehaviour {

    private enum STATE { WANDER, CHASE, ATTACK, DIE };

	public float wanderSpeed;
	public float chaseSpeed;
	public float minWanderTime;
	public float maxWanderTime;
	public Animator myAnimator;
	public LayerMask willAttackLayer;
	public float sightDistance;
	public float strikeDistance;
	public float rotationSpeed;

    private STATE _currentState;
	private float _currentWanderTime;
	private Vector2 _currentDirection;
	private GameObject _currentTarget;
	private Rigidbody2D _myRigidbody2D;

	// Use this for initialization
	void Start () {
		_myRigidbody2D = GetComponent<Rigidbody2D>();
		EnterStateWander ();
    }
	
	// Update is called once per frame
	void Update () {
		switch (_currentState) {
		case STATE.WANDER:
			UpdateWander ();
			break;
		case STATE.CHASE:
			UpdateChase ();
			break;
		case STATE.ATTACK:
			UpdateAttack();
			break;
		case STATE.DIE:
			UpdateDie ();
			break;
		}
    }

    private void EnterStateWander() {
        _currentState = STATE.WANDER;
		_currentDirection = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized;
		_currentWanderTime = Random.Range (minWanderTime, maxWanderTime);

    }

    private void UpdateWander() {
		_myRigidbody2D.velocity = _currentDirection * wanderSpeed;
		_currentWanderTime -= Time.deltaTime;
		myAnimator.transform.localEulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2 (_currentDirection.y, _currentDirection.x)) - 90f);
		if (_currentWanderTime <= 0)
			EnterStateWander();
		RaycastHit2D hit = Physics2D.Raycast (transform.position, _currentDirection, sightDistance, willAttackLayer.value);
		if (hit.collider != null) {
			EnterStateChase (hit.collider.gameObject);
		}
    }

	private void EnterStateChase(GameObject target) {
		_currentState = STATE.CHASE;
		_currentTarget = target;
		_currentDirection = (target.transform.position - transform.position).normalized;
    }

    private void UpdateChase() {
		Vector2 targetDirection = (_currentTarget.transform.position - transform.position).normalized;
		_currentDirection = Vector3.RotateTowards (_currentDirection.normalized, targetDirection, rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
		_myRigidbody2D.velocity = _currentDirection * chaseSpeed;
		myAnimator.transform.localEulerAngles = Vector3.forward * Mathf.Atan2 (_currentDirection.y, _currentDirection.x);
		myAnimator.transform.localEulerAngles = Vector3.forward * ((Mathf.Rad2Deg * Mathf.Atan2 (_currentDirection.y, _currentDirection.x)) - 90f);
		float targetDistance = Vector3.Distance (_currentTarget.transform.position, transform.position);
		if (targetDistance <= strikeDistance) {
			EnterStateAttack ();
		} else if (targetDistance > sightDistance || Vector2.Angle (_currentDirection, targetDirection) > 30f) {
			_currentState = STATE.WANDER;
		}
    }

    private void EnterStateAttack() {
        _currentState = STATE.ATTACK;
		_myRigidbody2D.velocity = Vector2.zero;
		myAnimator.SetTrigger ("attack");
    }

    private void UpdateAttack() {

    }

    public void DoAttack() {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, _currentDirection, strikeDistance, willAttackLayer.value);
		if(hit.collider != null){
			//if(hit.collider.gameObject.GetComponent<PlayerHealth>() != null)
				//hit.collider.gameObject.GetComponent<PlayerHealth>().GetHit();
		}
    }

    public void AttackOver() {
		EnterStateWander ();
    }

    public void Die() {
        EnterStateDie();
    }

    private void EnterStateDie() {
        _currentState = STATE.DIE;
		myAnimator.SetTrigger("die");
		_myRigidbody2D.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
    }

    private void UpdateDie() {
		if (myAnimator.GetComponent<SpriteRenderer> ().sprite == null)
			Destroy (gameObject);
    }
}

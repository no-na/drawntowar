using UnityEngine;
using System.Collections;

public class ScorpionController : MonoBehaviour {

    private enum STATE { WANDER, CHASE };

	[SerializeField]
	public static float speed = 2.0f;
	public LayerMask willAttackLayer;

	[SerializeField]
    private STATE _currentState;
	private GameObject _currentTarget;
	private Rigidbody2D _myRigidbody2D;
	private Vector2 _currentDirection;

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
		}
    }

    private void EnterStateWander() {
        _currentState = STATE.WANDER;
		_currentDirection = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized;

    }
	
	public void SpeedUp(){
		speed += 0.05f;
	}
	
	public void SpeedReset(){
		speed = 0.05f;
	}
	
	public void SpeedHardReset(){
		speed = 0.00f;
	}

    private void UpdateWander() {
		_myRigidbody2D.velocity = _currentDirection * speed;
    }
	
	void OnCollisionEnter2D (Collision2D col){
		if(col.collider.tag == "v_wall" && _currentState == STATE.WANDER){
			Vector2 bounceVector = new Vector2 (_currentDirection.x,_currentDirection.y*-1f);
			_currentDirection = bounceVector;
		}
		else if(col.collider.tag == "h_wall" && _currentState == STATE.WANDER){
			Vector2 bounceVector = new Vector2 (_currentDirection.x*-1f,_currentDirection.y);
			_currentDirection = bounceVector;
		}
	}

	public void EnterStateChase(GameObject target) {
		gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		_currentState = STATE.CHASE;
		_currentTarget = target;
		_currentDirection = (target.transform.position - transform.position).normalized;
    }

    private void UpdateChase() {
		Vector2 targetDirection = (_currentTarget.transform.position - transform.position).normalized;
		_currentDirection = Vector3.RotateTowards (_currentDirection.normalized, targetDirection, speed *1.3f * Time.deltaTime, 0f).normalized;
		_myRigidbody2D.velocity = _currentDirection * speed * 1.2f;
		float targetDistance = Vector3.Distance (_currentTarget.transform.position, transform.position);
    }
}

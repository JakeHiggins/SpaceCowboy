using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpForce;
	public float moveForce;
	public float maxMoveVelocity;

	public Sprite rightFace, leftFace;

	private bool _hasDoubleJumped;
	private bool _hasJumped;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movement = new Vector2();

		if(Input.GetKeyDown (KeyCode.Space)) {

			if(!_hasJumped) {
				movement.y += jumpForce;
				_hasJumped = true;
				Debug.Log ("Jump");
			}
			else {
				if(!_hasDoubleJumped) {
					movement.y += jumpForce;
					_hasDoubleJumped = true;
					Debug.Log ("Double Jump");
				}
			}
		}
		if(Input.GetKey (KeyCode.A)) {
			movement.x -= moveForce;
			GetComponent<SpriteRenderer>().sprite = leftFace;
		}

		if(Input.GetKey (KeyCode.D)) {
			movement.x += moveForce;
			GetComponent<SpriteRenderer>().sprite = rightFace;
		}

		this.gameObject.rigidbody2D.AddForce(movement);

		ClampVelocity();
	}

	public void ClampVelocity() {
		Vector2 currentVelocity = this.gameObject.rigidbody2D.velocity;
		if(currentVelocity.x > maxMoveVelocity)
			currentVelocity.x = maxMoveVelocity;
		if(currentVelocity.x < -maxMoveVelocity)
			currentVelocity.x = -maxMoveVelocity;

		this.gameObject.rigidbody2D.velocity = currentVelocity;
	}

	public void CameraFollow() {

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Platform") {
			_hasJumped = false;
			_hasDoubleJumped = false;
		}
	}
}

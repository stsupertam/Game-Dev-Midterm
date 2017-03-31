using UnityEngine;
using System.Collections;

public class PlayerControllerPhysicBase : MonoBehaviour {

	public float acc;
	public float maxSpeed;

	private Rigidbody rb;

	Vector3 movement;
	bool isGrounded;
	float sqrMaxSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		isGrounded = true;
		sqrMaxSpeed = maxSpeed * maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (rb.velocity.y == 0 && !isGrounded)
			isGrounded = true;
	}

	void FixedUpdate(){
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		Move (h, v);
		Jump ();
		/*
		if (isGrounded) {
			Vector3 friction = 3 * Vector3.Normalize (new Vector3 (-rb.velocity.x, 0, -rb.velocity.z));
			rb.AddForce (friction);
		}*/
	}

	void Move(float h, float v){

		movement = Vector3.Normalize(new Vector3 (h, 0, v)) * acc;

		if (rb.velocity.sqrMagnitude >= sqrMaxSpeed) {
			Vector3 resistForce = Vector3.Normalize (new Vector3 (-rb.velocity.x, 0, -rb.velocity.z)) * maxSpeed;
			rb.AddForce (resistForce);
			//rb.velocity = rb.velocity.normalized * maxSpeed;
		} else
			rb.AddForce (movement);

	}

	void Jump(){
		if (Input.GetButton ("Jump") && isGrounded) {
			isGrounded = false;
			rb.AddForce (new Vector3(0.0f,1.0f,0.0f) * 300);
		}
	}
}

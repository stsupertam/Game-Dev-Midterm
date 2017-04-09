using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {

	public float acc;
	public float maxSpeed;
	public GameObject shot;
	public float fireInterval = 0.2f;
	public GameObject grenade;

	private Rigidbody rb;

	Vector3 movement;
	bool isJumpable;
	float sqrMaxSpeed;
	float timer;
	float maxRayLength = 100f;
	int sceneMask;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		isJumpable = false;
		sqrMaxSpeed = maxSpeed * maxSpeed;
		sceneMask = LayerMask.GetMask ("Scene");
	}
	
	// Update is called once per frame
	void Update () {
		//checkGrounded ();

		timer += Time.deltaTime;
		if (Input.GetButton("Fire1") && timer > fireInterval) {
			Shoot();
			timer = 0f;
		}
		else if (Input.GetButton("Fire2") && timer > fireInterval) {
			ThrowBomb();
			timer = 0f;
		}

		//Debug.Log (transform.TransformPoint(new Vector3 (0, 0, 0)));


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

		//Debug.Log (rb.velocity.y);
		//if (rb.velocity.y <= -4.7f) {
			//Debug.Log ("Dead");
			//UnityEditor.EditorApplication.Exit(0);
		//}
	}

    float get_angle(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        return angle;
    }
	void Move(float h, float v){

		movement = Vector3.Normalize(new Vector3 (h, 0, v)) * acc;

		Vector3 xzVelo = rb.velocity;
		xzVelo.y = 0f;

		if (xzVelo.sqrMagnitude >= sqrMaxSpeed) {
			Vector3 resistForce = Vector3.Normalize (new Vector3 (-rb.velocity.x, 0, -rb.velocity.z)) * maxSpeed;
			rb.AddForce (resistForce);
			//rb.velocity = rb.velocity.normalized * maxSpeed;
		} else
			rb.AddForce (movement);

	}

	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "Floor") {
			isJumpable = true;
			//Debug.Log ("touch");
		}
	}

	void Jump(){
		if (Input.GetButton ("Jump") && isJumpable) {
			isJumpable = false;
			rb.AddForce (Vector3.up * 700);
		}
	}

	void Shoot(){
    	if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
    		Instantiate(shot,this.gameObject.transform.position,Quaternion.Euler(new Vector3(0, -get_angle() + 90, 0)));
       	}

//		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit floorHit;
//		Vector3 playerToMouse = Vector3.zero;
//		Vector3 shotSpawnPos = transform.position + new Vector3 (0, 0.83f, 0);
//
//		if (Physics.Raycast (camRay, out floorHit, maxRayLength, sceneMask)) {
//			//Debug.Log (floorHit.point);
//			playerToMouse = floorHit.point - shotSpawnPos;
//			playerToMouse.y += 0.5f;
//			//Debug.Log (playerToMouse);
//		}
		//Instantiate(shot,this.gameObject.transform.position,Quaternion.Euler(new Vector3(0, -get_angle() + 90, 0)));
		//BulletController bullet = Instantiate (shot, shotSpawnPos, Quaternion.identity).GetComponent<BulletController>() as BulletController;
		//bullet.setDir (playerToMouse);
		//Debug.DrawRay (shotSpawnPos, playerToMouse);

	}

	void ThrowBomb(){
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		Vector3 playerToMouse = Vector3.zero;
		Vector3 shotSpawnPos = transform.position + new Vector3 (0, 0.83f, 0);

		if (Physics.Raycast (camRay, out floorHit, maxRayLength, sceneMask)) {
			playerToMouse = floorHit.point - shotSpawnPos;

			playerToMouse.y += 0.5f;
			//Debug.Log (playerToMouse);
		}

		BombController bomb = Instantiate (grenade, shotSpawnPos, Quaternion.identity).GetComponent<BombController>() as BombController;
		bomb.setDir (playerToMouse);
	}
}

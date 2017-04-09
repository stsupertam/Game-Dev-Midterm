using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float speed;
	public float shotYield;
	private Rigidbody rb;

	float timer;
	float lifeTime;
	Vector3 direction = Vector3.zero;
	bool isDirAssigned = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		lifeTime = shotYield / speed;
	}
	
	// Update is called once per frame
	void Update () {
//		timer += Time.deltaTime;
		transform.position += transform.forward * Time.deltaTime * 10f * speed;
//		if (timer > lifeTime)
//			Destroy (this.gameObject);

//		if (direction != Vector3.zero && !isDirAssigned) {
//			rb.velocity = direction.normalized * speed;
//			isDirAssigned = true;
//		}

	}

	void OnTriggerEnter(Collider col){
//		if (col.tag != "Player") {
//			if (col.tag == "Enemy")
//				Destroy (col.gameObject);
//			Destroy (this.gameObject);
//		}
	}

	public void setDir(Vector3 dir){
		if (dir == Vector3.zero)
			dir = Vector3.forward;
		direction = dir;
	}
}

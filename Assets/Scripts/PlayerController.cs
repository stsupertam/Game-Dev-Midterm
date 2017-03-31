using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 6;

    Rigidbody myRigidbody;
    Camera viewCamera;
    Vector3 velocity;

    void Start () {
        myRigidbody = GetComponent<Rigidbody> ();
        viewCamera = Camera.main;
    }

    void Update () {
        velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;
    }

    void FixedUpdate() {
        myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);
    }
}

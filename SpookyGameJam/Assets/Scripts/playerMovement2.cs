using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement2 : MonoBehaviour {

	public float moveSpeed;
	public float crouchSpeed;
	public Rigidbody rb;
	SpriteScript ss;
	private char direction;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		ss= this.GetComponent<SpriteScript>();
		this.direction = 't';
	}

	// Update is called once per frame
	void Update () {
		this.setIdle();
		Vector3 velocityVector = Vector3.zero;
		if (Input.GetKey(KeyCode.UpArrow)){
			velocityVector.z += moveSpeed;
			ss.SetAnimation("Walking Away");
			this.direction = 'a';
		} else if (Input.GetKey(KeyCode.DownArrow)){
			velocityVector.z -= moveSpeed;
			ss.SetAnimation("Walking Toward");
			this.direction = 't';
		}
		if (Input.GetKey(KeyCode.RightArrow)){
			velocityVector.x += moveSpeed;
			ss.SetAnimation("Walking Right");
			this.direction = 'r';
		} else if (Input.GetKey(KeyCode.LeftArrow)){
			velocityVector.x -= moveSpeed;
			ss.SetAnimation("Walking Left");
			this.direction = 'l';
		}
		ss.SetFramesPerSecond(8f);
		rb.velocity = velocityVector;
	}

	private void setIdle() {
		if (this.direction == 'a') {
			ss.SetAnimation("Idle Away");
		} 
		 if (this.direction == 't') {
			ss.SetAnimation("Idle Towards");
		} 
		 if (this.direction == 'l') {
			ss.SetAnimation("Idle Left");
		} 
		 if (this.direction == 'r') {
			ss.SetAnimation("Idle Right");
		}
	}
}

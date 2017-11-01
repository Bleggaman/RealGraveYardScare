using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpriteManager : MonoBehaviour {

	SpriteScript ss;
	Rigidbody rb;
	GhostMovement state;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		state = this.GetComponent<GhostMovement>();
		Invoke("thisWorksIGuess", 0.02f);
	}
	
	void thisWorksIGuess() {
		ss= this.GetComponent<SpriteScript>();
		ss.SetAnimation("Idle");
	}

	void Update() {
		if (state.scared) {
			//use a different animator to get the animation of ghost pooping candy
			//wait some time
			ss.SetAnimation("cry");
			ss.SetFramesPerSecond(2f);
			//wait 2 seconds
			ss.SetAnimation("poof");
			ss.SetFramesPerSecond(5f);
			//wait 1 second
			Destroy(this);
		}

		Vector3 vel = rb.velocity;
		if (vel.z > 0) {
			ss.SetAnimation("away");
		} else if (vel.z < 0) {
			ss.SetAnimation("towards");
		}

		if (vel.x > 0) {
			ss.SetAnimation("right");
		} else if (vel.x < 0) {
			ss.SetAnimation("left");
		}
		ss.SetFramesPerSecond(4f);
		if (vel.z == 0 && vel.x == 0) {
			ss.SetAnimation("Idle");
			ss.SetFramesPerSecond(3f);
		}

	}
}

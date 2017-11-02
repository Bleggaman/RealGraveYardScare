using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpriteManager : MonoBehaviour {

	SpriteScript ss;
	Rigidbody rb;
	GhostMovement state;
	GhostTestAI ai;
	bool animating;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		state = this.GetComponent<GhostMovement>();
		ai = this.GetComponent<GhostTestAI>();
		Invoke("thisWorksIGuess", 0.02f);
	}
	
	void thisWorksIGuess() {
		ss= this.GetComponent<SpriteScript>();
		ss.SetAnimation("Idle");
	}

	void Update() {

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

	IEnumerator animWait(float x){
		animating = true;
		yield return new WaitForSeconds(x);
		animating = false;
	}

	public void gotScare() {
		if (!animating) {
		ss.SetAnimation("cry");
		ss.SetFramesPerSecond(2f);
		
			StartCoroutine(animWait(2f));
		
		ss.SetAnimation("poof");
		ss.SetFramesPerSecond(5f);
		
			StartCoroutine(animWait(1f));
		
		//move the ghost 
		transform.position = new Vector3 (0f,0f, -50f);
		}
	}
}

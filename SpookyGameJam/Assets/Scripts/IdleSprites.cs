using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSprites : MonoBehaviour {
	SpriteScript ss;
	// Use this for initialization
	void Start () {
		Invoke("thisWorksIGuess", 0.02f);
	}
	
	void thisWorksIGuess() {
		ss= this.GetComponent<SpriteScript>();
		ss.SetAnimation("Idle");
	}
}

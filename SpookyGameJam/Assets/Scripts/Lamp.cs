using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : ScareMapObject, iActivate  {

	Collider collider;
	GameObject sender;
	bool lampOn = true; 
	GameObject lampChild;
	SpriteScript ss;
	List<GameObject> inRange;

	public Lamp (){
		scareValue = 3;

	}

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider> ();
		lampChild = transform.GetChild (0).gameObject;
		Invoke("thisWorksIGuess", 0.02f);
	}
	
	void thisWorksIGuess() {
		ss= this.GetComponent<SpriteScript>();
		ss.SetAnimation("on");
	}

	public override void activate(GameObject characterActivated){
		sender = characterActivated;
		lampOn = !lampOn;
		lampChild.SetActive (lampOn);
		if (lampOn) { 
			ss.SetAnimation("on");
			foreach (GameObject go in this.inRange) {
				if (!go.Equals(characterActivated)) {
					scare (sender, go);
				}
			}
		} else {
			ss.SetAnimation("off");
		}
	}

	void OnTriggerEnter(Collider other){
		this.inRange.Add(other.gameObject);
	}

	
	void OnTriggerExit(Collider other){
		this.inRange.Remove(other.gameObject);
	}
}

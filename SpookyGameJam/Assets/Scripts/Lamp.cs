using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : ScareMapObject, iActivate, iLightable  {

	Collider collider;
	GameObject sender;
	GameObject player;
	public bool lampOn = true; 
	GameObject lampChild;
	SpriteScript ss;
	public List<GameObject> inRange;
	public float distToLampLit = 3;

	public Lamp (){
		scareValue = 3;

	}

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider> ();
		lampChild = transform.GetChild (0).gameObject;
		Invoke("thisWorksIGuess", 0.02f);
		player = GameObject.Find ("Player");
	}
	
	void thisWorksIGuess() {
		ss= this.GetComponent<SpriteScript>();
		ss.SetAnimation("on");
	}

	public override void activate(GameObject characterActivated){
		Debug.Log ("LAMP ON by " + characterActivated);
		sender = characterActivated;
		lampOn = !lampOn;
		lampChild.SetActive (lampOn);
		if (lampOn) { 
			ss.SetAnimation("on");
			//foreach (GameObject go in this.inRange) {
			//	if (!go.Equals(characterActivated)) {
					//scare (sender, go);  doesn't scare right now
			//	}
			//}
		} else {
			ss.SetAnimation("off");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player") || other.CompareTag("Ghost")){
		this.inRange.Add(other.gameObject);
		}
	}


	void OnTriggerExit(Collider other){
		if (other.CompareTag("Player") || other.CompareTag("Ghost")){
			this.inRange.Remove(other.gameObject);
		}		
	}

	#region iLightable implementation

	public bool playerIsLit ()
	{
		Vector3 lightPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 3);
		if (lampOn && Vector3.Distance (lightPos, player.transform.position) < distToLampLit) {
			return true;
		}
		return false;
	}

	#endregion
}

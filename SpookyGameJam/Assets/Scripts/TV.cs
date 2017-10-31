using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : ScareMapObject, iActivate, iLightable  {

	Collider collider;
	GameObject sender;
	GameObject player;
	public bool TVOn = true; 
	GameObject TVChild1;
	GameObject TVChild2;
	SpriteScript ss;
	public List<GameObject> inRange;
	public float distToTVLit = 2;

	public TV (){
		scareValue = 3;

	}

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider> ();
		TVChild1 = transform.GetChild (0).gameObject;
		TVChild2 = transform.GetChild (1).gameObject;
		player = GameObject.Find ("Player");
	}

	void thisWorksIGuess() {
		ss= this.GetComponent<SpriteScript>();
		ss.SetAnimation("on");
	}

	public override void activate(GameObject characterActivated){
		Debug.Log ("LAMP ON");
		sender = characterActivated;
		TVOn = !TVOn;
		TVChild1.SetActive (TVOn);
		TVChild2.SetActive (TVOn);
		if (TVOn) { 

			foreach (GameObject go in this.inRange) {
				if (!go.Equals(characterActivated)) {
					//scare (sender, go);
				}
			}
		} else {
			
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
		if (TVOn && Vector3.Distance (gameObject.transform.position, player.transform.position) < distToTVLit) {
			return true;
		}
		return false;
	}

	#endregion
}

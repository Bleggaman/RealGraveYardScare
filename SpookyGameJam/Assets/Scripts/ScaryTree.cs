using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryTree : ScareMapObject, iActivate  {

	Collider collider;
	GameObject sender;
	bool treeOn = true; 
	GameObject treeChild;

	public ScaryTree (){
		scareValue = 3;

	}

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider> ();
		treeChild = transform.GetChild (0).gameObject;

	}

	// Update is called once per frame
	void Update () {
		//		Debug.Log (lampOn);
	}

	public override void activate(GameObject characterActivated){
		sender = characterActivated;
		treeOn = !treeOn;
		treeChild.SetActive (treeOn);
	}

	void OnTriggerEnter(Collider other){
		if (treeOn) {
			scare (sender, other.gameObject);
		}
	}
}


//i gave up making this work. the idea was that you 
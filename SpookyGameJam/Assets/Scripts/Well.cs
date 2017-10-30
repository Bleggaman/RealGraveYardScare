using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : ScareMapObject, iActivate, iHidingSpot, iScare  {

	Collider collider;
	GameObject sender;

	bool wellRising;
	public float wellUp;
	private float wellRiseSpeed = 1f;
	bool wellRisen = false;
	public ScareEquipItem bucketPrefab;
	bool bucketTaken = false;
	SpriteScript ss;

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider> ();
		Invoke("thisWorksIGuess", 0.02f);
	}
	
	void thisWorksIGuess() {
		ss= this.GetComponent<SpriteScript>();
		ss.SetAnimation("Idle");
	}

	// Update is called once per frame
	void Update () {
		//		Debug.Log (lampOn);
		if (!wellRisen && wellRising){
			wellUp += wellRiseSpeed;
		}
		if (wellUp >= 60 && !wellRisen) {
			wellRisen = true;
			ss.SetAnimation("FullBucket");
		}
	}

	public override void activate(GameObject characterActivated){
		sender = characterActivated;
		
		if (!wellRisen) {
			ss.SetAnimation("DrawWater");
			ss.SetFramesPerSecond(5f);
			wellRising = true;
		}

		if (wellRisen && !bucketTaken) {
			CharacterScript charScript = sender.GetComponent<CharacterScript> ();
			ScareEquipItem bucket = Instantiate (bucketPrefab, transform.position, transform.rotation);
			bucket.GetComponent<ScareEquipItem> ()._unit = sender;
			charScript.equipItems [0] = bucket;
			bucketTaken = true;
			ss.SetAnimation("Idle");
		}

	}

	void OnTriggerEnter(Collider other){

		//does nothing
	}

	#region iHidingSpot implementation

	public void iHide ()
	{
		Debug.Log ("Hiding in well");
	//	throw new System.NotImplementedException ();
	}

	#endregion
}

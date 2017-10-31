using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : ScareEquipItem, iActivate, iLightable {

	public bool lightOn = false;
	public Collider collider;
	public MeshRenderer mesh;
	public bool scaredCoolDown;
	public Light bulb;
	public CharacterScript playerScript;
	public playerMovement2 _playerMovement2;


	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshRenderer> ();
		mesh.enabled = false;
		bulb = transform.GetChild (0).GetComponent<Light>();
		playerScript = GameObject.Find ("Player").GetComponent<CharacterScript>();
		_playerMovement2 = GameObject.Find ("Player").GetComponent<playerMovement2> ();
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (playerScript.gameObject.transform.position.x, playerScript.gameObject.transform.position.y, 
			playerScript.gameObject.transform.position.z - 2.2f);

		if (_playerMovement2.direction == 't') {
			transform.eulerAngles = new Vector3 (0,180,0);
		}
		if (_playerMovement2.direction == 'a') {
			transform.eulerAngles = new Vector3 (0,0,0);
		}
		if (_playerMovement2.direction == 'l') {
			transform.eulerAngles = new Vector3 (0,270,0);
		}
		if (_playerMovement2.direction == 'r') {
			transform.eulerAngles = new Vector3 (0,90,0);
		}
	}

	public override void activate (GameObject playerActived)
	{
		lightOn = !lightOn;
		bulb.enabled = lightOn;
		mesh.enabled = lightOn;

	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Ghost")) {  //one of the things to be changed if make for two player
			//have to make it so it compares tag to be either player or ghost but not the player that is holding the objects
			if (lightOn && !scaredCoolDown) {
				Debug.Log ("adf'_;");
				scare (_unit, other.gameObject); 
				StartCoroutine (scareCool ());
			}
		}
	}

	IEnumerator scareCool()
	{
		scaredCoolDown = true;
		yield return new WaitForSeconds(3);
		scaredCoolDown = false;
	}

	#region iLightable implementation

	public bool playerIsLit ()
	{
		if (playerScript.equipItems [0] == this) {
			return lightOn;

		}
		return false;
	}

	#endregion
}

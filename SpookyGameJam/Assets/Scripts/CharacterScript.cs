using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour, iScarable {

	public bool qPressed = false;
	public ScareEquipItem[] equipItems = new ScareEquipItem[3];
	public ScareEquipItem flashLightPrefab;
	public ScareEquipItem scaryMaskPrefab;
	public ScareEquipItem regularScarePrefab;

	public ScareEquipItem flashLightRef;
	public List<iActivate> inRange = new List<iActivate>();

	// Use this for initialization
	void Start () {
		flashLightRef = transform.GetChild (0).GetComponent<ScareEquipItem>(); //gets flashlight;
		equipItems [0] = flashLightRef;
			
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)){


			foreach (iActivate collidingObject in this.inRange) {
//				Debug.Log (collidingObject);
				collidingObject.activate (gameObject);
			}
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			if (equipItems[0] != null) {
				equipItems [0].activate (gameObject);
			}
		}

		//not in use
		if (Input.GetKeyDown (KeyCode.E)) {
			if (equipItems[1] != null) {
				equipItems [1].activate (gameObject);
		
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			if (equipItems[2] != null) {
				equipItems [2].activate (gameObject);
			}
		}
	}



	void OnTriggerEnter(Collider other){
		if (other.CompareTag("MapActivateObject")){
			if (other.gameObject != gameObject) {
				this.inRange.Add (other.GetComponent<iActivate> ());
			}
		}
	}


	void OnTriggerExit(Collider other){
		if (other.CompareTag("MapActivateObject")){
			if (other.gameObject != gameObject) {
				this.inRange.Remove (other.GetComponent<iActivate> ());
			}
		}
	}

	#region iScarable implementation
	public void scare (int scarePower)
	{
		Debug.Log ("eek : " + scarePower);
	}
	#endregion
}

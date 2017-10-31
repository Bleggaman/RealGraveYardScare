using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashLightHitBox : MonoBehaviour {

	public playerMovement2 _playerMovement2;
	public FlashLight _flashLight;

	void Start(){

		_playerMovement2 = GameObject.Find ("Player").GetComponent<playerMovement2> ();
		_flashLight = GameObject.Find ("FlashLight").GetComponent<FlashLight>();
	}

	void Update(){

		transform.position = _playerMovement2.gameObject.transform.position;
		
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

	void OnTriggerStay(Collider other){
		{
			if (_flashLight.lightOn) {
				if (other.CompareTag("Ghost")){
					other.GetComponent<GhostTestAI> ().scare(5);     //change this to make it more expandable
				}
			}
		}
	}
}

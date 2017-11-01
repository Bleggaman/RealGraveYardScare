using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twigs : MonoBehaviour {
	public WorldInfo worldInfo;
	AudioSource audio;
	public float ghostHearingTwigsDist = 10;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		worldInfo = GameObject.Find ("Main Camera").GetComponent<WorldInfo> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player") || other.CompareTag("Ghost")){
		audio.Play ();
			if (other.CompareTag ("Player")) {
				foreach (GameObject ghost in worldInfo.ghostArray) {
					if (Vector3.Distance (gameObject.transform.position, ghost.transform.position) < ghostHearingTwigsDist) {
						ghost.GetComponent<GhostTestAI> ().receiveHint (transform.position);
					}
				}
			}
		}
	}
}

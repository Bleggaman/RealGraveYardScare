using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMove : MonoBehaviour {

	public int scaredNessLevel;
	public int ghostSightLength = 10;
	public 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 forward = transform.TransformDirection (Vector3.forward);



		//Regular Sight Ray Cast
		Debug.DrawRay (transform.position, forward * ghostSightLength, Color.green);
		if (Physics.Raycast (transform.position, forward, ghostSightLength)) {
			Debug.Log ("seen");
		}


		//looking at light source to see
		//Debug.DrawRay (transform.position, forward * 20, Color.green);
		if (Physics.Raycast (transform.position, forward, 20)) {
			//if the raycast crosses the player and crosses the light source collider
			Debug.Log ("seen by light source!");

		}
	}
}


//make it so that the ghosts can be prepared to scare you, but if they are in passing and have no idea their reaction time is 
//much slower. if they are prepared and already camping looking in a direction waiting, then they get you.

//can see items on the map change but cannot see the ghosts unless you have line of sight to them.
//doing the surprise action gives away position

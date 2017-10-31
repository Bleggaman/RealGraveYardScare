using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour, iLightable  {

	private GameObject player;
	public float distToFireLit = 2;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}



	#region iLightable implementation

	public bool playerIsLit ()
	{
		if (Vector3.Distance (player.transform.position, transform.position) < distToFireLit) {
			return true;
		}
		return false;
	}


	#endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScareProp : MonoBehaviour, iScare, iActivate {
	
	public int scareValue = 1;

	public virtual void scare(GameObject sender, GameObject receiver){
		UnitScript unitScript = receiver.GetComponent<UnitScript> ();
		int scarePower = unitScript.scaredLevel + scareValue;
			unitScript.eek (scarePower);
		//Debug.Log ("scared by " + sender);
	}
		
	public virtual void activate (GameObject playerActived)
	{
		Debug.Log ("activated " + gameObject);
	}
}



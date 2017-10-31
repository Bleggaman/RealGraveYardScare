using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScareProp : MonoBehaviour, iScare, iActivate {
	
	public int scareValue = 1;

	public virtual void scare(GameObject sender, GameObject receiver){
		UnitScript unitScript = receiver.GetComponent<UnitScript> ();
		unitScript.eek (scareValue);
		Debug.Log (receiver + " was scared by " + sender + " using " +  gameObject + " for " + scareValue);
	}
		
	public virtual void activate (GameObject playerActived)
	{
		Debug.Log ("activated " + gameObject);
	}
}



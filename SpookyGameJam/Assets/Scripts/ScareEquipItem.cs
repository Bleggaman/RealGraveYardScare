using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScareEquipItem : ScareProp, iScare, iActivate {
	
	public GameObject _unit;

	public abstract void activate (GameObject playerActived);
}



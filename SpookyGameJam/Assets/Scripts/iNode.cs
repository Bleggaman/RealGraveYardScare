using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iNode {

	List<iNode> getNodes ();
	GameObject getGameObject();
}

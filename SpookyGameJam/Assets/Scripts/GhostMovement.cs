using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour, iScarable {

	public WorldInfo worldInfo;
	public Transform graphics;
	public float moveSpeed = 9;
	public iNode targetNode;
	public iNode prevNode;
	public GameObject candyDrop;
	public NavMeshAgent agent;
	public float foVision = 120;
	public float visionRange = 2;

	public float nodeSensitivity = 1;
	//Properties
	public bool scared = false;
	public bool fleeing = false;
	public Transform scareLocation;
	public Vector3 movementVector;

	public bool alert = false;
	public Transform focusLocation;
	public bool hiding = false;
	public bool playerSeen = false;

	//Initialized in Start
	public Rigidbody rb;
	public ScareEquipItem equipItem;

	// Periods define the amount of time states persist
	public float fleePeriod = 5;
	public float animPeriod = 3;
	public float reactionPeriod = 0.5f;
	public float alertPeriod = 10f;

	// Timers track time internally

	private float moveAgainTime = 1;

	public GameObject player;
	public CharacterScript playerScript;

	bool seePlayer;
	bool playerSees;
	bool playerAlert = false;
	bool isAlert;
	float distLong = 6;
	float distShort = 2;
	float distMed = 4;
	public float fastReact = .1f;
	public float slowReact = .3f;
	public float playerAlertTime = .4f;

	public enum GhostState {hiding, wandering, seeing, dead, freeze};
	GhostState state = new GhostState();



	// Use this for initialization
	void Start () {
		worldInfo = GameObject.Find("Main Camera").GetComponent<WorldInfo>();
		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<CharacterScript> ();
		state = GhostState.wandering;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (moveAgainTime);

	
	//	Debug.Log ("Tome = " + Time.time);
		//Debug.Log ("move again " + moveAgainTime);


		print (state);

		//print (prevNode);
		//print (targetNode);

		interactionUpdate();

		if (canSeePlayer()) {
			seePlayer = true;
			Debug.Log ("Sees player");
			ghostSeesPlayer (); 
		} else {
			seePlayer = false;
		}
			moveTo ();

	}
	private void resetStatus() {
		fleeing = false;
		scared = false;

	
	}

	private void moveTo() {
		Vector3 dirVector = Vector3.zero;
		if (state == GhostState.wandering) {
			if (targetNode != null) {
				dirVector = targetNode.getGameObject ().transform.position - this.transform.position;
				if (dirVector.magnitude < nodeSensitivity) {
					// Reached destination
					prevNode = targetNode;
					targetNode = Utils.findiNode (gameObject, worldInfo);
				}

			} else {
				targetNode = Utils.findiNode (gameObject, worldInfo);
			} 
			agent.destination = targetNode.getGameObject ().transform.position;
		}
		if (state == GhostState.hiding) {
			if (targetNode != null) {
				dirVector = targetNode.getGameObject ().transform.position - this.transform.position;
				if (dirVector.magnitude < nodeSensitivity) {
					// Reached destination
					prevNode = targetNode;

					if (Time.time > moveAgainTime) {
						state = GhostState.wandering;

					}
				}
			}
		}
		if (state == GhostState.freeze) {
			agent.destination = transform.position;
			targetNode = null;


			if (Time.time > moveAgainTime) {
				state = GhostState.wandering;
			}
		}
	}


	private void flee(Transform source) {
		// What does it mean to flee
		Instantiate(candyDrop, transform.position, transform.rotation);
		fleeing = true;

		// fade to become invisible
	
		// run away from scare source
		Vector3 dirVector = transform.position - source.position;
		agent.destination = transform.position + dirVector.normalized * 10;
	}

	// UNTESTED METHOD

	private bool increaseTimer(float timer, float period) {
		period += Time.time;
		return (period < timer);
	}

	IEnumerator ghostReact(float reactTime){
		yield return new WaitForSeconds (reactTime);
		isAlert = true;

	}

	IEnumerator deAlert(){
		yield return new WaitForSeconds (3);
		isAlert = false;

	}

	//IEnumerator playerReact(){
	//	yield return new WaitForSeconds (playerAlertTime);
	//	playerAlert = true;
	//}

	IEnumerator playerDeAlert(){
		yield return new WaitForSeconds (3);
		playerAlert = false;
	}

	#region iScarable implementation

	public void scare (int scarePower)
	{

		if (isAlert) {
			Debug.Log ("sscare failed!");
		} else {
			Debug.Log ("eek + " + scarePower);
		}
	}

	#endregion

	private bool canSeePlayer () {


		Vector3 direction = agent.velocity.normalized;

		RaycastHit hit;
		Debug.DrawRay(transform.position, direction * 1, Color.green);
		if (Physics.Raycast(transform.position, direction * 7, out hit, 5)){
			
			if (hit.collider != null) {
//				Debug.Log (hit.collider.tag);
				return (hit.collider.gameObject.tag == "Player"); 
			}


		}
		return false;
	}

	public void ghostSeesPlayer(){
		
		if (playerSees) { //PLAYER SEES   NOT CURRENTLY BEING CALLED 
			if (!playerAlert) { //PLAYER NOT ALERT
				if (isAlert) { //GHOST IS ALERT
					if (Vector3.Distance (transform.position, player.transform.position) < distShort) {
						//if within range of activatable  use it 
						playerScript.scare (5);
						Debug.Log ("bleg");
						moveAgainTime = Time.time + 2;
						state = GhostState.freeze;

					} else {
						state = GhostState.freeze;
						moveAgainTime = Time.time + 4;   //better ai for hiding spot position
					}
				}
			} else { //PLAYER IS ALERT

				if (Vector3.Distance (transform.position, player.transform.position) < distShort) {
					Utils.findiNode (gameObject, worldInfo);
					//continues to face player
				}
			}
		} else { //PLAYER DOES NOT SEE
			if (isAlert) {
				if (Vector3.Distance (transform.position, player.transform.position) < distShort) {
					playerScript.scare (5);
					moveAgainTime = Time.time + 2;
					state = GhostState.freeze;


				} else if (Vector3.Distance (transform.position, player.transform.position) < distMed) {
					targetNode = Utils.findCloseHiding (gameObject, worldInfo);
					state = GhostState.hiding;
					moveAgainTime = Time.time + 2;

				} else {
					targetNode = Utils.findCloseHiding (gameObject, worldInfo);
					state = GhostState.hiding;
					moveAgainTime = Time.time + 2;
				}
			}
		}
	}

	public void interactionUpdate(){
		seePlayer = canSeePlayer ();

		if (!seePlayer && isAlert) {
			StartCoroutine (deAlert ());
		}

		if (seePlayer) {
			if (state == GhostState.hiding) {
				StartCoroutine (ghostReact (fastReact));
			} else {
				StartCoroutine (ghostReact (slowReact));
			}
		} 

		if (playerSees) {
		//	StartCoroutine (playerReact ());
		} else {
			if (playerAlert) {
				StartCoroutine (playerDeAlert ());
			}
		}
	}

	public void receiveHint(Vector3 hintLocation){
		targetNode = Utils.findHidingFromHint (gameObject, worldInfo, hintLocation);
		state = GhostState.wandering;
		//moveAgainTime = 10;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		//Physics.Raycast(transform.position, (player.transform.position - transform.position), out losPlayer, LayerMask.NameToLayer("UI"));
		//Gizmos.DrawLine(transform.position, player.transform.position);
	}
}

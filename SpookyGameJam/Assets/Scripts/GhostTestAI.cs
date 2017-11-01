using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostTestAI : MonoBehaviour, iScarable {

	public WorldInfo worldInfo;
	public Transform graphics;
	public AudioSource audio1;
	public AudioSource audio2;

	public float moveSpeed = 9;

	public GameObject candyDrop;
	public NavMeshAgent agent;
	public CharacterScript playerScript;
	public bool lineOfSight;
	public FlashLight flashLightref;
	public float flashLightSeeDist = 15;

	public float nodeSensitivity = 1;
	//Propertie


	public float ghostSeeRange = 2;
	public float ghostLoseSightRange = 10;

	//Initialized in Start
	public Rigidbody rb;
	public ScareEquipItem equipItem;
	public float stopChaseTime = 5;
	// Periods define the amount of time states persis

	// Timers track time internally
	public float scareDistance = .1f;

	public GameObject player;

	public bool foundChaseTarget;
	public float scaredYouUntil;
	public bool scaredYouOn;
	public bool playerUnderLight;
	public float reactionTime = .5f;
	public bool ghostReacting;
	public bool ghostUnderLight; 

	public bool seesPlayer;


	public Vector3 targetMove;
	public Vector3 hintLocation; 
	public Vector3 targetChase;
	private Vector3 vector3Null = new Vector3 (0, 0, 0);

	public enum GhostState {wandering, chasing, notMove};
	GhostState state = new GhostState();


	// Use this for initialization
	void Start () {
		
		worldInfo = GameObject.Find("Main Camera").GetComponent<WorldInfo>();
		player = GameObject.Find ("Player");
		playerScript = player.GetComponent<CharacterScript> ();
		state = GhostState.wandering;
		rb = GetComponent<Rigidbody> ();
		agent = GetComponent<NavMeshAgent> ();
		flashLightref = GameObject.Find ("FlashLight").GetComponent<FlashLight> ();
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (state);

		seesPlayer = seeingPlayer ();

		if (seesPlayer) {
			if (!ghostReacting && state != GhostState.chasing) {
				ghostReacting = true;
				StartCoroutine(ghostReact ());
			}
		}
		moveTo ();
	}
		
	private void moveTo() { //if under light does nothing
		if (ghostUnderLight) {
			agent.destination = transform.position;
		} else {

			//Wandering: goes from node to node
			//can be interjected with hint by changing target directly
			if (state == GhostState.wandering) {
				if (targetMove != vector3Null) { //has target
					if (Vector3.Distance (transform.position, targetMove) < nodeSensitivity) { //arrived
						targetMove = Utils.findiNode (gameObject, worldInfo).getGameObject ().transform.position;
					}
				} else {
					targetMove = Utils.findiNode (gameObject, worldInfo).getGameObject ().transform.position;
				}
			}

			//Chasing: if sees player, goes to player. else, goes to where last saw character. If it reaches this point still 
			//chasing, it finds another nearby point to chase character from.
			//if the ghost is in the flashlight, it cannot do anything (but the player is giving away its position)
			if (state == GhostState.chasing) {


				if (Vector3.Distance (transform.position, player.transform.position) < scareDistance) {
					player.GetComponent<CharacterScript> ().scare (5);
					StartCoroutine(scaredPlayer (2f)); //scared the player
				} else if (seesPlayer) {
					hintLocation = player.transform.position;
					targetChase = hintLocation;
					targetMove = hintLocation;
				} else {
					if (targetChase == vector3Null || Vector3.Distance (transform.position, targetMove) < nodeSensitivity) {
						targetChase = Utils.findiNodeFromHint (worldInfo, hintLocation).getGameObject ().transform.position;
						targetMove = targetChase;
					}
					if (Vector3.Distance (transform.position, targetChase) < nodeSensitivity) {
						targetChase = vector3Null;
					}
				}
			}

			//ScaredYou: ghost waits for three seconds before wandering
			if (state == GhostState.notMove) {
				targetMove = transform.position;
			}
			agent.destination = targetMove;
		}
	}
			
	private bool seeingPlayer () {
		
		if (worldInfo.playerLiit) { 
			return true;
		}
		if (!Physics.Linecast (transform.position, player.transform.position)) {
			Debug.Log ("line of sight");
			lineOfSight = true;
			if (flashLightref.lightOn) {
				if (Vector3.Distance (gameObject.transform.position, player.transform.position) < flashLightSeeDist) {
					return true;
				}
			}
			if (state == GhostState.chasing || state == GhostState.notMove) {
				if (Vector3.Distance (transform.position, player.transform.position) < ghostLoseSightRange) {
					return true;
				} 
			} else if (state == GhostState.wandering) {
				if (Vector3.Distance (transform.position, player.transform.position) < ghostSeeRange) {
					return true;
				}
			}
		}
		return false;
	}

	public void receiveHint(Vector3 hintLocation){
		if (state == GhostState.wandering) {
			targetMove = hintLocation;
		} else if (state == GhostState.chasing){
			targetChase = hintLocation;
			}
		//moveAgainTime = 10;
	}


	IEnumerator stopChase(){
		yield return new WaitForSeconds (stopChaseTime); //TIME TO STOP THE CHASE
		state = GhostState.wandering;
		foundChaseTarget = false;
		targetChase = vector3Null;
	}

	IEnumerator ghostReact(){
		yield return new WaitForSeconds (reactionTime); //TIME TO STOP THE CHASE
		ghostReacting = false;
		state = GhostState.chasing;


	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		//Physics.Raycast(transform.position, (player.transform.position - transform.position), out losPlayer, LayerMask.NameToLayer("UI"));
		//Gizmos.DrawLine(transform.position, player.transform.position);
	}

	#region iScarable implementation

	public void scare (int scarePower)
	{
		if (state != GhostState.notMove) {
			if (state == GhostState.chasing) {
				Debug.Log ("sscare failed!");
				if (!audio2.isPlaying) {
					audio2.Play ();
				}
			} else {
				Debug.Log ("Gasdfasdfhost eeked + " + scarePower);
				audio1.Play ();
				StartCoroutine (wasScared (.2f));
			}
		} else
			Debug.Log ("already scared");
		
	}
	#endregion

	IEnumerator scaredPlayer(float secondsNotMove){
		state = GhostState.notMove;
		yield return new WaitForSeconds (secondsNotMove);

		//really here we make the ghost disappear
		transform.position = new Vector3 (0, 0, 0);
		state = GhostState.wandering;
	}

	IEnumerator wasScared(float secondsNotMove){
		state = GhostState.notMove;
		yield return new WaitForSeconds (secondsNotMove);

		//really here we make the ghost disappear
		transform.position = new Vector3 (0, 0, 0);
		state = GhostState.wandering;
	}
}



//get the player is under light or using flashlight bool
//give the hints when the player is walking over stuff
//ghost AI: make disapear when scared
//ghost AI: make respawn at grave and chase
//ghost AI: make go to lamp and activate
//ghost AI: make more nodes
///ghost A: 
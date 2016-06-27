using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {


	public float speedRot = 5f;
	public float huntSpeed = 3f;
	public float huntDistance = 3f;
	public float attackSpeed = 7f;

	private Vector3 originalLoc;
	GameObject playerShip;
	enum enemyTwoStatus {INITIALIZING, HUNTING, TARGETING, ATTACKING};
	enemyTwoStatus status;
	private Vector3 destVect;

	// Use this for initialization
	void Awake(){
		originalLoc = transform.position;
		playerShip = GameObject.FindGameObjectWithTag ("Player");
		status = enemyTwoStatus.INITIALIZING;
	}
	
	// Update is called once per frame
	void Update () {
		switch (status){
		case enemyTwoStatus.INITIALIZING:
			transform.position += Vector3.down * huntSpeed * Time.deltaTime;
			if (originalLoc.y - transform.position.y >= huntDistance) {
				status = enemyTwoStatus.HUNTING;
			}
			break;
		case enemyTwoStatus.HUNTING:
			destVect = transform.position - playerShip.transform.position;
			Debug.Log ("Hunting: " + destVect);
			Quaternion destRot = Quaternion.LookRotation (Vector3.forward, destVect);
			Quaternion smoothRot = Quaternion.Slerp (transform.rotation, destRot, (Time.deltaTime * speedRot));
			transform.rotation = smoothRot;
			//if the rotation angle and the playership angle match, enemy has a lock and will now target to attack
			if (transform.rotation == destRot) {
				status = enemyTwoStatus.TARGETING;
			}
			break;
		case enemyTwoStatus.TARGETING:
			Debug.Log ("Targeting: " + destVect);
			destVect = getAttackCoords(destVect);
			break;
		case enemyTwoStatus.ATTACKING:
			Debug.Log ("attacking state " + destVect);
			Vector3 position = transform.position;

			position -= destVect * attackSpeed * Time.deltaTime;
			transform.position = position;
			break;
			
		}

	}

	private Vector3 getAttackCoords(Vector3 destVector){
		status = enemyTwoStatus.ATTACKING;
		return destVector.normalized;
	}

	private void Launch(Vector3 destination){
		transform.Translate (destination);
	}
}

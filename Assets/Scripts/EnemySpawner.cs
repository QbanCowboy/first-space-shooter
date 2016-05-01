using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 2f;
	public float spawnDelay = .5f;

	private bool movingRight = false;
	private float xmax, xmin;
	private float padding;


	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3(0,0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3(1,0, distanceToCamera));
		padding = width / 2;
		xmax = rightEdge.x;
		xmin = leftEdge.x;
		SpawnUntilFull ();
	}


	void SummonEnemies(){
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;			
		}
	}

	public void OnDrawGizmos (){
		Gizmos.DrawWireCube(transform.position, new Vector3 (width, height, 0));
	}


	// Update is called once per frame
	void Update () {
		if ((transform.position.x - padding) <= xmin) {
			movingRight = true;
		} else if ((transform.position.x + padding) >= xmax) {
			movingRight = false;
		}
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	
		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}

	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;	
		}
		if (NextFreePosition()){
		Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	bool AllMembersDead(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}

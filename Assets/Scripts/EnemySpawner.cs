using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject asteroidPrefab;
	public GameObject[] powerUp;

	public float gizmoWidth = 10f;
	public float gizmoHeight = 5f;
	//Initial maximum spawn delay for enemy ships
	public float asteroidSpawnDelay = 2f;
	public float spawnDelayOnNewLife = 6f;
	//minimum and maximum dropRate times for Power Ups
	public float minDrop = 6f;
	public float maxDrop = 10f;

	public enum LevelState {
		LEVEL_START,
		LEVEL_MAIN,
		LEVEL_END
	};

	private static LevelState stateOfLevel;

	private float minSpawnRate = .5f;
	private Vector2 position;

	private float typeOneEnemyTimer;

	void Awake(){
		stateOfLevel = LevelState.LEVEL_START;
	}

	void OnEnable(){
		typeOneEnemyTimer = 0f;
		Invoke ("SpawnAsteroid", asteroidSpawnDelay);
	}

	void OnDisable(){
		CancelInvoke ();
	}

	public void OnDrawGizmos (){
		Gizmos.DrawWireCube(transform.position, new Vector3 (gizmoWidth, gizmoHeight, 0));
	}


	// Update is called once per frame
	void Update () {
		typeOneEnemyTimer += Time.deltaTime;
		//Timer to deploy the power up onto an enemy ship

		/* On Level Start, initialize how many of each type of enemy will be attacking, and set the time_between_spawns for each type,
		 * then after x amount of seconds, hide the level display text (disable).
		 * Then, set the level state to Level Main.
		 * 
		 * On Level Main, start calling the functions that spawn the different types of enemies.  Keep track of number of ships destroyed,
		 * and any other numbers.  If all the ships are destroyed, set the Level state to Level End.
		 * 
		 * On Level End, show any stats (maybe), reset anything needed, and then set the Level state to Level Start again.
		 *
		 * Switch (
		 * */

	}


	void SpawnEnemy(){
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		float xVal = Random.Range (min.x, max.x);
		xVal = Mathf.Clamp (xVal, (min.x + .6f), (max.x - .6f));
		Vector2 enemyLoc = new Vector2 (xVal, transform.position.y);
		GameObject newEnemy = Instantiate (enemyPrefab, enemyLoc, Quaternion.identity) as GameObject;
	
		//if time elapsed is over 15 seconds, increase the Type 1 enemy rate of fire;
		ScheduleNextAsteroid ();
	}

	void SpawnAsteroid(){
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		float xVal = Random.Range (min.x, max.x);
		xVal = Mathf.Clamp (xVal, (min.x + .6f), (max.x - .6f));
		Vector2 enemyLoc = new Vector2 (xVal, transform.position.y);
		GameObject newEnemy = Instantiate (asteroidPrefab, enemyLoc, Quaternion.identity) as GameObject;
		ScheduleNextAsteroid ();
	}

	void ScheduleNextAsteroid(){
		float spawnInSeconds;
		if (typeOneEnemyTimer > 25f) {
			asteroidSpawnDelay -= .5f;
		}
		if (asteroidSpawnDelay > minSpawnRate) {
			spawnInSeconds = Random.Range (minSpawnRate, asteroidSpawnDelay);
		} else {
			spawnInSeconds = minSpawnRate;
		}
		Invoke ("SpawnAsteroid", spawnInSeconds);
	}


	public void CallPowerUp (Vector3 shipPos){
		print ("Ship destroyed, calling for Power Up");
		int powerUpType = Random.Range (0, 2);
		if (powerUpType == powerUp.Length) {
			powerUpType--;
		}
		GameObject powerUpCoin = Instantiate (powerUp[powerUpType], shipPos, Quaternion.identity) as GameObject;
		powerUpCoin.GetComponent<PowerUp> ().SetPowerUp (powerUpType);
		powerUpCoin.GetComponent<PowerUp> ().DropPowerUp (shipPos);
	}
		

}

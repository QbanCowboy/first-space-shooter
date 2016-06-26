using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject asteroidPrefab;
	public GameObject[] powerUp;
	public LevelController levelController;

	//Initial maximum spawn delay for enemy ships
	public float spawnDelayOnNewLife = 6f;
	//minimum and maximum dropRate times for Power Ups
	public float minDrop = 6f;
	public float maxDrop = 10f;


//	private float minSpawnRate = .5f;
	private Vector2 position;

	private float typeOneEnemyTimer = 4f;
	private float AsteroidTimer = 2f;
	private int typeOneEnemyCount, typeTwoEnemyCount, typeThreeEnemyCount = 0;
	private float typeOneSpawnTime, typeTwoSpawnTime, typeThreeSpawnTime, asteroidSpawnTime = 0;
	private bool enemiesSet = false;

	void Awake(){
	}
		

	void OnEnable(){
	}

	void OnDisable(){
	}


	// Update is called once per frame
	void Update () {
		if (enemiesSet) {
			typeOneSpawnTime -= Time.deltaTime;
			asteroidSpawnTime -= Time.deltaTime;
			if (typeOneSpawnTime <= 0) {
				SpawnEnemy ();
				typeOneEnemyCount--;
				typeOneSpawnTime = typeOneEnemyTimer;
			}
			if (asteroidSpawnTime <= 0) {
				SpawnAsteroid ();
				asteroidSpawnTime = AsteroidTimer;
			}

		}
		if ((enemiesSet) && (NoMoreEnemiesLeft ())) {
			Invoke ("enemiesFinished", 2f);
		}
	}


	private bool NoMoreEnemiesLeft(){
		if (typeOneEnemyCount == 0) {
			enemiesSet = false;
			return true;
		} else {
			return false;
		}
	}

	private void enemiesFinished(){
		levelController.enemiesFinished ();	
	}



	void SpawnEnemy(){
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		float xVal = Random.Range (min.x, max.x);
		xVal = Mathf.Clamp (xVal, (min.x + .6f), (max.x - .6f));
		Vector2 enemyLoc = new Vector2 (xVal, transform.position.y);
		GameObject newEnemy = Instantiate (enemyPrefab, enemyLoc, Quaternion.identity) as GameObject;
	
	}

	void SpawnAsteroid(){
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		float xVal = Random.Range (min.x, max.x);
		xVal = Mathf.Clamp (xVal, (min.x + .6f), (max.x - .6f));
		Vector2 enemyLoc = new Vector2 (xVal, transform.position.y);
		GameObject newEnemy = Instantiate (asteroidPrefab, enemyLoc, Quaternion.identity) as GameObject;
	}

/*	void ScheduleNextAsteroid(){;
		Invoke ("SpawnAsteroid", asteroidSpawnDelay);
	}


	void ScheduleNextEnemy(){
		Invoke ("SpawnEnemy", typeOneEnemyTimer);
	}*/

	public void setEnemyCount (int typeOne, int typeTwo, int typeThree, float enemyOneTimer){
		typeOneEnemyCount = typeOne;
		typeTwoEnemyCount = typeTwo;
		typeThreeEnemyCount = typeThree;
		typeOneEnemyTimer = enemyOneTimer;
		typeOneSpawnTime = typeOneEnemyTimer;
		asteroidSpawnTime = AsteroidTimer;
		enemiesSet = true;
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

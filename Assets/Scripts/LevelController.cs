using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	public EnemySpawner enemySpawner;
	public Text levelState_Text;
	//Enum list to show/control the current state of the game
	public enum LevelState {
		LEVEL_START,
		LEVEL_MAIN,
		LEVEL_END
	};

	public float levelTextTimer = 2f;

	private int currentLevel;
	LevelState levelState;

	private int typeOneEnemy, typeTwoEnemy, typeThreeEnemy=0;
	private float typeOneTimer = 4f;
	// Use this for initialization
	void Start () {
		levelState = LevelState.LEVEL_START;
		currentLevel = 1;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	//Timer to deploy the power up onto an enemy ship

	/* On Level Start, initialize how many of each type of enemy will be attacking, and set the time_between_spawns for each type,
	 * then after x amount of seconds, hide the level display text (disable).
	 * Then, set the level state to Level Main.
	 * 
	 * On Level Main, start calling the functions that spawn the different types of enemies.  Keep track of number of ships destroyed,
	 * and any other numbers.  If all the ships are destroyed, set the Level state to Level End.
	 * 
	 * On Level End, show any stats (maybe), reset anything needed, and then set the Level state to Level Start again.
	 */
	 	
		if (levelState == LevelState.LEVEL_START) {
			levelState_Text.gameObject.SetActive (true);
			levelState_Text.text = "Level " + currentLevel.ToString ();
			Invoke ("hideLevelText", levelTextTimer);
		}

		if (levelState == LevelState.LEVEL_END) {
			
		}

	}

	void InitializeEnemies(){
		switch (currentLevel) {
		case 1:
			typeOneEnemy = 5;
			typeOneTimer = 4f;
			break;
		case 2:
			typeOneEnemy = 10;
			typeOneTimer = 3f;
			break;
		case 3:
			typeOneEnemy = 10;
			typeOneTimer = 1.5f;
			break;
		case 4:
			typeOneEnemy = 15;
			typeOneTimer = 1f;
			break;
		}
	}

	void hideLevelText(){
		if (levelState_Text) {
			levelState_Text.gameObject.SetActive (false);
		}
		InitializeEnemies ();
		levelState = LevelState.LEVEL_MAIN;
		enemySpawner.setEnemyCount (typeOneEnemy, 0, 0, typeOneTimer);
	}

	public void enemiesFinished(){
		levelState = LevelState.LEVEL_END;
		currentLevel++;
		levelState = LevelState.LEVEL_START;
	}
}


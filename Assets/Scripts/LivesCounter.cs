using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivesCounter : MonoBehaviour {

	public int startingLifeAmt = 3;
	public PlayerController playerShip;
	public EnemySpawner enemySpawner;


	private Text livesText;
	private float playerShipSpawnDelay = 3f;

	void Awake(){
		livesText = GetComponent<Text> ();
		livesText.text = "Lives: " + startingLifeAmt.ToString ();
	}

	// Use this for initialization
	void Start () {
	
	}

	private void RestartPlayer(){
		playerShip.gameObject.SetActive (true);
	}
		

	public void SubtractALife(){
		startingLifeAmt-= 1;
		if (startingLifeAmt <= 0) {
			SceneManager.LoadScene ("Win");
		}
		if (livesText) {
			livesText.text = "Lives: " + startingLifeAmt.ToString ();
			Invoke ("RestartPlayer", playerShipSpawnDelay);
		}
	}
}

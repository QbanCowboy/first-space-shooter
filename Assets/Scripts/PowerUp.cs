using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	private int powerUpType;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void DropPowerUp (Vector3 enemyLoc){
		GameObject enemySpawn = GameObject.FindGameObjectWithTag ("EnemySpawner");
		gameObject.transform.position = enemyLoc;
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -3f, 0);
	}


	public void SetPowerUp (int powerUp){
		powerUpType = powerUp;
	}

	public int GetPowerUp (){
		return powerUpType;
	}

	public void DestroyPowerUp(){
		Destroy (gameObject);
	}

}
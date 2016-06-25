using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public float health = 150f;
	public int enemyValue = 10;
	public float speed = 2f;
	public float collisionDamage = 100f;
	public float rateOfDrop = .1f;

	private Vector2 position;
	private ScoreKeeper score;

	void Awake(){
	}


	void Start(){
		score = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void Update(){
		position = transform.position;
		if (position.y <= 1) {
		}
		transform.position = new Vector2 (position.x, position.y - speed * Time.deltaTime);
	}
		

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if ((health <= 0) && (score) ){
				score.Score (enemyValue);
				DestroyShip ();
			}
			return;
		}

	}

	private void DeployPowerUp(){
		GameObject EnemySpawner = GameObject.FindGameObjectWithTag ("EnemySpawner");
		if (EnemySpawner) {
			EnemySpawner.GetComponent<EnemySpawner> ().CallPowerUp (transform.position);
		}
	}
		



	public float CollisionDamage (){
		return collisionDamage;
	}

	public void DestroyShip (){
		float RNG = Random.Range (0f, 1f);
		if (rateOfDrop >= RNG){
			DeployPowerUp ();
		}
		Destroy (gameObject);
	}


}
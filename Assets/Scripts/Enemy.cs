using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float health = 150f;
	public GameObject enemyLaser;
	public float enemyLaserSpeed = 5f;
	public float minRate = 1f;
	public float maxRate = 4f;
	float rateOfFire;
	float time;
	float clock;
	public int enemyValue = 10;

	private ScoreKeeper score;
	private AudioSource laserSound;
//	public float shotsPerSeconds = 0.5f;



	void Start(){
		score = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		rateOfFire = Random.Range (minRate, maxRate);
		time = rateOfFire;
		clock = 0;
		laserSound = GetComponent<AudioSource> ();
	}
		
	void Update(){
		clock += Time.deltaTime;
		if (time == 0) {
			FireLaser ();
			time = rateOfFire;
			rateOfFire = Random.Range (minRate, maxRate);
		} else if (time <= clock){
			time = 0;
			clock = 1;
		}

	}

/*  *This Method is based on the class example
	void Update (){
		float probability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < probability) {
			FireLaser ();
		}
	}
	*/

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				score.Score (enemyValue);
				Destroy (gameObject);
			}
		}
	}

	void FireLaser (){
		Vector3 shipPos = transform.position;
		shipPos.y -= .5f;
		GameObject laserBlast = Instantiate (enemyLaser, shipPos, Quaternion.identity) as GameObject;
		laserBlast.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -enemyLaserSpeed, 0);
		laserSound.Play ();
		}


}

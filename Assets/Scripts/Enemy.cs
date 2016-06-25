using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float health = 150f;
	public GameObject enemyLaser;
	public float enemyLaserSpeed = 3f;
	public float rateOfFire=1f;
	public int enemyValue = 10;
	public float speed = 2f;
	new private Animator animation;
	public float collisionDamage = 100f;
	public float rateOfDrop = 0.1f;


	private Vector2 position;
	private ScoreKeeper score;
	private AudioSource laserSound;
	private float timeToFire;
	private bool canFire = true;


	void Awake(){
		timeToFire = 0;
	}


	void Start(){
		animation = GetComponent<Animator> ();
		score = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		laserSound = GetComponent<AudioSource> ();
	}
		
	void Update(){
		timeToFire += Time.deltaTime;
		if ((timeToFire >= rateOfFire) && (canFire)) {
			FireLaser ();
			timeToFire = 0;
		}
		transform.position += Vector3.down * speed * Time.deltaTime;
			

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
			animation.SetTrigger ("Enemy_Hit");
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

	void FireLaser (){
		GameObject playerShip = GameObject.Find ("PlayerShip");
		Vector3 shipPos = transform.position;
		shipPos.y -= .5f;
		if (playerShip) {
			GameObject laserBlast = Instantiate (enemyLaser, shipPos, Quaternion.identity) as GameObject;
			Vector2 direction = playerShip.transform.position - shipPos;
			if (laserBlast) {
				laserBlast.GetComponent<EnemyCircleBullet> ().SetDirection (direction);
			}
			laserSound.Play ();
		} else {
			Debug.Log ("Player Ship is inactive");
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

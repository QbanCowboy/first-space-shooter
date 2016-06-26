using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


	public float maxHealth = 150;
	private float health;
	public float laserSpeed = 3;
	public float rateOfFire = .4f;
	public float speed = 3;
	public float padding = 1f;
	float xmin, xmax, ymin, ymax;
	public GameObject laserFire;
	public GameObject shield;
	private AudioSource laserSound;
	public GameObject enemySpawner;
	//attributes that control the double blasters Power Up on the ship
	private bool doubleBlasters = false;
	private float doubleBlasters_timer = 0;
	public float doubleBlasters_TimeLimit = 4f;

	//health attributes and the percentage sent to the Health Bar UI
	public HealthBar healthBar;
	private float healthPct;

	void Awake(){
	} 

	private void ResetShip(){
		health = maxHealth;
		shield.GetComponent<shields> ().TurnOff ();
		healthBar.fillLevelChange (health/maxHealth);

	}

	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
		ymin = leftMost.y + padding;
		Vector3 yPos = leftMost - rightMost;
		ymax = yPos.y / 4;
		laserSound = GetComponent<AudioSource> ();
	}
	void OnDisable(){
		Debug.Log ("Disabled");
		CancelInvoke ();
	}

	void OnEnable(){
		ResetShip ();
		enemySpawner.SetActive (true);
	} 

	void Fire(){
		if (!doubleBlasters) {

			Vector3 shipPos = transform.position;
			shipPos.y += .5f;
			GameObject laserBlast = Instantiate (laserFire, shipPos, Quaternion.identity) as GameObject;
			laserBlast.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, laserSpeed, 0);
			laserSound.Play ();
		} else {
			Vector3 leftLaser = transform.position;
			Vector3 rightLaser = transform.position;
			leftLaser.x += -.44f;
			leftLaser.y += .2f;
			rightLaser.x += .44f;
			rightLaser.y += .2f;
			GameObject LeftBlaster = Instantiate (laserFire, leftLaser, Quaternion.identity) as GameObject;
			LeftBlaster.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, laserSpeed, 0);
			GameObject RightBlaster = Instantiate (laserFire, rightLaser, Quaternion.identity) as GameObject;
			RightBlaster.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, laserSpeed, 0);
			laserSound.Play ();
		}
	}



	void Update () {

		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");
		Vector2 direction = new Vector2 (x, y).normalized;
		Move (direction);


		if (Input.GetKeyDown (KeyCode.Space)) {
		InvokeRepeating ("Fire", 0.000001f, rateOfFire);
		} 

		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ();
		}

		if (doubleBlasters) {
			doubleBlasters_timer += Time.deltaTime;
		}

		if (doubleBlasters_TimeLimit < doubleBlasters_timer){
			doubleBlasters_timer = 0;
			doubleBlasters = false;
		}
			

	}

	void Move (Vector2 direction){

		Vector2 pos = transform.position;

		pos += direction * speed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, xmin, xmax);
		pos.y = Mathf.Clamp (pos.y, ymin, ymax);

		transform.position = pos;
	}


	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			if (!shield.GetComponent<shields>().shieldsOn) {
				missile.Hit ();
				updateHealth (missile.GetDamage ());
			}
		}
		PowerUp powerUpCoin = col.gameObject.GetComponent<PowerUp> ();
		if (powerUpCoin) {

			switch (powerUpCoin.GetPowerUp ()) {
			case 0:
				doubleBlasters = true;
				break;
			case 1:
				shield.GetComponent<shields> ().TurnOn ();
				break;
			}
			powerUpCoin.DestroyPowerUp ();
		}

		Enemy enemyShip = col.gameObject.GetComponent<Enemy> ();
		if (enemyShip) {
			updateHealth (enemyShip.CollisionDamage ());
			enemyShip.DestroyShip ();
		}

	}

	void updateHealth (float damage){
		health -= damage;
		healthPct = health / maxHealth;
		healthBar.fillLevelChange (healthPct);
		if (health <= 0) {
			GameObject.Find ("Lives").GetComponent<LivesCounter> ().SubtractALife ();
			enemySpawner.SetActive (false);
			gameObject.SetActive (false);
//			SceneManager.LoadScene ("Win");
//			Destroy (gameObject);
		}
	}

	private void EquipPowerUp(int type){
		
	}

}

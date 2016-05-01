using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float health = 150;
	public float laserSpeed = 3;
	public float rateOfFire = .4f;
	public float speed = 3;
	public float padding = 1f;
	float xmin, xmax;
	public GameObject laserFire;
	private AudioSource laserSound;

	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
		laserSound = GetComponent<AudioSource> ();
	}

	void Fire(){
		Vector3 shipPos = transform.position;
		shipPos.y += .5f;
		GameObject laserBlast = Instantiate (laserFire, shipPos, Quaternion.identity) as GameObject;
		laserBlast.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, laserSpeed, 0);
		laserSound.Play ();
	}


	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
		//	this.transform.position += new Vector3(-speed * Time.deltaTime, 0,0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
		//	this.transform.position += new Vector3 (speed * Time.deltaTime, 0,0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.Space)){
			InvokeRepeating ("Fire", 0.000001f, rateOfFire);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ();
		}


		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);

		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	
	}
	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			print (health);
			missile.Hit ();
			if (health <= 0) {
				Destroy (gameObject);
			}
		}
	}
}

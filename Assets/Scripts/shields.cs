using UnityEngine;
using System.Collections;

public class shields : MonoBehaviour {

	public float totalHealth = 200f;
	private float health;
	public bool shieldsOn = true;

	// Use this for initialization
	void Start () {
		health = totalHealth;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TurnOff(){
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<CircleCollider2D> ().enabled = false;
		shieldsOn = false;
	}

	public void TurnOn(){
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<CircleCollider2D> ().enabled = true;
		health = totalHealth;
		shieldsOn = true;
	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			takeDamage (missile.GetDamage ());
			missile.Hit ();
			}
		Enemy enemyShip = col.gameObject.GetComponent<Enemy> ();
		if (enemyShip) {
			takeDamage (enemyShip.CollisionDamage ());
			enemyShip.DestroyShip ();
		}
	}

	void takeDamage (float damage){
		health -= damage;
		if (health <= 0) {
			TurnOff ();
		}
	}
}

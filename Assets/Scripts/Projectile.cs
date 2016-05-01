using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float rad = 1f;
	public float damage = 100f;


public void OnDrawGizmos (){
	Gizmos.DrawWireSphere (transform.position, rad);
	}

	public float GetDamage(){
		return damage;
	}

	public void Hit(){
		Destroy (gameObject);
	}
}
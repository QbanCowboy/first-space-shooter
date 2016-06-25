using UnityEngine;
using System.Collections;

public class EnemyCircleBullet : MonoBehaviour {


	float speed; //the bullet speed
	Vector2 _direction; //the direction of the bullet
	bool isReady; //to know when the bullet direction is set

	//set the default values in Awake function
	void Awake(){
		speed = 5f;
		isReady = false;

	}

	// Use this for initialization
	void Start () {
	
	}

	public void SetDirection (Vector2 direction){
		_direction = direction.normalized;
		isReady = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isReady) {
			
			Vector2 position = transform.position;

			position += _direction * speed * Time.deltaTime;

			transform.position = position;
		}
	
	}
		
}

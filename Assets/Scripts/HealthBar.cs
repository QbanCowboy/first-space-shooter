using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public float updateSpeed = 4.0f;
	private Image healthBar;
	private float newFillLevel;

	// Use this for initialization

	void Awake (){
		newFillLevel = 1;
	}

	void Start () {
		healthBar = GetComponent<Image> ();
	
	}

	// Update is called once per frame
	void Update () {
		UpdateBar ();

	}

	public void UpdateBar(){
		if (newFillLevel != healthBar.fillAmount){
			healthBar.fillAmount = Mathf.Lerp (healthBar.fillAmount, newFillLevel, updateSpeed * Time.deltaTime);
		}
	}

	public void fillLevelChange (float newVal){
		newFillLevel = newVal;
	}
}

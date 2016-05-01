using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	
	private Text scoreText;
	string title = "Score: ";
	private int totalScore = 0;

	void Start(){
		scoreText = GetComponent<Text> ();
		Reset ();
	}

	public void Score (int points){
		totalScore += points;
		scoreText.text = title + totalScore;
	}


	public void Reset(){
		totalScore = 0;
		scoreText.text = title + totalScore;
	}

}

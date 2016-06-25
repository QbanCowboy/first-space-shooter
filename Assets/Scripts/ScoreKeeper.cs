using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	
	public static Text scoreText;
	public static string title = "Score: ";
	public static int totalScore = 0;

	void Start(){
		scoreText = GetComponent<Text> ();
		Reset ();
	}

	public void Score (int points){
		totalScore += points;
		if (scoreText) {
			scoreText.text = title + totalScore;
			GetComponent<AudioSource> ().Play ();
		}
	}


	public static void Reset(){
		totalScore = 0;
		if (scoreText) {
			scoreText.text = title + totalScore;
		}
	}

}

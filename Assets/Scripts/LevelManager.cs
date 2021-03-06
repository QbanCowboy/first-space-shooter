using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


	public void LoadLevel (string name){
		Debug.Log ("Level Load requested for: "+name);
		SceneManager.LoadScene (name);
	}
	public void  QuitRequest (){
		Debug.Log ("Quit game requested");
		Application.Quit ();
	}

	public void LoadNextLevel() {
		//SceneManager.GetActiveScene() can return the name of the scene or the build index
		//by using .name or .buildIndex
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}


}

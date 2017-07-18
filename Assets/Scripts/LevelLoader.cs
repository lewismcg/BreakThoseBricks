using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public GameObject quitButton;

	//Basic Function for loading Level
	public void LoadScene (int level){
		Application.LoadLevel (level);
	}

	public void QuitGame(){
		if (Application.isEditor) {
			Debug.Log ("Tried to quit from editor");
		} else if (Application.isWebPlayer) {
			quitButton = GameObject.FindGameObjectWithTag ("Quit");
			quitButton.SetActive (false);
			Debug.Log ("Tried to quit from Web Player");
		} else if (Application.platform == RuntimePlatform.WebGLPlayer) {
			quitButton = GameObject.FindGameObjectWithTag ("Quit");
			quitButton.SetActive (false);
			Debug.Log ("Tried to quit from WebGL Player");
		} else {
			Application.Quit ();
		}
	}
}

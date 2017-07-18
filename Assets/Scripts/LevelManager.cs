using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject[] bricks;
	public int count = 0;
	private GameManager gameManager;
	public string FinishTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bricks = GameObject.FindGameObjectsWithTag ("Brick");
		Debug.Log ("Brick Count: " + bricks.Length);
		count = bricks.Length;

		if (count == 0) {
			Debug.Log ("All bricks are gone!");

			//Wait before returning to Main Scene
			StartCoroutine(Pause());
		}
	}
	IEnumerator Pause(){
		print ("Before Waiting 5 secs");
		//Switch Game Manager state
		gameManager = GameObject.FindObjectOfType<GameManager>();
		gameManager.SwitchState (GameState.Completed);
		gameManager.ChangeText ("You Win!");
		FinishTime = gameManager.formattedTime;

		Debug.Log ("Took " + FinishTime + " to finish the game!");

		yield return new WaitForSeconds(5);

		//Reload Main Menu
		LoadScene(0);
		print("After waiting 5 seconds");
	}
	public void LoadScene(int level){
		Application.LoadLevel (level);
	}
}

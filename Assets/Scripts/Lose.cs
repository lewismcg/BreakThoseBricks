using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour {

	private Ball ball;
	private GameManager gameManager;

	IEnumerator Pause() {
		print ("Before Waiting 2 Seconds:");
		//Switch Gamemanger state
		gameManager = GameObject.FindObjectOfType<GameManager>();
		gameManager.SwitchState (GameState.Failed);

		yield return new WaitForSeconds(2);

		//Find the ball and reset the game start
		ball = GameObject.FindObjectOfType<Ball>();
		ball.gameStarted = false;

		gameManager.EnableButtons ();

		print("after waiting 2 seconds!");
	}

	void OnTriggerEnter2D(Collider2D trigger){
		print(trigger.ToString ());
		if(trigger.name.Equals("Ball")){
			
			print ("Lost Triggered!");

			//Wait before restarting level
			StartCoroutine (Pause ());
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	NotStarted,
	Playing,
	Completed,
	Failed
}
	
//Require an audio source for the object
[RequireComponent(typeof(AudioSource))]

public class GameManager : MonoBehaviour {

	//Sounds to be played when entering one of the gamestates
	public AudioClip StartSound;
	public AudioClip FailedSound;

	private GameState currentState = GameState.NotStarted;

	//All blocks found in this level, to keep track of how many are left
	private Brick[] allBricks;
	private Ball[] allBalls;
	private Paddle paddle;

	public float Timer = 0.0f;
	private int minutes;
	private int seconds;
	public string formattedTime;

	public GameObject restartButton;
	public GameObject mainMenuButton;
	public GameObject buttonBackground;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;

		//find all the blocks in ths scene
		allBricks = FindObjectsOfType(typeof(Brick)) as Brick[];

		//find all the balls in ths scene
		allBalls = FindObjectsOfType(typeof(Ball)) as Ball[];

		paddle = GameObject.FindObjectOfType<Paddle> ();

		print ("Bricks: " + allBricks.Length);
		print ("Balls: " + allBalls.Length);
		print ("Paddle " + paddle);

		//Change Text
		ChangeText("Click to Begin!");

		SwitchState (GameState.NotStarted);
	}
	
	// Update is called once per frame
	void Update () {
		//print (currentState);
		switch (currentState) {
		case GameState.NotStarted:
			{
				//Change Text
				ChangeText ("Click to begin!");
				//Check if the player taps or clicks
				if (Input.GetMouseButtonDown (0)) {
					SwitchState (GameState.Playing);
				}
			}
			break;
		case GameState.Playing:
			{
				Timer += Time.deltaTime;
				minutes = Mathf.FloorToInt (Timer / 60F);
				seconds = Mathf.FloorToInt (Timer - minutes * 60);
				formattedTime = string.Format ("{0:0}:{1:0}", minutes, seconds);

				//Change Text
				ChangeText("Time : " + formattedTime);

				bool allBlocksDestroyed = false;

				//Are there no balls left
				if (FindObjectOfType (typeof(Ball)) == null)
					SwitchState (GameState.Failed);

				if (allBlocksDestroyed)
					SwitchState (GameState.Completed);
			}
			break;
		case GameState.Failed:
			{
				//print ("GameState Failed!");
				ChangeText ("You Lose!");
			}
			break;
		case GameState.Completed:
			{
				bool allBlocksDestroyedFinal = false;

				//Destroy all balls
				Ball[] others = FindObjectsOfType (typeof(Ball)) as Ball[];

				foreach (Ball other in others) {
					Destroy (other.gameObject);
				}
			}
			break;
		}
	}
	public void EnableButtons(){
		//Enable Buttons for when the player loses
		restartButton.SetActive(true);
		mainMenuButton.SetActive (true);
		buttonBackground.SetActive (true);
	}
	public void ChangeText(string text){
		//Find Canvas and modify text
		GameObject canvas = GameObject.Find ("Canvas");
		Text[] textValue = canvas.GetComponentsInChildren<Text> ();
		textValue [0].text = text;
	}
	public void SwitchState(GameState newState){
		currentState = newState;

		switch (currentState) {
		default:
		case GameState.NotStarted:
			break;
		case GameState.Playing:
			GetComponent<AudioSource>().PlayOneShot (StartSound);
			break;
		case GameState.Completed:
			GetComponent<AudioSource>().PlayOneShot (StartSound);
			break;
		case GameState.Failed:
			GetComponent<AudioSource>().PlayOneShot (FailedSound);
			break;
			
		}
	}
}

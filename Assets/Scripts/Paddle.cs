using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Paddle : MonoBehaviour {
	public int i =0;
	//Make the AudioClip Configurable in the editor
	public AudioClip Sound;

	// Use this for initialization
	void Start () {
		print ("This is my first Unity Script!");
	}
	
	// Update is called once per frame
	void Update () {
		//Set Variable for current position
		Vector3 paddlePos = new Vector3(8f, this.transform.position.y,0f);

		//Get Mouse Position
		float mousePos = Input.mousePosition.x / Screen.width * 16;

		//Set new mouse x Position
		paddlePos.x = Mathf.Clamp(mousePos, 0.5f, 15.5f);

		//Change the paddle to match the new x position
		this.transform.position = paddlePos;
	}
	//On Collision will only be called if one of the components is a rigid body
	void OnCollisionEnter2D(Collision2D col){
		//Change the sound pitch if a slowdown powerup has been picked up
		GetComponent<AudioSource> ().pitch = Time.timeScale;

		//Play it once for the collision hit
		GetComponent<AudioSource> ().PlayOneShot (Sound);
	}
}

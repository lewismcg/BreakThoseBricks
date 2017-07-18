using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Paddle paddle;
	public bool gameStarted = false;
	private Vector3 paddleVector;

	//Make the min and max speed configurable in the editor
	public float MinSpeed = 10;
	public float MaxSpeed = 20;

	//To prevent the ball from bouncing horizontally we enforce a minimum vertical velocity
	public float MinVerticalMovement = 0.5f;

	// Use this for initialization
	void Start () {
		//Set the ball on the paddle position
		paddleVector = this.transform.position - paddle.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted) {
			this.transform.position = paddle.transform.position + paddleVector;
			if (Input.GetMouseButtonDown (0)) {
				print ("Mouse Clicked!");
				gameStarted = true;
				this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range(-2.0f,2.0f), 10f);
				this.GetComponent<TrailRenderer> ().enabled = true;
			}
		}
		launchBall ();
	}
	public void launchBall(){
		//Get Current speed and direction
		Vector2 direction = GetComponent<Rigidbody2D>().velocity;
		//float speed = 20f;
		float speed = direction.magnitude;
		direction.Normalize();

		//Make sure the ball never goes straight horizontal or else it could never come down on the paddle
		if (direction.x > -MinVerticalMovement && direction.x < MinVerticalMovement) {
			//Adjust the x, make sure it goes in a direction within the range limit set
			direction.x = direction.x < 0 ? - MinVerticalMovement : MinVerticalMovement;

			//Adjust the y position make sure it keeps going in the direction it was going (up or down)
			direction.y = direction.y < 0 ? - MinVerticalMovement : MinVerticalMovement;

			//Apply it back to the ball
			GetComponent<Rigidbody2D>().velocity = direction * speed;
		}
		if (speed < MinSpeed || speed > MaxSpeed) {
			// Limit the speed so that so that it is always above min and below max
			speed = Mathf.Clamp(speed, MinSpeed, MaxSpeed);

			//Apply the limit
			//NOTE: we dont use delta time here as we set the velocity once, not every frame
			GetComponent<Rigidbody2D>().velocity = direction * speed;
		}
	}
}

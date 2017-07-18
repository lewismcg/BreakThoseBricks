using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : BasePowerUp {
	//Ball Prefab instanciated when the power up is picked up
	public GameObject BallPreFab;

	//Make the Min and Max speed configureable in the editor
	public float MinSpeed = 10;
	public float MaxSpeed = 20;

	//To Prevent the ball from bouncing horizontally we enforce a minimum vertical movement
	public float MinVerticalMovement = 0.5f;

	//Override the OnPickUp methoid of the Parent class
	protected override void OnPickUp(){
		//Call the default Behaviour of the base class
		base.OnPickUp();
		print ("On Pick Up Call!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D c){
		print ("Extra Collision!");
		print(c.gameObject.name);
		if (c.gameObject.name == "Paddle") {
			print ("Extra Collision Paddle!");
			launchBall ();
		}
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

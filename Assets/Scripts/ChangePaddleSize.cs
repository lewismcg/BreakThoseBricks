using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePaddleSize : BasePowerUp {

	//How much units the paddle will change when this powerup is picked up
	//Can also be ngative to reduce addle size
	public Vector3 SizeIncrease = Vector3.zero;

	public Vector3 MinPaddleSize = new Vector3 (0.1f, 0.1f, 0.4f);

	//Notice how we override the OnPickup Method of the base class
	protected override void OnPickUp ()
	{
		//Call the default behavior
		base.OnPickUp ();

		//Then do the powerup specific behaviour, changing the size in this case
		Paddle p = FindObjectOfType(typeof(Paddle)) as Paddle;
		p.transform.localScale += SizeIncrease;

		//Limit the minimal size of the paddle
		Vector3 size = p.transform.localScale;
		if (size.x < MinPaddleSize.x) {
			size.x = MinPaddleSize.x;	
		}
		if (size.y < MinPaddleSize.y) {
			size.y = MinPaddleSize.y;	
		}
		if (size.z < MinPaddleSize.z) {
			size.z = MinPaddleSize.z;	
		}
		p.transform.localScale = size;
	}
}

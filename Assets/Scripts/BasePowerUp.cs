using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(AudioSource))]

public class BasePowerUp : MonoBehaviour {

	public float DropSpeed = 1; //How fast does it drop?
	public AudioClip Sound; //Sound played when powerup is picked up

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator OnTriggerEnter2D(Collider2D other){
		//Only Interact with the paddle
		print(other.ToString());
		if (other.name == "Paddle") {
			//Notify the derived Pickups that it is being Picked up
			OnPickUp();

			//Disable Further Collisions
			GetComponent<Collider2D>().enabled = false;
			GetComponent<Renderer>().enabled = false;

			//Change the sound pitch 
			GetComponent<AudioSource>().pitch = Time.timeScale;

			//Play audio and wait
			GetComponent<AudioSource>().PlayOneShot(Sound);
			yield return new WaitForSeconds (Sound.length);
		}
	}
	//Every Powerup that derives for this class should implement from this class
	protected virtual void OnPickUp(){
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Make sure an AudioSource component on the GameObject where the script is added.
[RequireComponent(typeof(AudioSource),typeof(Animation))]

public class Brick : MonoBehaviour {

	public int maxHits;
	public int timesHit;
	private bool brickIsDestroyed = false;

	//Define the audio Clip and pitch
	public AudioClip Sound;
	public float PitchStep = 0.05f;
	public float MaxPitch = 1.3f;

	//Make sure the current pict value is global
	public static float pitch = 1;

	//falling variables
	public bool FallDown = false;

	[HideInInspector]
	public bool BlockIsDestroyed = false;

	private Vector2 velocity = Vector2.zero;

	// Use this for initialization
	void Start () {
		timesHit = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (FallDown && velocity != Vector2.zero) {
			Vector2 pos = (Vector2)transform.position;
			pos += velocity * Time.deltaTime;
		}
	}

	void OnBecameInvisible(){
		BlockIsDestroyed = true;
		Destroy (gameObject);
	}

	private IEnumerator OnCollisionExit2D (Collision2D c){
		if (timesHit == maxHits) {
			print ("Destroyed on Exit!");

			print ("Play Woggle!");
			GetComponent<Collider2D> ().enabled = false;

			//Play the woggle animation
			GetComponent<Animation> ().Play ();

			//wait here for the length of time of the Woggle Animation
			yield return new WaitForSeconds (GetComponent<Animation> () ["Woggle"].length);

			//Animation has finished now decide to falldown or just disapear
			if (FallDown) {
				print ("Falling!");
				//Fall down to the direction the ball hit it, with a random speed and plus a little "gravity"
				velocity = new Vector2 (0, Random.Range (1, 12.0f) * -1);
			} else {
				GetComponent<Renderer> ().enabled = false;
			}
			Destroy (gameObject);
		} else {
			print ("Max Hits not reached!");
		}
	}
		
	void OnCollisionEnter2D(Collision2D col){
		timesHit++;
		print ("Ouch You Hit Me this many time: " + timesHit );

		print ("Playing Brick Sound!");
		//Increase pitch
		pitch += PitchStep;

		//Limit Pitch
		if (pitch > MaxPitch)
			pitch = 1;

		//Apply Pitch
		GetComponent<AudioSource>().pitch = pitch;

		//Play it once for the collision hit
		GetComponent<AudioSource>().PlayOneShot(Sound);

		StartCoroutine (OnCollisionExit2D (col));
	}

}

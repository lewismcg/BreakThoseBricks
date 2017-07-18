using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PowerUpDrop : MonoBehaviour {

	public BasePowerUp PowerUpPreFab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D c){
		GameObject.Instantiate (PowerUpPreFab, this.transform.position, Quaternion.identity);
	}
}

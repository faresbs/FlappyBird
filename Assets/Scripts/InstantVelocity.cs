using UnityEngine;
using System.Collections;

public class InstantVelocity : MonoBehaviour {

	public Vector2 velocity = new Vector2(-60, 0);
	private Rigidbody2D body2d;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
	}
	//FixedUpdate should be used instead of Update when dealing with Rigidbody
	void FixedUpdate(){
		body2d.velocity = velocity;
	}
}

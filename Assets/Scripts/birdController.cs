using UnityEngine;
using System.Collections;

public class birdController : MonoBehaviour {
	private Rigidbody2D body2d;
	public float jumpheight;

	// Use this for initialization
	void Start () {
		body2d = gameObject.GetComponent<Rigidbody2D> ();
		}

	// Update is called once per frame
	void Update () {
		var rotation = Mathf.Lerp (10,-60,Mathf.Abs(body2d.velocity.y)/90);
		transform.localRotation = Quaternion.Euler (0, 0, rotation);
		//press any key to flap
		if(Input.anyKeyDown){
			body2d.velocity = new Vector2 (body2d.transform.position.x, jumpheight);
		}
	}
}
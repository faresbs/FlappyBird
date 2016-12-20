using UnityEngine;
using System.Collections;

public class birdController : MonoBehaviour {
	private Rigidbody2D body2d;
	public float jumpheight;

	public delegate void OnDestroy();
	public event OnDestroy DestroyCallBack;

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

	void OnTriggerEnter2D(Collider2D other){
		if ((other.tag == "limit") || (other.tag == "pipe")) {
			if(DestroyCallBack != null)
			{
				DestroyCallBack();
			}
			Destroy (this.gameObject);

			//destroy the pipe that the player collides with
			if (other.tag == "pipe") {
				Destroy (other.gameObject);
			}
		}
	}
}
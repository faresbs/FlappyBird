using UnityEngine;
using System.Collections;
//Destroy object(in this case the pipe), if he surpass the offset limit
public class DestroyOffScreen : MonoBehaviour {

	public delegate void OnDestroy();
	public event OnDestroy DestroyCallBack;

	public float offset = 20f;
	float offsetX;
	Rigidbody2D body2d;

	void Awake () {
		body2d = GetComponent<Rigidbody2D> ();
	}

	void Start(){
		offsetX = (Screen.width * PerfectCamera.pixelToUnit) + offset; 
	}

	// Update is called once per frame
	void Update () {
		var posX = transform.position.x;
		var dirX = body2d.velocity.x;
		if (Mathf.Abs (posX) > offsetX) {
			//check if the DestroyCallBack have a value
			if(DestroyCallBack != null)
			{
				DestroyCallBack();
			}
			if (dirX < 0 && posX < offsetX) {
				Destroy (this.gameObject);
			}
		}

	}
}

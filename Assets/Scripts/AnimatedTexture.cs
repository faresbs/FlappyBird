using UnityEngine;
using System.Collections;

//make the background moves
public class AnimatedTexture : MonoBehaviour {
	public  Vector2 speed = Vector2.zero;
	private Vector2 offset = Vector2.zero;
	private Material material;

	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer> ().material;
		offset = material.GetTextureOffset ("_MainTex");
	}
	
	// Update is called once per frame
	//increment the offset and reapply it to the material
	//make sure to set the wrap mode in the texture settings of the background on 'Repreat'
	void Update () {
		offset += speed * Time.deltaTime;
		material.SetTextureOffset ("_MainTex", offset);
	}
}

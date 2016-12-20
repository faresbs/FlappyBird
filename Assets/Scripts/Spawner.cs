using UnityEngine;
using System.Collections;

//Instantiate a pipe every delay
//a coroutine is calling a function that will complete before returning, they are used instead of update
//check in the https://docs.unity3d.com/ScriptReference/index.html (unity3D documentation) to learn more about coroutines
public class Spawner : MonoBehaviour
{

    public GameObject prefab;
    public float delay;
    public bool active = true;

	public float min = -70;
	public float max = 20;
	private float pos;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(PipeGenerator());
    }

    IEnumerator PipeGenerator()
    {
		
        yield return new WaitForSeconds(delay);

        if (active)
        {
            var newTransform = transform;
			pos = Random.Range(min, max);
			newTransform.position = new Vector3 (newTransform.position.x, pos,newTransform.position.z);
			Instantiate(prefab, newTransform.position, Quaternion.identity);
        }

        StartCoroutine(PipeGenerator());

    }

}
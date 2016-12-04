using UnityEngine;
using System.Collections;

//Instantiate a pipe every delay
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
    using UnityEngine;
using System.Collections;

//camera will fit the screen
public class PerfectCamera : MonoBehaviour
{

    public static float pixelToUnit = 1f;
    public static float scale = 1f;

    public Vector2 nativeResolution = new Vector2(240, 160);

    void Awake()
    {
        //reference to the camera 
        var camera = GetComponent<Camera>();

		//if the camera is in orthographic (true) or perspective (false)
        if (camera.orthographic)
        {
            scale = Screen.height / nativeResolution.y;
            pixelToUnit *= scale;
            //change the size of the camera
            camera.orthographicSize = (Screen.height / 2.0f) / pixelToUnit;
        }
    }
}

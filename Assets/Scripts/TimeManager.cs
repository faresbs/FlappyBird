using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {
    //duration is the time of transition from current time to the new time
	public void ManipulateTime(float newTime, float duration){
        //timeScale is the scale at which the time is passing. This can be used for slow motion effects.
        //When timeScale is 1.0 the time is passing as fast as realtime
        // if timeScale = 0 we need to make a bit faster so can that everything else can start executing as we go back to the normal timeScale
        if (Time.timeScale == 0)
			Time.timeScale = 0.1f;

		StartCoroutine (FadeTo (newTime, duration));
	}

	IEnumerator FadeTo(float value, float time){

		for (float t = 0f; t < 1; t += Time.deltaTime / time) {
            //lerp(max,min,t)
            //the parameter t is clamped to the range of [0,1]
			Time.timeScale = Mathf.Lerp(Time.timeScale, value, t);
            //when time is almost zero
			if(Mathf.Abs(value - Time.timeScale) < .01f){
				Time.timeScale = value;
				return false;
			}

			yield return null;
		}

	}
    //when the player dies, we'll slow time to give us the effect like the game is stopping

}

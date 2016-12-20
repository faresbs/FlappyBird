using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//for the time span
using System;

public class GameManager : MonoBehaviour {

    private bool beatBestTime;
    private float timeElapsed = 0f;
    private float bestTime = 0f;

    public Text continueText;
    public Text scoreText;
    private float blinkTime = 0f;
    private bool blink;

    //reference to the player instance
    public GameObject playerPrefab;
    //reference to instance of the player 
    private GameObject player;  
	private Spawner spawner;

    private TimeManager timeManager;
    private bool gameStarted;

	void Awake(){  
       
		//get the reference of the component Spawner(the script Spawner) in the Spawner object
		spawner = GameObject.Find ("spawner").GetComponent<Spawner> ();
        timeManager = GetComponent<TimeManager>();
	}

	// Use this for initialization
	void Start () {
     
		spawner.active = false;

        //set the timeScale to zero, this way we don't have to keep track  or manage whenever the backgrounds are scrolling or not
        Time.timeScale = 0;
       
        continueText.text = "Press Any Button To Start";

        //reference on the BestTime
        bestTime = PlayerPrefs.GetFloat("BestTime");
    }
	
	// Update is called once per frame
	void Update () {
   //detect if the game is over then we gonna listen for any key to turn time back to default value of 1 to reset the game 
	if(!gameStarted && Time.timeScale == 0)
        {
            if (Input.anyKeyDown)
            {
				Time.timeScale = 1;
				ResetGame();
            }
        }
    
    //make the text Blink
        if (!gameStarted)
        {
            blinkTime++;
            //blink every 40th frame (40 % 40 = 0)
            if (blinkTime % 40 == 0)
            {
                blink = !blink;
            }

            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);

            //if it beats the high score it will change the color of the text from white to yellow
            var colorText = beatBestTime ? "#FF0" : "#FFFF";
            //similar to when we do it in HTML
            scoreText.text = "TIME: " + FormatTime(timeElapsed) +"<color=" +colorText+">        BEST: " + FormatTime(bestTime)+"</color>";
        } else {
            //increment the time 
            timeElapsed += Time.deltaTime;
            scoreText.text = "TIME: " + FormatTime(timeElapsed);
        }
    }


    void OnplayerKilled()
    {
        spawner.active = false;
		 
        //unlink the connection to the DestroyCallBack delicate from the OnplayerKilled method 
		var playerDestroyScript = player.GetComponent<birdController>();
        playerDestroyScript.DestroyCallBack -= OnplayerKilled;

		
        //reset the velocity because we want the player to start with zero velocity so that he can fall straight down from the top
		player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        //set the time to zero and do it over 5.5 seconds
        timeManager.ManipulateTime(0, 5.5f);

        gameStarted = false;

        continueText.text = "Press Any Button To Restart";

        //keep track of the best score
        if(timeElapsed > bestTime)
        {
            bestTime = timeElapsed;
            //PlayerPrefs is a class that allow us to save values into unity similar to how coockies or local storage work in HTML
            //PlayerPrefs(ID,value)
            PlayerPrefs.SetFloat("BestTime",bestTime);
            beatBestTime = true;
        }
    }

    //reset the game: active the spawner and create the player instance
    public void ResetGame()
    {
        spawner.active = true;
        //create the instance of the player in that pos (center of the screen)
		player = (GameObject)Instantiate(playerPrefab, new Vector3(0, 0, 0),Quaternion.identity);

        //reference to the destroy script
		var playerDestroyScript = player.GetComponent<birdController>();
        //connect up the delegate and point it to an other method in our case OnplayerKiller
        playerDestroyScript.DestroyCallBack += OnplayerKilled;
        
        //what this does it tells to the playerDestroyScript that when DestroyCallBack() get called it's acually gonna call this method
        //OnplayerKilled inside of our game manager

        gameStarted = true;
        //hide the text with alpha 
        continueText.canvasRenderer.SetAlpha(0);

        //reset the time
        timeElapsed = 0;

        beatBestTime = false;
    }
       
    
    //time span
        string FormatTime(float value)
    {
        TimeSpan t = TimeSpan.FromSeconds(value);
        //convert t from seconds to minutes and seconds
		return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

}

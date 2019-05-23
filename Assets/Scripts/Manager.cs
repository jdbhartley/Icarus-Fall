using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	public Rect gameBounds;
	public Rect windowRect = new Rect((Screen.width/2), (Screen.height/2), 400, 300);
	
	//Score vars
	public double score;
	public int levelTime;
	public bool died1=false;
	
	//Loading scene
	public GameObject loadingscene;
	
	public void IncreaseScore (int amount) {
		int multiplier = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerShip>().GetMultiplier();
		Debug.Log (multiplier);
		if (multiplier > 0) {
		score = score + (amount*multiplier);
		}
		else {
			score += amount;
		}
	}
	
	public void GameEnd (bool died) {
		Time.timeScale = 0;
		Debug.Log ("Game Ended, score = " + score.ToString ());
		died1 = died;
	}
	
	void OnGUI () {
		GUI.Label(new Rect(0,0,500, 30), "Score: " + score.ToString ());
		if (died1==true)
		{
			windowRect = GUI.Window(0, windowRect, FailedWindow, "Level Failed.");
		}
		else if (Time.time>=levelTime)
		{
			windowRect = GUI.Window(0, windowRect, CompleteWindow, "Level Complete!");
		}
	}
	
	
// Make the contents of the window
 void FailedWindow(int windowID) {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Retry"))
            print("Got a click");
        
    }
	
 void CompleteWindow(int windowID) {
    if (GUI.Button(new Rect(10, 20, 100, 20), "Continue"))
        print("Got a click");
    
}
}

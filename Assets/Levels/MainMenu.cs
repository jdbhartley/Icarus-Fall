using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GUIText starttext;
	bool ShowMenu;
	bool ShowNameMenu;
	bool ShowConfirmMenu;
	bool ShowLoadMenu;
	public Font guiFont;
	string pilotName = "";
	
	public GameObject startButton;
	public GameObject highScoreButton;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
//	if (OT.Touched(gameObject) && starttext)
//		{
//			Debug.Log ("Touched!");
//			Destroy (starttext);
//			ShowMenu = true; 
//			ShowLoadMenu = true;
//		}
		
		if(OT.Touched (startButton)) {
			ShowMenu = true;
			ShowLoadMenu = true;
		}
	
	}
	
	void OnGUI () {
		GUI.skin.font = guiFont;
		if(ShowMenu)
		{
			//gray overlay screen
			GUI.Box (new Rect (-100,-100,Screen.width +100, Screen.height+100), "");
			// Make a group on the center of the screen
			GUI.BeginGroup (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
	
			// We'll make a box so you can see where the group is on-screen.
			if(ShowLoadMenu == true) {
				GUI.Box (new Rect (0,0,400,200), "");
				if (GUI.Button (new Rect (120,30,160,60), "New Game")) {
					ShowNameMenu = true;
					ShowLoadMenu = false;
				}
				
				if (GUI.Button (new Rect (120,105,160,60), "Continue")) {
					//load game
				}
			}
			
			if(ShowNameMenu == true) {
				GUI.Box (new Rect (0,0,400,200), "Pilot Name");
				pilotName = GUI.TextField (new Rect (120, 30, 160, 60), pilotName);
				
				if (GUI.Button (new Rect (120,105,160,60), "Done")) {
					ShowConfirmMenu = true;
					ShowNameMenu = false;
				}
			}
			
			if(ShowConfirmMenu == true) {
				GUI.Box (new Rect (0,0,400,200), "Start new game and erase \n all previous save data?");
				if (GUI.Button (new Rect (120,60,160,60), "Continue")) {
					PlayerPrefs.DeleteAll ();
					PlayerPrefs.SetString ("pilotName", pilotName);
					PlayerPrefs.Save ();
					Application.LoadLevel (1);
				}
				
				if (GUI.Button (new Rect (120,135,160,60), "Cancel")) {
				}
			}
	
			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();
		}
	}
}

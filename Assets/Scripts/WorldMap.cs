using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMap : MonoBehaviour {
	public GameObject objMoon;
	public Texture blankwhite;
	public Font guiFont;
	public Rect textarea;
	public Rect button;
	public Planet currPlanet;
	public Planet moon;
	
	// Use this for initialization
	void Start () {
	LoadPlanetData ();
	}
	
	void LoadPlanetData () {
		moon.name = "The Moon";
		moon.highscore = PlayerPrefs.GetInt ("moonscore");
		moon.complete = PlayerPrefs.GetInt ("mooncomplete");
		moon.mesh = objMoon;
		moon.levelIndex = 2;
	}
	
	// Update is called once per frame
	void Update () {
	if (OT.Touched(objMoon))
		{
			(objMoon.GetComponent("Halo") as Behaviour).enabled = true;
			currPlanet = moon;
		}
	}
	
	void OnGUI() {
		GUI.skin.font = guiFont;
			if(currPlanet.levelIndex > 1) {
			GUI.TextArea (textarea, "Level " + (currPlanet.levelIndex - 1).ToString () + ": " + currPlanet.name + "\n" + "High Score: " + currPlanet.highscore);
			if(GUI.Button (button, "Start Level")) {
				Application.LoadLevel (currPlanet.levelIndex);
			}   
		}
	}
	
	public struct Planet
	{
		public string name;
		public int highscore;
		public int complete;
		public GameObject mesh;
		public int levelIndex;
		
	}
}

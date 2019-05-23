using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Manager {
	
	public SeamlessScroll sun;
	public int sunTime;
	
	//Loading
	void Awake() {
		loadingscene.renderer.enabled = true;
//		LoadProjectilePool ();
	}
	
	// Use this for initialization
	void Start () {
		Destroy (loadingscene);
	}
	
	// Update is called once per frame
	void Update () {
	if (Time.timeSinceLevelLoad >= levelTime)
		{
			sun.enabled = false;
			GameEnd (false);
		}
		
	if (Time.timeSinceLevelLoad >= sunTime)
		{
			sun.enabled = true;
		}
	}
}

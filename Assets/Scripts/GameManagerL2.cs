using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerL2 : Manager {
	
	//Loading
	void Awake() {
//		loadingscene.renderer.enabled = true;
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
			GameEnd (false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class DieAfterSeconds : MonoBehaviour {
	
	public float DieAfterXSeconds;
	float t;
	// Use this for initialization
	void Start () {
	t = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
	if (Time.timeSinceLevelLoad > t + DieAfterXSeconds) {
			Destroy (this.gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollThenDie : MonoBehaviour {
	
	public float speed;
	public List<int> spawnTimes = new List<int>();
	
	// Update is called once per frame
	void Update () {
		foreach (int i in spawnTimes) {
			if (i == (int)Time.timeSinceLevelLoad)
			{
				this.transform.position = new Vector3(28, Random.Range (-10f, 10f), 0);
			}
		}
		
		if (this.transform.position.x > -30) {
			this.transform.position = (this.transform.position - (new Vector3(speed, 0, 0) * Time.deltaTime));
		}
	}
}

using UnityEngine;
using System.Collections;

public class Cacheable : MonoBehaviour {
	public bool inUse = false;
	public int maxNumberOnScreen;
	public ObjectCache cache;
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.x > 19)
			inUse = false;
		if (this.transform.position.x < -19)
			inUse = false;
		if (this.transform.position.y < -14)
			inUse = false;
	}
}

using UnityEngine;
using System.Collections;

public class BulletPowerup : MonoBehaviour {
	public bool scatterShot;
	public bool shotSpeed;
	
	// Update is called once per frame
	void Update () {
	this.GetComponent<OTSprite>().position -= (new Vector2(5, 0) * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player") {
			if (scatterShot == true)
			{
				col.GetComponent<PlayerShip>().scatterShot = true;
			}
			if (shotSpeed == true)
			{
				if (col.transform.parent.GetComponent<HandleInput>().shootDelay > 0.2)
					col.transform.parent.GetComponent<HandleInput>().shootDelay /= 2;
			}
			Destroy(this.gameObject);
		}
	}
}

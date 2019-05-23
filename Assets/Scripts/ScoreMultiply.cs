using UnityEngine;
using System.Collections;

public class ScoreMultiply : MonoBehaviour {
	public int multiplyAmount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<OTSprite>().position -= (new Vector2(5, 0) * Time.deltaTime);
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player") {
			col.GetComponent<PlayerShip>().IncreaseMultiplier(multiplyAmount);
			Destroy(this.gameObject);
		}
	}
}

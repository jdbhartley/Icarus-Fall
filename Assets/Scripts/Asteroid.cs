using UnityEngine;
using System.Collections;

public class Asteroid : Cacheable {

	float speedhoriz;
	float speedvert;
	
	public bool rotate;
	public GameObject explosion;
	
	public int health;
	
	// Use this for initialization
	void Start () {
	Debug.Log("Called Start in Asteroid");
		speedvert = Random.Range (4,10);
		speedhoriz = Random.Range (2, 7);
	}
	
	// Update is called once per frame
	void Update () {
		if (rotate == true)
		{
			this.GetComponent<OTSprite>()._rotation = this.GetComponent<OTSprite>()._rotation + 150 * Time.deltaTime;
		}
		
	this.transform.position = this.transform.position - (new Vector3(speedhoriz, speedvert, 0) * Time.deltaTime);
		
		if (this.transform.position.y < -21)
			this.transform.parent.gameObject.GetComponent<ObjectCache>().StopObject (this.gameObject);
	}
	
	public void TakeDamage(int amount) {
		health -= amount;
		
		if (health < 0 )
		{
			Explode ();
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		//If it hit and enemy AND were not an enemy bullet
		if (col.CompareTag ("Enemy") & col.name != this.name) {
			col.GetComponent<EnemyStandard>().DoDamage (1000, false);
			Explode ();
			Debug.Log ("Hit an Enemy: " + col.name);
		}
		//if were an enemy bullet and we hit the player
		else if (col.CompareTag ("Player"))
		{
			col.GetComponent<PlayerShip>().TakeDamage (100);
			col.GetComponent<PlayerShip>().Explode ();
			Explode ();
			Debug.Log ("Hit the Player");
		}
		else if (col.CompareTag ("MotherShip"))
		{
			Explode ();
		}
	}
	
	void Explode()
	{
		GameObject e = (GameObject)Instantiate (explosion);
		e.GetComponent<OTAnimatingSprite>().position = this.transform.position;
		e.GetComponent<OTAnimatingSprite>().Play ("YellowGreenExplosion");
		this.transform.parent.gameObject.GetComponent<ObjectCache>().StopObject (this.gameObject);
	}
	
}

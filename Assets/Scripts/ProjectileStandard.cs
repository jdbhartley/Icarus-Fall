using UnityEngine;
using System.Collections;

public class ProjectileStandard : Cacheable {
	float projectileSpeed;
	int projectileDamage;
	public int poolIndex;
	bool goLeft;
	bool whitePolarity;
	OTSprite sprite;
	bool justSetup;
	
	public GameObject burst;
	
	//Use for loading
	void Awake () {
		sprite = this.GetComponent<OTSprite>();
	}
	
	void SetupSprite()
	{
		//If going left, flip horizontally
		if (goLeft == true)
			sprite.flipHorizontal = true;
		else
			sprite.flipHorizontal = false;

		//Check polarity
		if (whitePolarity == true) 
		{
			sprite.frameIndex = 1;
		}
		else
		{
			sprite.frameIndex = 0;
		}
		
		justSetup = true;
	}
	
	public void SetupProjectile(bool goleft, int damage, float speed, bool whitepolarity, bool justsetup, float rotation)
	{
		goLeft = goleft;
		projectileDamage = damage;
		projectileSpeed = speed;
		whitePolarity = whitepolarity;
		justSetup = justsetup;
		this.gameObject.GetComponent<OTSprite>().rotation = rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (inUse == true && justSetup == false)
		{
			SetupSprite ();
		}
		//move the bullet
		if (goLeft == false) {
			this.transform.Translate (Vector3.right * (projectileSpeed * Time.deltaTime));
		}
		else if (goLeft == true) {
			this.transform.Translate (Vector3.left * (projectileSpeed * Time.deltaTime));
		}
		
		//Stop the bullet and reset it
		if (this.transform.position.x > 16.1f || this.transform.position.x < -16.1f) {
			this.transform.parent.GetComponent<ObjectCache>().StopObject (this.gameObject);
		}
		if (this.transform.position.y < -10.4f || this.transform.position.y > 10.8f) {
			this.transform.parent.GetComponent<ObjectCache>().StopObject (this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		//If it hit and enemy AND were not an enemy bullet
		if (col.CompareTag ("Enemy") && goLeft == false) {
			col.gameObject.GetComponent<EnemyStandard>().DoDamage (projectileDamage, true);	
			GameObject b = (GameObject)Instantiate (burst);
			b.GetComponent<OTSprite>().position = this.transform.position + new Vector3(Random.Range (0.5f, 2f),0,0);
			this.transform.parent.GetComponent<ObjectCache>().StopObject (this.gameObject);
		}
		//if were an enemy bullet and we hit the player
		else if (col.CompareTag ("Player") && goLeft == true)
		{
			col.GetComponent<PlayerShip>().TakeDamage (projectileDamage);
			this.transform.parent.GetComponent<ObjectCache>().StopObject (this.gameObject);
		}
		else if (col.CompareTag ("Obstacle") && goLeft == false)
		{
			col.GetComponent<Asteroid>().TakeDamage (projectileDamage);
			this.transform.parent.GetComponent<ObjectCache>().StopObject (this.gameObject);
		}
	}
}

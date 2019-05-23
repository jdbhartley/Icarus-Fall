using UnityEngine;
using System.Collections;

public class EnemyStandard : Cacheable {
	public int MaxHP;
	public int HP;
	public float shipSpeed;
	public int projectileDamage;
	public float projectileSpeed;
	public bool whitepolarity;
	public Manager manager;
	ObjectCache bulletCache;
	public GameObject explosion;
	OTSprite sprite;
	public float shootDelay;
	public int collisionDamage;
	public bool scatterShot;
	
	void Awake () {
		bulletCache = GameObject.FindGameObjectWithTag ("EnemyBulletCache1").GetComponent<ObjectCache>();
	}
	
	// Use this for initialization
	protected virtual void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager>();
		Debug.Log (manager);
		HP = MaxHP;
		StartShooting();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		
		//If were off the screen, destroy us
		if (this.transform.position.x < -19)
		{
			HP = MaxHP;
			this.transform.parent.gameObject.GetComponent<ObjectCache>().StopObject (this.gameObject);
		}
	
	}
	
	protected virtual void Move() {
		this.transform.Translate ((Vector3.left) * shipSpeed * Time.deltaTime);
	}
	
	protected virtual void StartShooting() {
		StartCoroutine (ShootBySeconds (shootDelay));
	}
	
	IEnumerator ShootBySeconds(float delay)
	{
		while(1==1) {
			if(scatterShot == false) {
				Shoot ();
				this.audio.Play ();
			}
			else if (scatterShot == true) {
				ScatterShot();
				this.audio.Play ();
			}
		yield return new WaitForSeconds(delay);
		}
	}
	
	void Shoot () {
		ProjectileStandard bscript = bulletCache.SpawnObject(this.transform.position).GetComponent<ProjectileStandard>();
		bscript.SetupProjectile (true, projectileDamage, projectileSpeed, whitepolarity, false, 0);
	}
	
	//scattershot!
	void ScatterShot () {
		ProjectileStandard bscript = bulletCache.SpawnObject(this.transform.position).GetComponent<ProjectileStandard>();
		ProjectileStandard bscript1 = bulletCache.SpawnObject(this.transform.position).GetComponent<ProjectileStandard>();
		ProjectileStandard bscript2 = bulletCache.SpawnObject(this.transform.position).GetComponent<ProjectileStandard>();
		bscript.SetupProjectile (true, projectileDamage, projectileSpeed, whitepolarity, false, 0);
		bscript1.SetupProjectile (true, projectileDamage, projectileSpeed, whitepolarity, false, 20);
		bscript2.SetupProjectile (true, projectileDamage, projectileSpeed, whitepolarity, false, -20);
		
		if (this != null)
		{
			this.audio.Play ();
		}
	}
	
	void Explode(bool full)
	{
		if (full == true) 
		{
			GameObject e = (GameObject)Instantiate (explosion);
			e.GetComponent<OTAnimatingSprite>().position = this.transform.position;
			e.GetComponent<OTAnimatingSprite>().Play ("YellowGreenExplosion");
			DropPowerup ();
		}
		else
		{
			GameObject e = (GameObject)Instantiate (explosion);
			e.GetComponent<OTAnimatingSprite>().position = this.transform.position;
			e.GetComponent<OTAnimatingSprite>().Play(0, 0, 0.1f);
			Debug.Log ("Little Explosion");
		}
	}
	
	void DropPowerup() {
		//get a random number 
		int randomNumber = Random.Range (1, 15);
		GameObject go;
		Debug.Log (randomNumber + " is the random number");
		switch (randomNumber) {
		case 5:
			go = (GameObject)GameObject.Instantiate (Resources.Load ("multiplier2"));
			go.GetComponent<OTSprite>().position = new Vector2(this.transform.position.x, this.transform.position.y);
			break;
		case 10:
			if (2 == (int)Random.Range (1, 2)) {
				go = (GameObject)GameObject.Instantiate (Resources.Load ("multiplier4"));
				go.GetComponent<OTSprite>().position = new Vector2(this.transform.position.x, this.transform.position.y);
			}
			break;
		case 12: 
			if (GameObject.FindGameObjectWithTag ("Player").transform.parent.GetComponent<HandleInput>().shootDelay > 0.2f)
			{
			go = (GameObject)GameObject.Instantiate (Resources.Load ("rapidshot"));
			go.GetComponent<OTSprite>().position = new Vector2(this.transform.position.x, this.transform.position.y);
			}
			break;
		case 14:
			if (GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerShip>().scatterShot != true) {
				go = (GameObject)GameObject.Instantiate (Resources.Load ("multishot"));
				go.GetComponent<OTSprite>().position = new Vector2(this.transform.position.x, this.transform.position.y);
			}
			break;
			
				
		}
	}
	
	public void DoDamage (int damage, bool playerHit) {
		//Explode (false);
		HP = HP - damage;
		
		//If it dies
		if (HP <= 0) {
			Explode (true);
			if (playerHit == true) {
				manager.IncreaseScore (MaxHP);
			}
			HP = MaxHP;
			this.transform.parent.gameObject.GetComponent<ObjectCache>().StopObject (this.gameObject);
		}
	}
}

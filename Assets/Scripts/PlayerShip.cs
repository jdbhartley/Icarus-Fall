using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShip : MonoBehaviour {
	public float maxHP; //Max Health points
	float HP; //Health points
	public float maxSP; //Max Shield points
	float SP; //Shield points
	
	public int Lives; // Player lives
	
	public bool whitepolarity = true;
	public float projectileSpeed;
	public int projectileDamage;
	int currBullet = 0;
	ObjectCache bulletCache;
	public GameObject explosion;
	GameManager manager;
	GameObject flash;
	int multiplier = 0;
	
	public GameObject healthBar;
	public GameObject shieldBar;
	
	public int specialWeapon;
	public int weaponEnergy;
	
	public bool scatterShot;
	
	Vector2 barSize;
	
	void Awake () {
		bulletCache = GameObject.FindGameObjectWithTag ("BulletCache").GetComponent<ObjectCache>();
	}
	
	// Use this for initialization
	void Start () {
		HP = maxHP;
		SP = maxSP;
	}
	
	public void IncreaseMultiplier(int increase) {
		multiplier += increase;
	}
	
	public int GetMultiplier() {
		return multiplier;
	}
	
	// Update is called once per frame
	void Update () {
		CheckPolarity ();
	}
	
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Enemy")
		{
			col.GetComponent<EnemyStandard>().DoDamage (100, true);
			TakeDamage (col.GetComponent<EnemyStandard>().collisionDamage);
		}
	}
	
	public void FireSpecial() {
		switch(specialWeapon) {
		case 0: //MotherShip Attack
			if (weaponEnergy >= 100) {
				GameObject.FindWithTag ("MotherShip").GetComponent<MotherShip>().Attack();
				weaponEnergy = 0;
			}
			break;
		}
	}
	
	public void Explode()
	{
		GameObject e = (GameObject)Instantiate (explosion);
		e.GetComponent<OTAnimatingSprite>().position = this.transform.position;
		e.GetComponent<OTAnimatingSprite>().Play ("RedExplosion");
	}
	
	public void Shoot () {
		ProjectileStandard bscript = bulletCache.SpawnObject (this.transform.GetChild(0).transform.position).GetComponent<ProjectileStandard>();	
		bscript.SetupProjectile(false, projectileDamage, projectileSpeed, whitepolarity, false, 0);
		this.audio.Play ();
	}
	
	public void ScatterShot() {
		ProjectileStandard bscript = bulletCache.SpawnObject (this.transform.GetChild(0).transform.position).GetComponent<ProjectileStandard>();	
		bscript.SetupProjectile(false, projectileDamage/2, projectileSpeed, whitepolarity, false, 0);
		
		ProjectileStandard bscript1 = bulletCache.SpawnObject (this.transform.GetChild(0).transform.position).GetComponent<ProjectileStandard>();	
		bscript1.SetupProjectile(false, projectileDamage/2, projectileSpeed, whitepolarity, false, 15);
		
		ProjectileStandard bscript2 = bulletCache.SpawnObject (this.transform.GetChild(0).transform.position).GetComponent<ProjectileStandard>();	
		bscript2.SetupProjectile(false, projectileDamage/2, projectileSpeed, whitepolarity, false, -15);
		this.audio.Play ();
	}
	
	public void TakeDamage(float amount)
	{
		//If we still have a shield
		if (SP>0) {
			SP = SP - amount;
			//shieldBar.transform.localScale = shieldBar.transform.localScale - new Vector3((amount/10), 0, 0);
			barSize = shieldBar.GetComponent<OTSprite>().size;
			shieldBar.GetComponent<OTSprite>().size = new Vector2((barSize.x - amount/10), barSize.y);
			//Check if we just broke the shield
			if (SP <= 0)
			{
				SP = 0;
				shieldBar.renderer.enabled = false;
				//Break shield animation
			}
		} //We dont have a shield no-more.
		else if (SP <= 0) {
			HP = HP - amount;
			//healthBar.transform.localScale = healthBar.transform.localScale - new Vector3((amount/10), 0, 0);
			barSize = healthBar.GetComponent<OTSprite>().size;
			healthBar.GetComponent<OTSprite>().size = new Vector2((barSize.x - amount/10), barSize.y);
			if (HP <=0) { //we just died
				Explode ();
				
				if (Lives>0)
				{
					Respawn ();
				}
				else {
					manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
					manager.GameEnd(true);
					Debug.Log("Your Dead! game over");
				}
			}
		}
		
	}
	
	void Respawn() {
		Destroy(GameObject.FindGameObjectWithTag ("LivesContainer").transform.GetChild (Lives - 1).gameObject);
		Lives--;
		HP = maxHP;
		SP = maxSP;
		scatterShot = false;
		if (this.transform.parent.GetComponent<HandleInput>().shootDelay < 0.2)
		{
			this.transform.parent.GetComponent<HandleInput>().shootDelay = 0.23f;
		}
		multiplier = 0;
		shieldBar.GetComponent<OTSprite>().size = new Vector2(10f,0.6666667f);
		shieldBar.renderer.enabled = true;
		healthBar.GetComponent<OTSprite>().size = new Vector2(10f,0.6666667f);
		healthBar.renderer.enabled = true;
		
	}
	public void OnGUI() {
		GUI.Label (new Rect(100, 30, 400,50), "Multiplier: " + multiplier);
		GUI.Label (new Rect(100, 50, 400,50), "Weapon Energy: " + weaponEnergy);
	}
	
	public void CheckPolarity() {
		if (whitepolarity == true)
		{
			//this.GetComponent<OTAnimatingSprite>().animationFrameset = "whiteship";
			this.GetComponent<OTAnimatingSprite>().PlayLoop ("whiteship");
		}
		else {
			//this.GetComponent<OTAnimatingSprite>().animationFrameset = "blackship";
			this.GetComponent<OTAnimatingSprite>().PlayLoop ("blackship");
		}
	}

}

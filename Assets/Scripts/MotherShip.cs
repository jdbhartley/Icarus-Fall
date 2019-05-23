using UnityEngine;
using System.Collections;

public class MotherShip : MonoBehaviour {
	public float delay;
	public ObjectCache bulletCache;
	public bool InUse;
	
	public void Attack () {
		StartCoroutine (MoveIn (0.05f));
	}
	
	IEnumerator MoveIn(float speed) {
		while(this.transform.position.x < -9)
		{
			this.GetComponent<OTSprite>().position += (new Vector2(10f, 0) * Time.deltaTime);
			yield return new WaitForSeconds(speed);
		}
		StartCoroutine (ShootAllGuns(0.2f, 4));
		StopCoroutine ("MoveIn");
	}
	
	IEnumerator MoveOut(float speed) {
		while(this.transform.position.x > -24)
		{
			this.GetComponent<OTSprite>().position -= (new Vector2(10f, 0) * Time.deltaTime);
			yield return new WaitForSeconds(speed);
		}
		StopCoroutine ("MoveOut");
	}
	
	IEnumerator ShootAllGuns(float delay, float secondsToShoot) {
		
		float endTime = Time.timeSinceLevelLoad + secondsToShoot;
		for(int i = 0; i < this.transform.GetChildCount(); i++)
		{
		   if(this.transform.GetChild(i).tag == "muzzleflash")
			{
				this.transform.GetChild(i).renderer.enabled = true;
			}
		}
		int gun1Rot = 0;
		int gun2Rot = -65;
		int gun3Rot = 40;
		int gun4Rot = 60;
		
		while(Time.timeSinceLevelLoad < endTime) {
			
		for(int i = 0; i < this.transform.GetChildCount(); i++)
		{
		   if(this.transform.GetChild(i).tag == "Gun1")
			{
				ProjectileStandard bscript = bulletCache.SpawnObject (this.transform.GetChild(i).transform.position).GetComponent<ProjectileStandard>();
				Debug.Log (this.transform.GetChild (i).name);
				bscript.SetupProjectile(false, 4, 20, true, false, gun1Rot);
				this.audio.Play ();
			}
				
			if(this.transform.GetChild(i).tag == "Gun2")
			{
				ProjectileStandard bscript = bulletCache.SpawnObject (this.transform.GetChild(i).transform.position).GetComponent<ProjectileStandard>();
				Debug.Log (this.transform.GetChild (i).name);
				bscript.SetupProjectile(false, 4, 20, true, false, gun2Rot);
				this.audio.Play ();
			}
		}
			gun1Rot -= 4;
			gun2Rot += 4;
			yield return new WaitForSeconds(delay);
		}
		for(int i = 0; i < this.transform.GetChildCount(); i++)
		{
		   if(this.transform.GetChild(i).tag == "muzzleflash")
			{
				this.transform.GetChild(i).renderer.enabled = false;
			}
		}
		StartCoroutine (MoveOut (0.05f));
		StopCoroutine ("ShootAllGuns");
	}
	
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Enemy")
		{
			col.GetComponent<EnemyStandard>().DoDamage (150, true);
		}
	}
	
}

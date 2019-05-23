using UnityEngine;
using System.Collections;

public class SunE2AI : EnemyStandard {
	
	// Use this for initialization
	protected override void Start () {
		StartCoroutine (DoTween());
		manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Manager>();
		Debug.Log (manager);
		HP = MaxHP;
		
	}
	
	protected override void Move() {
		base.Move ();
	}
	
	IEnumerator DoTween()
	{
		while (1==1)
		{
			if ((int)GetComponent<OTSprite>().rotation == 300) {
				new OTTween(GetComponent<OTSprite>(), 1.2f, OTEasing.SineIn).Tween ("rotation", -60, 60.1f);
			}
			if ((int)GetComponent<OTSprite>().rotation == 60) {
				new OTTween(GetComponent<OTSprite>(), 1.2f, OTEasing.SineIn).Tween ("rotation", 60.1f, -60);
			}
			yield return new WaitForSeconds(1.2f);
		}
	}
	
	protected override void StartShooting() {
	}
}

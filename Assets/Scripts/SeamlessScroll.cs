using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeamlessScroll : MonoBehaviour {
	public bool continuous;
	public float scrollSpeed;
	Vector3 translate;
	public bool animate = false;
	public Texture frame3;
	public Texture frame2;
	Texture frame1;
	public float frameDelay;
	
	public bool NewAnimation;
	
	public List<Texture> frames;
	
	// Use this for initialization
	void Start () {
		translate = new Vector3(-scrollSpeed * Time.deltaTime , 0, 0);
		
		//Animation
		frame1 = this.gameObject.GetComponent<OTSprite>().texture;
		if (animate == true) {
			StartCoroutine(AnimateEverySeconds (0.3f));
		}
		if (NewAnimation == true) {
			Debug.Log ("Using New Animation");
			StartCoroutine(NewAnimateEverySeconds (frameDelay));
		}
	}
	
	// Update is called once per frame
	void Update () {
		Scroll ();
	}
	
	void Scroll () {
		this.transform.Translate (translate);
	}
	
	//animation
	IEnumerator AnimateEverySeconds(float delay)
	{
		while(1==1) {
			if (this.gameObject.GetComponent<OTSprite>()._image == frame1) {
				this.gameObject.GetComponent<OTSprite>()._image = frame2;
				this.gameObject.GetComponent<OTSprite>().size = new Vector2(42.66667f, 21.33333f);
			}
			else if (this.gameObject.GetComponent<OTSprite>()._image == frame2){
				this.gameObject.GetComponent<OTSprite>()._image = frame3;
				this.gameObject.GetComponent<OTSprite>().size = new Vector2(42.66667f, 21.33333f);
			}
			else if (this.gameObject.GetComponent<OTSprite>()._image == frame3){
				this.gameObject.GetComponent<OTSprite>()._image = frame1;
				this.gameObject.GetComponent<OTSprite>().size = new Vector2(42.66667f, 21.33333f);
			}
			yield return new WaitForSeconds(delay);
		}
	}
	
	//new animation
	IEnumerator NewAnimateEverySeconds(float delay)
	{
		int i = 0;
		while(1==1) {
			if (i < frames.Count - 1) {
				i++;
			}
			else {
				i=0;
			}
			
			this.gameObject.GetComponent<OTSprite>()._image = frames[i];
			this.gameObject.GetComponent<OTSprite>().size = new Vector2(21.33333f, 21.33333f);
			yield return new WaitForSeconds(delay);
		}
	}
	
	
	void OnBecameInvisible () {
		if (continuous == true)
			this.transform.position = new Vector3(42.66667f, transform.position.y, transform.position.z);
		else 
			Destroy (this.gameObject);
	}
}

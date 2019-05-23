using UnityEngine;
using System.Collections;

public class HandleInput : MonoBehaviour {
	
	public OTObject button_polarity;
	public OTObject button_specialfire;
	
	OTSprite polaritySprite;
	OTSprite specialfireSprite;
	
	public float speed;
	public float shootDelay;
	RuntimePlatform platform;
	float nextFire;
	public bool enablepolarity;
	Manager manager;
	
	public GameObject muzzleflash;
	
	PlayerShip shipScript;
	
	void Awake () {
		//Get the platform
		platform = Application.platform;
		
		//If were on flashplayer get rid of the buttons
		if (platform == RuntimePlatform.WindowsWebPlayer || platform == RuntimePlatform.OSXWebPlayer) {
			button_polarity.visible = false;
			button_specialfire.visible = false;
		}
	}
	
	// Use this for initialization
	void Start () {
		shipScript = this.transform.GetChild (0).GetComponent<PlayerShip>();
		polaritySprite = button_polarity.GetComponent<OTSprite>();
		specialfireSprite = button_specialfire.GetComponent<OTSprite>();
		
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (platform == RuntimePlatform.Android)
			HandleTouches ();
		else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.WindowsWebPlayer || platform == RuntimePlatform.OSXWebPlayer)
			HandleKeys ();
		
		
		//Exit if the esc key is hit, or the back key on android.
		if (Input.GetKey (KeyCode.Escape))
		{
			Application.Quit ();
		}
	}
	
	IEnumerator ButtonAnimation(OTSprite button)
	{
		button.frameIndex = 0;
		yield return new WaitForSeconds(2.0f);
		button.frameIndex = 2;
	}
	
	void HandleKeys () {
		//Input for PC
		
		//WASD
		if (Input.GetKey (KeyCode.UpArrow) & this.transform.position.y < manager.gameBounds.y) {
			this.transform.Translate (Vector3.up * 20 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow) & this.transform.position.x > manager.gameBounds.x) {
			this.transform.Translate (Vector3.left * 20 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.DownArrow) & this.transform.position.y > manager.gameBounds.height) {
			this.transform.Translate (Vector3.down * 20 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.RightArrow) & this.transform.position.x < manager.gameBounds.width) {
			this.transform.Translate (Vector3.right * 20 * Time.deltaTime);
		}
		
		//Shoot
		if (Input.GetKey (KeyCode.Space)) {
			if (Time.time > nextFire) {
				muzzleflash.renderer.enabled = true;
				nextFire = Time.time + shootDelay;
				if (shipScript.scatterShot == false) {
					shipScript.Shoot ();
				}
				else if (shipScript.scatterShot == true) {
					shipScript.ScatterShot ();
				}
			}
		}
		
		if (Input.GetKeyUp (KeyCode.Space)) {
			muzzleflash.renderer.enabled = false;
		}
		
		if(Input.GetKeyDown (KeyCode.LeftControl)) {
			shipScript.FireSpecial();
		}
		
		//Switch Polarities
		if (enablepolarity == true) {
			if (Input.GetKeyDown (KeyCode.LeftControl)) {
				switch (shipScript.whitepolarity)
					{
					case true:
						shipScript.whitepolarity = false;
						break;
						
					case false:
						shipScript.whitepolarity = true;
						break;
					}
			}
		}
		
		//Special fire
	}

	void HandleTouches () {
		
		//Movement for Android
		if (Input.touchCount > 0)
		{
			//Set position to finger position
			this.transform.position = Vector3.Lerp (this.transform.position, Camera.mainCamera.ScreenToWorldPoint (new Vector3(Input.GetTouch (0).position.x + 70, Input.GetTouch (0).position.y, this.transform.position.z)), Time.deltaTime * speed);
			
			//Constantly shoot while moving
			if (Time.time > nextFire) {
				nextFire = Time.time + shootDelay;
				shipScript.Shoot ();
			}
		}
		
		//Switch Polarities
		if (OT.Clicked (button_polarity)) {
				ButtonAnimation (polaritySprite);
				switch (shipScript.whitepolarity)
				{
				case true:
					shipScript.whitepolarity = false;
					break;
					
				case false:
					shipScript.whitepolarity = true;
					break;
				}
		}
		
		//Special Fire
		if (OT.Over (button_specialfire)) {
		}
	}
	
	void OnGUI() {
		if (Input.touchCount > 0)
		{
		GUILayout.Label ("Taps: " + Input.GetTouch (0).tapCount);
		}
	}
}

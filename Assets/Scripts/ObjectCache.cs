using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCache : MonoBehaviour {
	public GameObject objectToCache;
	public int maxObjects;
	List<GameObject> cache;
	
//	public ObjectCache(GameObject objectToCache)
//	{
//		
//		for (int i=0;i<= objectToCache.GetComponent<Cacheable>().maxNumberOnScreen;i++)
//		{
//			cache[i] = (GameObject)Instantiate(objectToCache);
//			cache[i].transform.position = new Vector3(0, 30f, objectToCache.transform.position.z);
//			
//		}
//	}
	
	public GameObject SpawnObject(Vector3 position)
	{
		foreach (GameObject g in cache)
		{
			if (g != null)
			{
				if (g.GetComponent<Cacheable>().inUse == false)
				{
					g.transform.position = position;
					Cacheable script = (Cacheable)g.GetComponent (typeof(Cacheable));
					script.enabled = true;
					script.inUse = true;
					return g;
					break;
				}
			}
		}
		Debug.Log ("Returned Null!?");
		return null;
	}
	
	public void StopObject(GameObject objectToStop)
	{
		Cacheable script = objectToStop.GetComponent<Cacheable>();
		if (script.inUse == true)
		{
			script.enabled = false;
			script.inUse = false;
			objectToStop.transform.position = this.transform.position;
		}
	}
	
	void Awake () {
		cache = new List<GameObject>();
		for (int i = 0; i <= maxObjects; i++)
		{
			cache.Add ((GameObject)Instantiate (objectToCache));
			cache[i].transform.parent = this.transform;
			cache[i].transform.position = this.transform.position;
			cache[i].GetComponent<Cacheable>().inUse = false;
			cache[i].GetComponent<Cacheable>().enabled = false;
		}
		
		
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

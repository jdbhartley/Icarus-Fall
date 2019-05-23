using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public List<SpawnGroup> spawnGroups;
	
	void Update () {
		SpawnCheck();
	}
	
	void SpawnCheck () {	
		foreach (SpawnGroup sG in spawnGroups)
		{
			if ((int)sG.timeToSpawn == (int)Time.timeSinceLevelLoad & sG.spawned == false) {
				SpawnStuff(sG.xPos, sG.yPos, sG.cache);
				sG.spawned = true;
			}
		}
	}
	
	void SpawnStuff(float xPos, float yPos, ObjectCache cache)
	{
		cache.SpawnObject (new Vector3(xPos,yPos,0.99f));
	}
	
	void OnGUI (){
		GUI.Label (new Rect(0,50,300,50), Time.timeSinceLevelLoad.ToString());
	}
	
	public void SetSpawnGroup(float x, float y, ObjectCache objCache, int timeToSpawn) 
	{
		SpawnGroup sGroup = new SpawnGroup();
		sGroup.cache = objCache;
		sGroup.timeToSpawn = timeToSpawn;
		sGroup.xPos = x;
		sGroup.yPos = y;
		
		spawnGroups.Add(sGroup);
	}
	
	[System.Serializable]
	public class SpawnGroup
	{
		public float timeToSpawn;
		public ObjectCache cache;
		public float xPos;
		public float yPos;
		public bool spawned = false;
	}

}
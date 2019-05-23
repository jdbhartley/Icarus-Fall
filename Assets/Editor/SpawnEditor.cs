using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Xml.Serialization;

public class SpawnEditor : EditorWindow {
	string[] space1EnemyTypes = new string[] {"space1", "space2", "asteroid1", "asteroid2"};
	string[] sunEnemyTypes = new string[] {"sunE1", "Ghost Head", "sunE3"};
	string[] levelSelect = new string[] {"Space1", "Sun"};
	int enemyIndex;
	int levelIndex;
	int timeToSpawn;
	List<TempGroup> currEnemies = new List<TempGroup>();
	EnemySpawner spawner;
	TempGroup tempGroup = new TempGroup();
	
    // Add menu named "My Window" to the Window menu
    [MenuItem ("IcarusFall/SpawnEditor")]
    static void Init () {
        // Get existing open window or if none, make a new one:
        SpawnEditor window = (SpawnEditor)EditorWindow.GetWindow (typeof (SpawnEditor));
	 	
    }
	
	void Start () {
	}
    
    void OnGUI () {
		Debug.DrawLine (GameObject.Find ("ScreenBounds").transform.position, GameObject.Find ("ScreenBounds").transform.GetChild(0).transform.position);
		levelIndex = EditorGUILayout.Popup (levelIndex, levelSelect);
		
		//Level select popup
		//If level is Space1
		if (levelIndex == 0) {
			enemyIndex = EditorGUILayout.Popup (enemyIndex, space1EnemyTypes);
		}
		if (levelIndex == 1) {
			enemyIndex = EditorGUILayout.Popup (enemyIndex, sunEnemyTypes);
		}
		
		GUILayout.Label ("Time to spawn group");
		timeToSpawn = (int)GUILayout.HorizontalSlider(timeToSpawn, 0, 200);
		
		if (GUILayout.Button ("Place Object"))
		{				
			if(levelIndex == 0) //space1
			{
				switch(enemyIndex) {
					case 0:
						//currEnemies.Add (PrefabUtility.InstantiatePrefab (Resources.Load ("Enemy1")));
						tempGroup.obj = (GameObject)Instantiate (Resources.Load ("Space1/spEnemy1"));
						tempGroup.cache = GameObject.FindWithTag ("cache1").GetComponent<ObjectCache>();
						tempGroup.obj.GetComponent<Cacheable>().cache = tempGroup.cache;
						currEnemies.Add( tempGroup );
						break;
					case 1:
						tempGroup.obj = (GameObject)Instantiate (Resources.Load ("Space1/spEnemy2"));
						tempGroup.cache = GameObject.FindWithTag ("cache2").GetComponent<ObjectCache>();
						tempGroup.obj.GetComponent<Cacheable>().cache = tempGroup.cache;
						currEnemies.Add( tempGroup );
						break;
					case 2:
						tempGroup.obj = (GameObject)Instantiate (Resources.Load ("Space1/asteroid1"));
						tempGroup.cache = GameObject.FindWithTag ("cache3").GetComponent<ObjectCache>();
						tempGroup.obj.GetComponent<Cacheable>().cache = tempGroup.cache;
						currEnemies.Add( tempGroup );
						break;
					case 3:
						tempGroup.obj = (GameObject)Instantiate (Resources.Load ("Space1/asteroid2"));
						tempGroup.cache = GameObject.FindWithTag ("cache4").GetComponent<ObjectCache>();
						tempGroup.obj.GetComponent<Cacheable>().cache = tempGroup.cache;
						currEnemies.Add( tempGroup );
						break;
					}
				}
			if(levelIndex == 1) //sun
			{
				switch(enemyIndex) {
				case 0:
						//currEnemies.Add (PrefabUtility.InstantiatePrefab (Resources.Load ("Enemy1")));
						tempGroup.obj = (GameObject)Instantiate (Resources.Load ("Sun/sunE1"));
						tempGroup.cache = GameObject.FindWithTag ("cache1").GetComponent<ObjectCache>();
						tempGroup.obj.GetComponent<Cacheable>().cache = tempGroup.cache;
						currEnemies.Add( tempGroup );
						break;
					case 1:
						tempGroup.obj = (GameObject)Instantiate (Resources.Load ("Sun/sunE2"));
						tempGroup.cache = GameObject.FindWithTag ("cache2").GetComponent<ObjectCache>();
						tempGroup.obj.GetComponent<Cacheable>().cache = tempGroup.cache;
						currEnemies.Add( tempGroup );
						break;
					case 2:
						tempGroup.obj = (GameObject)Instantiate (Resources.Load ("Sun/sunE3"));
						tempGroup.cache = GameObject.FindWithTag ("cache3").GetComponent<ObjectCache>();
						tempGroup.obj.GetComponent<Cacheable>().cache = tempGroup.cache;
						currEnemies.Add( tempGroup );
						break;
				}
			}
		}
		
		if (GUILayout.Button ("Save Group")) {
			spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
			foreach (GameObject i in GameObject.FindGameObjectsWithTag ("Enemy")) {
				Debug.Log (i + " " + i.GetComponent<Cacheable>().cache);
				spawner.SetSpawnGroup (i.transform.position.x, i.transform.position.y, i.GetComponent<Cacheable>().cache, timeToSpawn);
				DestroyImmediate (i);
			}
			foreach (GameObject i in GameObject.FindGameObjectsWithTag ("Obstacle")) {
				Debug.Log (i + " " + i.GetComponent<Cacheable>().cache);
				spawner.SetSpawnGroup (i.transform.position.x, i.transform.position.y, i.GetComponent<Cacheable>().cache, timeToSpawn);
				DestroyImmediate (i);
			}
			
		}
		
		if (GUILayout.Button ("Delete Group")) {
			foreach (TempGroup i in currEnemies) {
				DestroyImmediate (i.obj);
			}
		}
		
		if (GUILayout.Button ("Repaint")) {
			Repaint ();
		}
		
		GUILayout.Label ("LevelIndex = " + levelIndex.ToString () + " typeIndex = " + enemyIndex.ToString ());
		GUILayout.Label ("TimeToSpawnGroup = " + timeToSpawn.ToString ());
		if (currEnemies != null)
		{
		GUILayout.Label ("Enemies In Progress" + currEnemies.Count);
		}
		GUILayout.Label ("Enemies on Screen " + GameObject.FindGameObjectsWithTag ("Enemy").Length);
    }
	
	public class TempGroup {
		public GameObject obj;
		public ObjectCache cache;
	}
}
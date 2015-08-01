using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public class ClickData{
//	public ArrayList
//
//}

public class MainMenu : MonoBehaviour {
	
	public List<Object> sceneToLoad;
	public List<SceneStats> SceneStatList = new List<SceneStats>();
	public Vector3 targetPos;
	public Camera camera;
	
	private int index = 0;
	private bool clickInTheButton = false;
	private bool finished = false;
	private ExperimentStats keyValueWriter = new ExperimentStats();

	
	public class SceneStats{
		public string sceneName;
		public int xClick; 
		public int yClick;
		public int xEnemy;
		public int yEnemy;
		public float sceneDuration;
	}
	
	void Start() {
	}

	public void buttonClick () {
		Object.DontDestroyOnLoad (gameObject);
		Application.LoadLevel (sceneToLoad[0].name);
		clickInTheButton = true;
	}

	public void createSceneStats(){
		SceneStats currentSceneStats = new SceneStats();
		currentSceneStats.sceneDuration = Time.timeSinceLevelLoad ;
		targetPos = GameObject.FindGameObjectWithTag("RandomEnemy").transform.position;

		currentSceneStats.sceneName = sceneToLoad[index].name;
		currentSceneStats.xClick = (int) Input.mousePosition.x;
		currentSceneStats.yClick = (int) Input.mousePosition.y;
		currentSceneStats.xEnemy = (int) camera.WorldToScreenPoint(targetPos).x;
		currentSceneStats.yEnemy = (int) camera.WorldToScreenPoint(targetPos).y;
		
		Debug.Log (sceneToLoad [index].name + ", Clock "+ currentSceneStats.sceneDuration);
		SceneStatList.Add(currentSceneStats);
	}

	void Update(){
		camera = Camera.main;
		if (Input.GetMouseButtonDown(0) && clickInTheButton && !finished){
			if (index < sceneToLoad.Capacity - 1){

				createSceneStats();

				++ index;
				Application.LoadLevel (sceneToLoad[index].name);
			}
			else{

				createSceneStats();
				
				OutputSceneStats (SceneStatList);

				Destroy(GameObject.FindGameObjectWithTag("SceneRoot"));
				gameObject.GetComponentInChildren<Canvas>().enabled = true;
				finished = true;
				Debug.Log ("Finished");
			}
		}
	}

	void OutputSceneStats(List<SceneStats> statsList){
		for (int i = 0; i<sceneToLoad.Capacity; ++i){
			keyValueWriter.Set (statsList[i].sceneName + "-xclick", statsList[i].xClick.ToString());
			keyValueWriter.Set (statsList[i].sceneName + "-xEnemy", statsList[i].xEnemy.ToString());
			keyValueWriter.Set (statsList[i].sceneName + "-yclick", statsList[i].yClick.ToString());
			keyValueWriter.Set (statsList[i].sceneName + "-yEnemy", statsList[i].yEnemy.ToString());
			keyValueWriter.Set (statsList[i].sceneName + "-clock ", statsList[i].sceneDuration.ToString());
		}
		keyValueWriter.WriteToFile("ExperimentStats-stats.csv");

	}
}

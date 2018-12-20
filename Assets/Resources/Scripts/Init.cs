using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Init : MonoBehaviour {

	private GameObject _backgroundObject;
	private string _mapConfigurationPath;
	private string _jsonString;


	// Use this for initialization
	void Start () {
		_mapConfigurationPath = Application.streamingAssetsPath + "Configurations/mapConfiguration.json";
		_jsonString = File.ReadAllText(_mapConfigurationPath);
		ListOfMaps maps = JsonUtility.FromJson<ListOfMaps>(_jsonString);
		Map initMap = maps.maps[0];

		_backgroundObject = GameObject.Find("Background");
		_backgroundObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Backgrounds/" + initMap.background);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

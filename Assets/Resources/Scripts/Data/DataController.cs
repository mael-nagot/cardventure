using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{

	public static DataController instance;
    public GameData gameData;
	private bool isReady = false;
    private string gameDataFileName = "data.json";
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        LoadGameData();
    }

    private void LoadGameData()
    {
        /* 
        Cannot just read the files in the streaming path on Android since it is stored in an apk.
        Need a www reader for that specific case.
        */
        string filePath;
        string dataAsJson = null;
        Debug.Log(Application.persistentDataPath);
        filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);
        if (File.Exists(filePath))
        {
            dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
            SaveGameData();
        }
		isReady = true;
    }

    public void SaveGameData()
    {
        string filePath;
        string dataAsJson = JsonUtility.ToJson(gameData);

        filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);

        File.WriteAllText(filePath, dataAsJson);
    }

    // Update is called once per frame
    public bool GetIsReady()
    {
        return isReady;
    }
}

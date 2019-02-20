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
    public string filePath;

    /*
    Use Singleton pattern for the data controller object to have it never destroyed and only once instance of it.
    It needs to be present in all the scenes.
     */
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
        filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);
        LoadGameData();
    }

    /// <summary>
    /// Load the game data from the save file. If no save file exists, then it creates one.
    /// </summary>
    public void LoadGameData()
    {
        /* 
        Cannot just read the files in the streaming path on Android since it is stored in an apk.
        Need a www reader for that specific case.
        */
        string dataAsJson = null;
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

    /// <summary>
    /// Save the game data.
    /// </summary>
    public void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);

        File.WriteAllText(filePath, dataAsJson);
    }

    /// <summary>
    /// Return true when the save file is fully loaded.
    /// This function is done to avoid trying to access data when they are not fully loaded.
    /// </summary>
    public bool GetIsReady()
    {
        return isReady;
    }

    /// <summary>
    /// Return true when a game is ongoing so that the main menu can allow to continue the game.
    /// </summary>
    public bool isGameSessionStarted()
    {
        return gameData.isGameSessionStarted;
    }

    public int getLevel()
    {
        return gameData.level;
    }
}

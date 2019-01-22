﻿using System.Collections;
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

    /*
    Load the game data from the save file. If no save file exists, then it creates one.
     */
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

    public void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);

        File.WriteAllText(filePath, dataAsJson);
    }

    // Update is called once per frame
    public bool GetIsReady()
    {
        return isReady;
    }
}

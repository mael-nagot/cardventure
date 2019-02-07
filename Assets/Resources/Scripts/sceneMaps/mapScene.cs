using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapScene : MonoBehaviour
{
    public Map map;
    // Start is called before the first frame update
    void Start()
    {
        string mapName = selectMap(DataController.instance.getLevel());
        map = loadMap(mapName);
        loadMapSound(map);
        loadMapBackground(map);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string selectMap(int level)
    {
        return "Forest";
    }

    private Map loadMap(string mapName)
    {
        Map map = Resources.Load("Data/Maps/"+mapName) as Map;
        return map;
    }

    private void loadMapSound(Map map)
    {
        StartCoroutine(SoundController.instance.playBackgroundMusic(map.bgmFile,1,1));
        StartCoroutine(SoundController.instance.playBackgroundSound(map.bgsFile,1,1));
    }

    private void loadMapBackground(Map map)
    {
        GameObject.Instantiate(map.mapBackground);
    }
    
}

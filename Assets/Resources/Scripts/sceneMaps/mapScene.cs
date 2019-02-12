using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mapScene : MonoBehaviour
{
    public Map map;
    public ListOfMaps listOfMaps;
    // Start is called before the first frame update
    void Start()
    {
        string mapName = selectMap(DataController.instance.getLevel());
        map = listOfMaps.getMap(mapName);
        loadMapSound(map);
        loadMapBackground(map);
        StartCoroutine(generateMap());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private string selectMap(int level)
    {
        return "Forest";
    }

    private void loadMapSound(Map map)
    {
        StartCoroutine(SoundController.instance.playBackgroundMusic(map.bgmFile, 1, 1));
        StartCoroutine(SoundController.instance.playBackgroundSound(map.bgsFile, 1, 1));
    }

    private void loadMapBackground(Map map)
    {
        GameObject.Instantiate(map.mapBackground);
    }

    // To be rewriten, for now it is just to see how it looks
    private IEnumerator generateMap()
    {
        yield return null;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!((i==0 && j==0) || (i==0 && j==2) || (i==6 && j==0) || (i==6 && j==2)))
                {
                    float random = UnityEngine.Random.Range(0.0f, 10.0f);
                    string item = "";
                    if (random < 1)
                    {
                        item = "forestChest";
                    }
                    else if (random < 2)
                    {
                        item = "forestRest";
                    }
                    else if (random < 6)
                    {
                        item = "forestQuest";
                    }
                    else
                    {
                        item = "forestSword";
                    }
                    GameObject mapItem = GameObject.Instantiate(Resources.Load("Prefabs/Maps/MapItems/" + item) as GameObject);
                    mapItem.transform.position = new Vector3((-6.80f + i * 2.27f), (2.15f - j * 2.19f), 4);
                    yield return null;
                }
            }
        }
    }

}

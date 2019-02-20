using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class MapScene : MonoBehaviour
{
    public Map map;
    public ListOfMaps listOfMaps;
    IEnumerator Start()
    {
        string mapName = selectMap(DataController.instance.getLevel());
        map = listOfMaps.getMap(mapName);
        loadMapSound(map);
        loadMapBackground(map);
        yield return StartCoroutine(generateMap());
        shakeObject(1, 2);
    }

    void Update()
    {

    }

    // To be rewritten.
    private string selectMap(int level)
    {
        return "Forest";
    }

    /// <summary>
    /// This method plays the background music and background sound corresponding to the map.
    /// </summary>
    /// <param name="Map map">The map from which the bgm and bgs have to be loaded.</param>
    private void loadMapSound(Map map)
    {
        StartCoroutine(SoundController.instance.playBackgroundMusic(map.bgmFile, 1, 1));
        StartCoroutine(SoundController.instance.playBackgroundSound(map.bgsFile, 1, 1));
    }

    /// <summary>
    /// This method displays the map background from a prefab
    /// </summary>
    /// <param name="Map map">The map from which the background needs to be loaded</param>
    private void loadMapBackground(Map map)
    {
        GameObject.Instantiate(map.mapBackground);
    }

    /// <summary>
    /// This method makes a map item game object shaking so that the player understands he can interact with it.
    /// </summary>
    /// <param name="int x">x of the map item on the map (1-7)</param>
    /// <param name="int y">y of the map item on the map (1-3)</param>
    private void shakeObject(int x, int y)
    {
        GameObject objectToAnimate = getMapItemFromCoordinates(x, y);
        StartCoroutine(objectToAnimate.GetComponent<MapItemManagement>().shakeObject());
    }

    /// <summary>
    /// This method make a shaking map item game object stopping to shake
    /// </summary>
    /// <param name="int x">x of the map item on the map (1-7)</param>
    /// <param name="int y">y of the map item on the map (1-3)</param>
    private void stopShakingObject(int x, int y)
    {
        GameObject objectToAnimate = getMapItemFromCoordinates(x, y);
        objectToAnimate.GetComponent<MapItemManagement>().stopShakingObject();
    }

    /// <summary>
    /// This method gets the map item game object from its position.
    /// </summary>
    /// <param name="int x">x of the map item on the map (1-7)</param>
    /// <param name="int y">y of the map item on the map (1-3)</param>
    private GameObject getMapItemFromCoordinates(int x, int y)
    {
        return GameObject.Find("mapItem" + x + "-" + y);
    }

    // To be rewriten, for now it is just to see how it looks
    private IEnumerator generateMap()
    {
        yield return null;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!((i == 0 && j == 0) || (i == 0 && j == 2) || (i == 6 && j == 0) || (i == 6 && j == 2)))
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
                    mapItem.transform.SetParent(GameObject.Find("Canvas").transform);
                    mapItem.name = "mapItem" + (i + 1) + "-" + (j + 1);
                    RectTransform rectTransform = mapItem.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector3((-1000f + i * 330f), (300 - j * 300f), 4);
                    rectTransform.pivot = new Vector2(0.5f, 1);
                    mapItem.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    yield return null;
                }
            }
        }
    }

}

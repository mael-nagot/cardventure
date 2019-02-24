using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class MapScene : MonoBehaviour
{
    public Map map;
    public ListOfMaps listOfMaps;
    private int[,] mapTilesId;
    [SerializeField]
    private GameObject linePrefab;

    IEnumerator Start()
    {
        string mapName = selectMap(DataController.instance.getLevel());
        map = listOfMaps.getMap(mapName);
        loadMapSound(map);
        loadMapBackground(map);
        generateMap();
        yield return StartCoroutine(displayMap());
        for (int i = 0; i < map.sizeY; i++)
        {
            drawLineBetweenObjects(1, 1, 2, i + 1);
        }
        shakeObject(1, 1);
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


    private void generateMap()
    {
        Tile[] listOfTiles = map.possibleTiles;
        int defaultTile = getDefaultTileIndex(listOfTiles);
        mapTilesId = new int[map.sizeX, map.sizeY];
        initializeMapWithDefaultTile(defaultTile);
        AddOtherTilesToMap(listOfTiles, defaultTile);
        debugMapTilesId();
    }

    private void initializeMapWithDefaultTile(int defaultTile)
    {
        for (int i = 0; i < mapTilesId.GetLength(0); i++)
        {
            for (int j = 0; j < mapTilesId.GetLength(1); j++)
            {
                if ((i == 0 || i == mapTilesId.GetLength(0) - 1) && j > 0)
                {
                    mapTilesId[i, j] = -1;
                }
                else
                {
                    mapTilesId[i, j] = defaultTile;
                }
            }
        }
    }

    private void AddOtherTilesToMap(Tile[] listofTiles, int defaultTile)
    {
        for (int i = 0; i < listofTiles.Length; i++)
        {
            if (!listofTiles[i].isDefault)
            {
                int nbOfThisTileInTheMap;
                int minNbOfTiles = listofTiles[i].minNumber;
                if (minNbOfTiles == -1) { minNbOfTiles = 0; }
                int maxNbOfTiles = listofTiles[i].maxNumber;
                if (maxNbOfTiles == -1) { maxNbOfTiles = (mapTilesId.GetLength(0) * mapTilesId.GetLength(1) - 2); }
                nbOfThisTileInTheMap = UnityEngine.Random.Range(minNbOfTiles, maxNbOfTiles + 1);
                for (int j = 0; j < nbOfThisTileInTheMap; j++)
                {
                    if (verifyIfTileAvailable(listofTiles[i].minX - 1, listofTiles[i].maxX - 1, defaultTile))
                    {
                        int randomX;
                        int randomY;
                        while (true)
                        {
                            randomX = UnityEngine.Random.Range(listofTiles[i].minX - 1, listofTiles[i].maxX);
                            randomY = UnityEngine.Random.Range(0, mapTilesId.GetLength(1));
                            if (mapTilesId[randomX, randomY] == defaultTile)
                            {
                                mapTilesId[randomX, randomY] = i;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private bool verifyIfTileAvailable(int min, int max, int defaultTile)
    {
        for (int i = min; i < max + 1; i++)
        {
            for (int j = 0; j < mapTilesId.GetLength(1); j++)
            {
                if (mapTilesId[i, j] == defaultTile)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // For debugging purpose, to be removed
    void debugMapTilesId()
    {
        string toDebug = "";
        for (int i = 0; i < mapTilesId.GetLength(0); i++)
        {
            toDebug += "[";
            for (int j = 0; j < mapTilesId.GetLength(1); j++)
            {
                if (j == mapTilesId.GetLength(1) - 1)
                {
                    toDebug += mapTilesId[i, j];
                }
                else
                {
                    toDebug += mapTilesId[i, j] + ",";
                }
            }
            toDebug += "]";
        }
        Debug.Log(toDebug);
    }

    private int getDefaultTileIndex(Tile[] listofTiles)
    {
        for (int i = 0; i < listofTiles.Length; i++)
        {
            if (listofTiles[i].isDefault)
            {
                return i;
            }
        }
        return 0;
    }

    private IEnumerator displayMap()
    {
        yield return null;
        for (int i = 0; i < map.sizeX; i++)
        {
            for (int j = 0; j < map.sizeY; j++)
            {
                if (!((i == 0 && j > 0) || (i == (map.sizeX - 1) && j > 0)))
                {
                    GameObject mapItem = GameObject.Instantiate(map.possibleTiles[mapTilesId[i, j]].tilePrefab as GameObject);
                    mapItem.transform.SetParent(GameObject.Find("Canvas").transform);
                    mapItem.name = "mapItem" + (i + 1) + "-" + (j + 1);
                    RectTransform rectTransform = mapItem.GetComponent<RectTransform>();
                    if (i == 0 || i == (map.sizeX - 1) || map.sizeY == 1)
                    {
                        rectTransform.anchoredPosition = new Vector3((-1000f + i * 2000 / (map.sizeX - 1)), 50, 4);
                    }
                    else
                    {
                        rectTransform.anchoredPosition = new Vector3((-1000f + i * 2000 / (map.sizeX - 1)), (380 - j * 680 / (map.sizeY - 1)), 4);
                    }
                    rectTransform.pivot = new Vector2(0.5f, 1);
                    if (map.sizeX <= 7 && map.sizeY <= 3)
                    {
                        mapItem.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    }
                    else
                    {
                        mapItem.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    }
                    yield return null;
                }
            }
        }
    }

    private void drawLineBetweenObjects(int xObj1, int yObj1, int xObj2, int yObj2)
    {
        // Getting the map Items for drawing a line in between
        RectTransform object1RectTransform = getMapItemFromCoordinates(xObj1, yObj1).GetComponent<RectTransform>();
        RectTransform object2RectTransform = getMapItemFromCoordinates(xObj2, yObj2).GetComponent<RectTransform>();

        // Getting the anchored position of the elements
        float anchoredY1 = object1RectTransform.anchoredPosition.y;
        float anchoredY2 = object2RectTransform.anchoredPosition.y;

        // Defining pivot depending on Y position of the 2 elements
        if (anchoredY1 + 50 > anchoredY2 && anchoredY1 - 50 < anchoredY2)
        {
            object1RectTransform.SetPivot(new Vector2(1, 0.5f));
            object2RectTransform.SetPivot(new Vector2(0, 0.5f));
        }
        else if (anchoredY1 + 150 > anchoredY2 && anchoredY1 < anchoredY2)
        {
            object1RectTransform.SetPivot(new Vector2(0.95f, 0.65f));
            object2RectTransform.SetPivot(new Vector2(0.05f, 0.35f));
        }
        else if (anchoredY1 - 150 < anchoredY2 && anchoredY1 > anchoredY2)
        {
            object1RectTransform.SetPivot(new Vector2(0.95f, 0.35f));
            object2RectTransform.SetPivot(new Vector2(0.05f, 0.65f));
        }
        else if (anchoredY2 > anchoredY1)
        {
            object1RectTransform.SetPivot(new Vector2(0.85f, 0.85f));
            object2RectTransform.SetPivot(new Vector2(0.15f, 0.15f));
        }
        else
        {
            object1RectTransform.SetPivot(new Vector2(0.85f, 0.15f));
            object2RectTransform.SetPivot(new Vector2(0.15f, 0.85f));
        }

        float xLine0 = object1RectTransform.position.x;
        float yLine0 = object1RectTransform.position.y;
        float xLine1 = object2RectTransform.position.x;
        float yLine1 = object2RectTransform.position.y;

        // Instantiating the line prefab in the canvas
        GameObject canvasObject = GameObject.Find("Canvas");
        RectTransform imageRectTransform = GameObject.Instantiate(linePrefab, Vector3.zero, Quaternion.identity, canvasObject.transform).GetComponent<RectTransform>();

        // Setting the points to the world map position of the map items
        Vector3 pointA = new Vector3(xLine0, yLine0, 0);
        Vector3 pointB = new Vector3(xLine1, yLine1, 0);

        // Getting the Vector between the 2 points
        Vector3 differenceVector = pointB - pointA;

        // Setting the image arrow to the right position and right size
        int lineHeight = 10;
        imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude / canvasObject.GetComponent<Canvas>().scaleFactor, lineHeight);
        imageRectTransform.pivot = new Vector2(0, 0.5f);
        imageRectTransform.position = pointA;

        // Calculating and setting the angle of the line to reach the end point
        float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        imageRectTransform.localRotation = Quaternion.Euler(0, 0, angle);

        // Restoring the Pivot to the previous Position so that the tooltip is correctly displayed
        object2RectTransform.SetPivot(new Vector2(0.5f, 1));
        object1RectTransform.SetPivot(new Vector2(0.5f, 1));
    }

}

public static class Pivot
{
    /// <summary>
    /// Set pivot without changing the position of the element
    /// </summary>
    public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
    {
        Vector3 deltaPosition = rectTransform.pivot - pivot;    // get change in pivot
        deltaPosition.Scale(rectTransform.rect.size);           // apply sizing
        deltaPosition.Scale(rectTransform.localScale);          // apply scaling
        deltaPosition = rectTransform.rotation * deltaPosition; // apply rotation

        rectTransform.pivot = pivot;                            // change the pivot
        rectTransform.localPosition -= deltaPosition;           // reverse the position change
    }
}

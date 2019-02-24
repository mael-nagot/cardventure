using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMap", menuName = "GameData/Map", order = 1)]
public class Map : ScriptableObject
{
    public GameObject mapBackground;
    public GameObject eventBackground;
    public GameObject battleBackground;

    public AudioClip bgmFile;
    public AudioClip bgsFile;
    public int level;
    public int sizeX;
    public int sizeY;
    public Tile[] possibleTiles;
    public int[] possibleTroopsEncountered;
    public int[] possibleEvents;

}

[System.Serializable]
public class Tile
{
    [Tooltip("1-Random, 2-Fight, 3-Rest, 4-Chest")]
    public int eventType;
    [Tooltip("The prefab to be displayed for this tile")]
    public GameObject tilePrefab;
    [Tooltip("Is it the default tile of the Map?")]
    public bool isDefault;
    [Tooltip("Minimum number of tile to be displayed in the Map (-1 means no Min Number)")]
    public int minNumber = -1;
    [Tooltip("Maximum number of tile to be displayed in the Map (-1 means no Max Number)")]
    public int maxNumber = -1;
    [Tooltip("Minimum X coordinate on the map the tile can be displayed (-1 means no Min X)")]
    public int minX = -1;
    [Tooltip("Maximum X coordinate on the map the tile can be displayed (-1 means no Max X)")]
    public int maxX = -1;

}

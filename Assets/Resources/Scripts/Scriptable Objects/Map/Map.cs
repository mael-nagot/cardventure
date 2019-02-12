using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMap", menuName = "GameData/Map", order = 1)]
public class Map : ScriptableObject
{
    public GameObject mapBackground;
    public GameObject eventBackground;
    public GameObject battleBackground;

    public string bgmFile;
    public string bgsFile;
    public int level;
    public int[] possibleTiles;
    public int[] possibleTroopsEncountered;
    public int[] possibleEvents;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public string localizationLanguage;
    public bool isGameSessionStarted = false;
    public int level;
    public string currentMap;
    public int[,] mapTiles;
    public List<MapItemLink> mapItemLinks;

}

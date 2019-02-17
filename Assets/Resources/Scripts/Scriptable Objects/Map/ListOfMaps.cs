using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class MapItem
{
    public string key;
    public Map map;

    public MapItem(string keyToSet, Map mapToSet)
    {
        key = keyToSet;
        map = mapToSet;
    }
}


[CreateAssetMenu(fileName = "NewListOfMaps", menuName = "GameData/ListOfMaps", order = 2)]
public class ListOfMaps : ScriptableObject
{
    public List<MapItem> listOfMaps;

    public Map getMap(string mapName)
    {
        Map map = null;
        foreach (MapItem mapItem in this.listOfMaps)
        {
            if (mapItem.key == mapName)
            {
                map = mapItem.map;
            }
        }
        if (map != null)
        {
            return map;
        }
        else
        {
            throw new Exception("Map does not exist");
        }
    }
}

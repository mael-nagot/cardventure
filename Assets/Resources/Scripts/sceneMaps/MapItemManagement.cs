using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemManagement : MonoBehaviour
{
    public void onLongTap()
    {
        Debug.Log("Long tap detected on object: "+this.name);
    }

    public void onTap()
    {
        Debug.Log("Simple tap detected on object: "+this.name);
    }
}

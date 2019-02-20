using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour
{
    // Move the element to be the last child of the canvas so it is displayed on the top 
    void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}

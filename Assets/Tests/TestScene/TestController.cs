using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public static TestController instance;
    public ListOfMaps listOfMaps;
    public GameObject dataController;
    public GameObject loadingScreenController;
    public GameObject localizationManager;
    public GameObject soundController;
    public GameObject uiManager;
    public GameObject tooltip;
    public GameObject modalPanel;
    public GameObject genericButton;
    public AudioClip bgmForest;
    public AudioClip bgmIntro;
    public AudioClip bgmDungeon;
    public AudioClip bgsForest;
    public AudioClip bgsDungeon;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}

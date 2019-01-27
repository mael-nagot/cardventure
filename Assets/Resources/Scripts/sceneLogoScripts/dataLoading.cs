using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class DataLoading : MonoBehaviour
{

    [SerializeField]
    private float waitTime;
    private bool nextSceneIsLoading = false;

    void Awake()
    {
        DOTween.Init();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        // Tween to make the logo fade in and then fade out after a while
        Sequence logoDisplaySequence = DOTween.Sequence();
        logoDisplaySequence
                .Append(GameObject.Find("Logo").transform.DOScale(new Vector3(1, 1, 1), waitTime * 1.5f / 5))
                .Join(GameObject.Find("Logo").GetComponent<Image>().DOFade(1, waitTime * 2.5f / 5))
                .AppendInterval(1)
                .Append(GameObject.Find("Logo").GetComponent<Image>().DOFade(0, waitTime * 1.5f / 5))
                .OnComplete(() => LoadNextScene());
        // Load the localization if a save file exists and a language has already been set.
        if (!String.IsNullOrEmpty(DataController.instance.gameData.localizationLanguage))
        {
            LocalizationManager.instance.LoadLocalizedText("localizedText_" + DataController.instance.gameData.localizationLanguage + ".json");
        }
    }

    void Update()
    {

        // If clicking on the screen, skip the logo presentation
        if (Input.touchCount > 0 && !nextSceneIsLoading)
        {
            LoadNextScene();
            nextSceneIsLoading = true;
        }
    }

    void LoadNextScene()
    {
        /* 
        If a save file exists and a language has been set, wait for the localization to be loaded to load the menu
        otherwise, load the language selection scene.
        */
        if (!String.IsNullOrEmpty(DataController.instance.gameData.localizationLanguage))
        {
            StartCoroutine("checkLocalizationReadyAndLoadMenuScene");
        }
        else
        {
            SceneManager.LoadScene("languageSelection", LoadSceneMode.Single);
        }
    }

    // Wait for the localization to be loaded before loading the manu scene
    private IEnumerator checkLocalizationReadyAndLoadMenuScene()
    {
        while (!LocalizationManager.instance.GetIsReady())
        {
            yield return null;
        }

        StartCoroutine(LoadingScreenController.instance.loadScene("menu"));
    }
}

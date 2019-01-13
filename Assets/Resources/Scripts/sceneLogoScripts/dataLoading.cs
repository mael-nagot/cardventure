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

    void Awake()
    {
        DOTween.Init();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        Sequence logoDisplaySequence = DOTween.Sequence();
        logoDisplaySequence
                .Append(GameObject.Find("Logo").transform.DOScale(new Vector3(1, 1, 1), waitTime * 1.5f / 5))
                .Join(GameObject.Find("Logo").GetComponent<Image>().DOFade(1, waitTime * 2.5f / 5))
                .AppendInterval(1)
                .Append(GameObject.Find("Logo").GetComponent<Image>().DOFade(0, waitTime * 1.5f / 5))
                .OnComplete(() => LoadNextScene());
        logoDisplaySequence.Play();
        if (!String.IsNullOrEmpty(DataController.instance.gameData.localizationLanguage))
        {
            LocalizationManager.instance.LoadLocalizedText("localizedText_" + DataController.instance.gameData.localizationLanguage + ".json");
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        if (!String.IsNullOrEmpty(DataController.instance.gameData.localizationLanguage))
        {
            StartCoroutine("checkLocalizationReadyAndLoadMenuScene");
        }
        else
        {
            SceneManager.LoadScene("languageSelection", LoadSceneMode.Single);
        }
    }

    private IEnumerator checkLocalizationReadyAndLoadMenuScene()
    {
        while (!LocalizationManager.instance.GetIsReady())
        {
            yield return null;
        }

        SceneManager.LoadScene("menu");
    }
}

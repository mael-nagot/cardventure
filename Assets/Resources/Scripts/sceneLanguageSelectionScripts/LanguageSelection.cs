using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LanguageSelection : MonoBehaviour
{
    public Button ButtonFrench;
    public Button ButtonEnglish;

    void Awake()
    {
        ButtonFrench.onClick.AddListener(() => loadLocalization("fr"));
        ButtonEnglish.onClick.AddListener(() => loadLocalization("en"));        
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        Sequence flagFadeIn = DOTween.Sequence();
        Sequence flagAnimation = DOTween.Sequence();
        flagFadeIn
                .Append(GameObject.Find("TextLanguage").GetComponent<Text>().DOFade(1, 1))
                .Join(GameObject.Find("ButtonFrench").GetComponent<Image>().DOFade(1, 1))
                .Join(GameObject.Find("ButtonEnglish").GetComponent<Image>().DOFade(1, 1));
        flagAnimation
                .Append(GameObject.Find("ButtonFrench").GetComponent<Image>().transform.DOScale(new Vector3(0.8f, 0.8f, 1), 1))
                .Join(GameObject.Find("ButtonEnglish").GetComponent<Image>().transform.DOScale(new Vector3(0.8f, 0.8f, 1), 1))
                .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if (LocalizationManager.instance.GetIsReady ())
        {
            Sequence flagFadeOut = DOTween.Sequence();
            flagFadeOut
                .Append(GameObject.Find("TextLanguage").GetComponent<Text>().DOFade(0, 1))
                .Join(GameObject.Find("ButtonFrench").GetComponent<Image>().DOFade(0, 1))
                .Join(GameObject.Find("ButtonEnglish").GetComponent<Image>().DOFade(0, 1))
                .OnComplete(() => SceneManager.LoadScene ("map"));
        }
    }

    //Loading Localization and saving the language choice in the game data json file
    private void loadLocalization(string language)
    {
        LocalizationManager.instance.LoadLocalizedText("localizedText_" + language + ".json");
        DataController.instance.gameData.localizationLanguage = language;
        DataController.instance.SaveGameData();
    }
}

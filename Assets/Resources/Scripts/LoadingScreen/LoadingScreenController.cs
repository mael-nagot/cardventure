using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public static LoadingScreenController instance;
    public GameObject loadingScreen;
    public Slider slider;

    AsyncOperation sceneLoading;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    public IEnumerator loadScene(string sceneToLoad)
    {
        loadingScreen.SetActive(true);
        sceneLoading = SceneManager.LoadSceneAsync(sceneToLoad);
        sceneLoading.allowSceneActivation = false;

        while (sceneLoading.isDone == false)
        {
            slider.value = sceneLoading.progress;
            if (sceneLoading.progress == 0.9f)
            {
                slider.value = 1;
                sceneLoading.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

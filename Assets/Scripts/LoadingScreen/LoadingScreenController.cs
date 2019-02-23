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

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// This method display the loading screen and then loads the scene in parameter asynchronously.
    /// It is retrieving the loading progress and updating the load bar accordingly
    /// </summary>
    /// <param name="string sceneToLoad">The name of the scene to be loaded</param>
    /// <param name="bool additive">Is it loaded on addition to current loaded scene or does it replace it?</param>
    public IEnumerator loadScene(string sceneToLoad, bool additive = false)
    {
        loadingScreen.SetActive(true);
        if (additive)
        {
            sceneLoading = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        }
        else
        {
            sceneLoading = SceneManager.LoadSceneAsync(sceneToLoad);
        }
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

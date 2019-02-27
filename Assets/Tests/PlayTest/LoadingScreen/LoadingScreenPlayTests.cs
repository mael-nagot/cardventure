using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class LoadingScreenPlayTests
    {
        [UnityTest]
        public IEnumerator TestSceneIsLoadedWhileLoadingScreenIsDone()
        {
            Debug.Log("This test checks that when loading a scene using the loading screen function, the scene is active at the end");
            yield return null;
            yield return loadLocalization();
            GameObject.Instantiate(TestController.instance.loadingScreenController as GameObject);
            yield return null;
            yield return LoadingScreenController.instance.loadScene("logo", true);
            Assert.IsNotNull(GameObject.Find("Logo"));
        }

        [SetUp]
        public void loadTestScene()
        {
            if (SceneManager.GetActiveScene().name != "testScene")
            {
                SceneManager.LoadScene("testScene", LoadSceneMode.Single);
            }
        }

        [TearDown]
        public void unloadTestScene()
        {
            if (SceneManager.GetActiveScene().name != "testScene")
            {
                SceneManager.UnloadSceneAsync("testScene");
            }
        }

        [TearDown]
        public void destroyAllGameObjects()
        {
            foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
            {
                Object.Destroy(go);
            }
        }

        public IEnumerator loadLocalization()
        {
            GameObject.Instantiate(TestController.instance.localizationManager as GameObject);
            yield return null;
            LocalizationManager.instance.LoadLocalizedText("localizedText_en.json");

            while (!LocalizationManager.instance.GetIsReady())
            {
                yield return null;
            }
        }

    }
}

using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class LocalizationPlayTests
    {
        [UnityTest]
        public IEnumerator TestLocalizationManagerIsInstantiatedInFirstScene()
        {
            Debug.Log("This test checks that the Localization Manager is instantiated from the very first scene.");
            SceneManager.LoadScene("logo", LoadSceneMode.Additive);
            yield return null;
            GameObject localizationManager = GameObject.FindWithTag("LocalizationManager");
            Assert.IsNotNull(localizationManager);
        }

        [UnityTest]
        [TestCase("localizedText_en.json", "new_game_menu", "New Game", ExpectedResult = null)]
        [TestCase("localizedText_fr.json", "continue_game_menu", "Continuer", ExpectedResult = null)]
        public IEnumerator TestLoadingLocalizationFileAndGettingValue(string file, string key, string value)
        {
            Debug.Log("This test checks that the LoadLocalizedText actually loads the right localization file and that the GetLocalizedValue retrieve the expected localization value from a key.");
            yield return null;
            GameObject.Instantiate(TestController.instance.localizationManager as GameObject);
            yield return null;
            LocalizationManager.instance.LoadLocalizedText(file);

            while (!LocalizationManager.instance.GetIsReady())
            {
                yield return null;
            }
            Assert.AreEqual(LocalizationManager.instance.GetLocalizedValue(key), value);
        }

        [UnityTest]
        [TestCase("localizedText_en.json", "new_game_menu", "New Game", ExpectedResult = null)]
        [TestCase("localizedText_fr.json", "continue_game_menu", "Continuer", ExpectedResult = null)]
        public IEnumerator TestTextObjectBeingLocalized(string file, string key, string value)
        {
            Debug.Log("This test checks that a text object instantiated with the LocalizedText script and a key is automatically localized.");
            yield return null;
            GameObject.Instantiate(TestController.instance.localizationManager as GameObject);
            yield return new WaitForSeconds(1);
            LocalizationManager.instance.LoadLocalizedText(file);

            while (!LocalizationManager.instance.GetIsReady())
            {
                yield return null;
            }
            GameObject textGameObject = new GameObject("TextObject");
            textGameObject.AddComponent<Text>();
            textGameObject.GetComponent<Text>().text = "Test";
            textGameObject.AddComponent<LocalizedText>();
            textGameObject.GetComponent<LocalizedText>().key = key;
            yield return null;
            Assert.AreEqual(textGameObject.GetComponent<Text>().text, value);
        }

        [SetUp]
        public void loadTestScene()
        {
            SceneManager.LoadScene("testScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void unLoadTestScene()
        {
            SceneManager.UnloadSceneAsync("testScene");
        }

        [TearDown]
        public void destroyAllGameObjects()
        {
            foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
            {
                Object.Destroy(go);
            }
        }

    }

}

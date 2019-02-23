using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class MapTest
    {
        [UnityTest]
        public IEnumerator TestGetMapFunction()
        {
            Debug.Log("This test checks that the GetMap function gets the correct map objet from a string");
            SceneManager.LoadScene("testScene", LoadSceneMode.Single);
            yield return null;
            ListOfMaps list = TestController.instance.listOfMaps;
            Assert.AreEqual(list.getMap("Forest").bgmFile.name, "BgmForest");
            yield return SceneManager.UnloadSceneAsync("testScene");

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

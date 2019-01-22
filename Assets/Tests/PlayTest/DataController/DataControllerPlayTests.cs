using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using NSubstitute;
using System.IO;

namespace Tests
{
    public class DataControllerPlayTests
    {

        [UnityTest]
		// This test checks that the data controller is instantiated on the very first scene loaded
        public IEnumerator TestDataControllerIsInstantiatedInFirstScene()
        {
			Debug.Log("This test checks that the data controller is instantiated on the very first scene loaded");
            SceneManager.LoadScene("logo", LoadSceneMode.Single);
            yield return null;
            GameObject dataController = GameObject.FindWithTag("DataController");
			Assert.IsNotNull(dataController);
        }

        [UnityTest]
		// This test checks that a test data save file is correctly loaded using the LoadGameData() of the data controller
        public IEnumerator TestLoadDataFileOnAnExistingFile()
        {
			Debug.Log("This test checks that a test data save file is correctly loaded using the LoadGameData() of the data controller");
			string testDataPath = "Assets/Tests/TestsData/DataController/data.json";
			yield return null;
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/DataController") as GameObject);
			yield return null;

			DataController.instance.filePath = testDataPath;
			DataController.instance.LoadGameData();
			while(!DataController.instance.GetIsReady())
			{
				yield return null;
			}
			Assert.AreEqual(DataController.instance.gameData.localizationLanguage,"ja");		
        }

        [UnityTest]
		// This test checks that the LoadGameData() is still creating a gameData object when the loading file has not been created
        public IEnumerator TestLoadDataFileOnANonExistingFileCreatesGameDataObject()
        {
			Debug.Log("This test checks that the LoadGameData() is still creating a gameData object when the loading file has not been created");
			string dummyPath = "Assets/Tests/TestsData/DataController/dummy.json";
			yield return null;
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/DataController") as GameObject);
			yield return null;

			DataController.instance.filePath = dummyPath;
			DataController.instance.LoadGameData();
			while(!DataController.instance.GetIsReady())
			{
				yield return null;
			}
			Assert.IsNotNull(DataController.instance.gameData);
			File.Delete(dummyPath);
        }

        [UnityTest]
		// This test checks that the LoadGameData() creates a save file if there is none existing
        public IEnumerator TestLoadDataFileCreatesSaveFileIfNoneExists()
        {
			Debug.Log("This test checks that the LoadGameData() creates a save file if there is none existing");
			string dummyPath = "Assets/Tests/TestsData/DataController/dummy.json";
			yield return null;
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/DataController") as GameObject);
			yield return null;

			DataController.instance.filePath = dummyPath;
			DataController.instance.LoadGameData();
			while(!DataController.instance.GetIsReady())
			{
				yield return null;
			}
			Assert.IsTrue(File.Exists(dummyPath));
			File.Delete(dummyPath);
        }

        [UnityTest]
		// This test checks that when loading a save file and saving it directly, the file is not modified (Load and Save are consistant)
        public IEnumerator LoadingAFileAndSavingItDoesNotModifyIt()
        {
			Debug.Log("This test checks that when loading a save file and saving it directly, the file is not modified (Load and Save are consistant)");
			string loadDataPath = "Assets/Tests/TestsData/DataController/data.json";
			string saveDataPath = "Assets/Tests/TestsData/DataController/data2.json";
			yield return null;
			GameObject.Instantiate(Resources.Load("Prefabs/Controllers/DataController") as GameObject);
			yield return null;

			string loadFileContent = File.ReadAllText(loadDataPath);
			DataController.instance.filePath = loadDataPath;
			DataController.instance.LoadGameData();
			while(!DataController.instance.GetIsReady())
			{
				yield return null;
			}
			DataController.instance.filePath = saveDataPath;
			DataController.instance.SaveGameData();
			yield return null;
			string saveFileContent = File.ReadAllText(saveDataPath);
			Assert.AreEqual(saveFileContent,loadFileContent);
			File.Delete(saveDataPath);
        }

		[TearDown]
		public void destroyAllGameObjects()
		{
			foreach (GameObject go in Object.FindObjectsOfType<GameObject>()) {
            	 Object.Destroy(go);
         	}
		}
    }
}
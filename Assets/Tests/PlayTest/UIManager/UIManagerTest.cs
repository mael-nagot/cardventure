using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class UIManagerTest
    {
        [UnityTest]
        public IEnumerator TestShowTooltipFunctionMakeTheToolTipActive()
        {
            Debug.Log("This test checks that the ShowToolTip function activates the tooltip");
            GameObject canvasObject = createCanvas();

            GameObject.Instantiate(TestController.instance.uiManager as GameObject);
            yield return null;
            GameObject tooltip = GameObject.Instantiate(TestController.instance.tooltip, Vector3.zero, Quaternion.identity, canvasObject.transform) as GameObject;
            yield return null;
            UIManager.instance.tooltip = tooltip;
            UIManager.instance.showToolTip(Vector3.zero, "Bananas");
            Assert.IsTrue(tooltip.activeSelf);
        }

        [UnityTest]
        public IEnumerator TestShowTooltipFunctionMakeTheToolTipDisplayedWithTheRightText()
        {
            Debug.Log("This test checks that the ShowToolTip function display the tooltip with the expected text");
            GameObject canvasObject = createCanvas();

            GameObject.Instantiate(TestController.instance.uiManager as GameObject);
            yield return null;
            GameObject tooltip = GameObject.Instantiate(TestController.instance.tooltip, Vector3.zero, Quaternion.identity, canvasObject.transform) as GameObject;
            yield return null;
            UIManager.instance.tooltip = tooltip;
            UIManager.instance.showToolTip(Vector3.zero, "Bananas");
            Assert.AreEqual(tooltip.transform.GetChild(0).GetComponent<Text>().text, "Bananas");
        }

        [UnityTest]
        public IEnumerator TestHideTooltipFunctionMakeTheToolTipInactive()
        {
            Debug.Log("This test checks that the HideToolTip function deactivates the tooltip");
            GameObject canvasObject = createCanvas();

            GameObject.Instantiate(TestController.instance.uiManager as GameObject);
            yield return null;
            GameObject tooltip = GameObject.Instantiate(TestController.instance.tooltip, Vector3.zero, Quaternion.identity, canvasObject.transform) as GameObject;
            yield return null;
            UIManager.instance.tooltip = tooltip;
            UIManager.instance.showToolTip(Vector3.zero, "Bananas");
            UIManager.instance.hideToolTip();
            Assert.IsFalse(tooltip.activeSelf);
        }

        [UnityTest]
        public IEnumerator TestModalPanelChoiceGeneratesTheButtonCorrectly()
        {
            Debug.Log("This test checks that the Modal Panel instantiates the right buttons when calling it");
            yield return makeLocalizationReady();
            yield return initializeUIManagerAndModalPanel();
            yield return UIManager.instance.modalPanelChoice("quit_confirm", new string[] { "yes", "no", "banana" });
            // -1 in the assertion because one button is instantiated for the test
            Assert.AreEqual(GameObject.FindGameObjectsWithTag("Button").Length - 1, 3);

        }

        [UnityTest]
        public IEnumerator TestModalPanelChoiceSetTheExpectedQuestionText()
        {
            Debug.Log("This test checks that the Modal Panel instantiates the question text with the right localization");
            yield return makeLocalizationReady();
            yield return initializeUIManagerAndModalPanel();
            yield return UIManager.instance.modalPanelChoice("quit_confirm", new string[] { "yes", "no", "banana" });
            Assert.AreEqual(GameObject.Find("Text").GetComponent<Text>().text, "Are you sure you want to quit?");
        }

        [UnityTest]
        public IEnumerator TestModalPanelChoiceSelectionReturnCorrectResult()
        {
            Debug.Log("This test checks that when clicking on the button of a modal panel, the correct result is returned");
            yield return makeLocalizationReady();
            yield return initializeUIManagerAndModalPanel();
            yield return UIManager.instance.modalPanelChoice("quit_confirm", new string[] { "yes", "no", "banana" });
            Assert.AreEqual(GameObject.Find("Text").GetComponent<Text>().text, "Are you sure you want to quit?");
            Button banana = GameObject.Find("PanelButton3").GetComponent<Button>();
            banana.onClick.Invoke();
            yield return null;
            Assert.AreEqual(UIManager.instance.modalPanelAnswer, "banana");
        }

        [UnityTest]
        public IEnumerator TestModalPanelCloseAfterClickingAButton()
        {
            Debug.Log("This test checks that the Modal Panel is closed when clicking on an answer button");
            yield return makeLocalizationReady();
            yield return initializeUIManagerAndModalPanel();
            yield return UIManager.instance.modalPanelChoice("quit_confirm", new string[] { "yes", "no", "banana" });
            Button no = GameObject.Find("PanelButton2").GetComponent<Button>();
            no.onClick.Invoke();
            yield return null;
            Assert.IsFalse(UIManager.instance.modalPanelGameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator TestGenericButtonsDeletedAfterModalPanelClosed()
        {
            Debug.Log("This test checks that the Generic Buttons are deleted after the modal panel is closed");
            yield return makeLocalizationReady();
            yield return initializeUIManagerAndModalPanel();
            yield return UIManager.instance.modalPanelChoice("quit_confirm", new string[] { "yes", "no", "banana" });
            Button no = GameObject.Find("PanelButton2").GetComponent<Button>();
            no.onClick.Invoke();
            yield return null;
            UIManager.instance.modalPanelGameObject.SetActive(true);
            // -1 in the assertion because one button is instantiated for the test
            Assert.AreEqual(GameObject.FindGameObjectsWithTag("Button").Length - 1, 0);
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

        private GameObject createCanvas()
        {
            GameObject canvasObject = new GameObject();
            canvasObject.name = "Canvas";
            canvasObject.AddComponent<Canvas>();
            canvasObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            return canvasObject;
        }

        private IEnumerator makeLocalizationReady()
        {
            GameObject.Instantiate(TestController.instance.localizationManager as GameObject);
            yield return null;
            LocalizationManager.instance.LoadLocalizedText("localizedText_en.json");

            while (!LocalizationManager.instance.GetIsReady())
            {
                yield return null;
            }
        }

        private IEnumerator initializeUIManagerAndModalPanel()
        {
            GameObject canvasObject = createCanvas();

            GameObject.Instantiate(TestController.instance.uiManager as GameObject);
            yield return null;
            GameObject modalPanel = GameObject.Instantiate(TestController.instance.modalPanel, Vector3.zero, Quaternion.identity, canvasObject.transform) as GameObject;
            yield return null;
            GameObject button = GameObject.Instantiate(TestController.instance.genericButton, Vector3.zero, Quaternion.identity, canvasObject.transform) as GameObject;
            yield return null;
            UIManager.instance.modalPanelGenericButton = button.GetComponent<Button>();
            UIManager.instance.modalPanelGameObject = modalPanel;
            UIManager.instance.modalPanelQuestion = GameObject.Find("Text").GetComponent<Text>();
        }

    }
}

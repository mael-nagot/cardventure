using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject tooltip;
    public Text modalPanelQuestion;
    [SerializeField]
    private Image modalPanelIconImage;
    public Button modalPanelGenericButton;
    private GameObject[] buttonsList;
    public GameObject modalPanelGameObject;
    public string modalPanelAnswer;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// This method display a tooltip.
    /// Example: UIManager.instance.showToolTip(this.transform.position, LocalizationManager.instance.GetLocalizedValue("quit_game_menu"));
    /// It will display the tooltip at the position of the object calling the function with the localized text having "quit_game_menu" as a key
    /// </summary>
    /// <param name="Vector3 position">The position where to display the tooltip</param>
    /// <param name="string text">The text to display in the tooltip</param>
    public void showToolTip(Vector3 position, string text)
    {
        tooltip.SetActive(true);
        tooltip.transform.GetChild(0).GetComponent<Text>().text = text;
        tooltip.transform.position = position;
        // Do position calculation so that the tooltip is not displayed out of the screen
        RectTransform rectTransformCanvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        RectTransform rectTransform = tooltip.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(1, 1);
        if (rectTransform.anchoredPosition.x < -rectTransformCanvas.rect.width / 2 + 3 / 2 * rectTransform.rect.width)
        {
            rectTransform.pivot = new Vector2(0, rectTransform.pivot.y);
        }
        else
        {
            rectTransform.pivot = new Vector2(1, rectTransform.pivot.y);
        }
        if (rectTransform.anchoredPosition.y > rectTransformCanvas.rect.height / 2 - rectTransform.rect.height)
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 1);
        }
        else
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0);
        }
    }

    /// <summary>
    /// If a tooltip is currently displayed, this method hides it.
    /// </summary>
    public void hideToolTip()
    {
        tooltip.SetActive(false);
    }

    /// <summary>
    /// This method open a modal panel having a question (Localized) and one button per answer in parameter
    /// Example: StartCoroutine(UIManager.instance.modalPanelChoice("quit_confirm", new string[] { "yes", "no" }));
    /// It will display a modal panel having the key "quit_confirm" localized and having 2 buttons yes and no.
    /// </summary>
    /// <param name="string questionKey">The key of the question to localize</param>
    /// <param name="string[] buttons">An array containing the different buttons to create</param>
    public IEnumerator modalPanelChoice(string questionKey, string[] buttons)
    {
        modalPanelAnswer = null;
        buttonsList = new GameObject[buttons.Length];
        openModalPanel();
        GameObject buttonPanel = GameObject.Find("ButtonPanel");
        modalPanelQuestion.text = LocalizationManager.instance.GetLocalizedValue(questionKey);
        // Create all the modal panel buttons and set listeners to them
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonsList[i] = GameObject.Instantiate(Resources.Load("Prefabs/UI/Button"), Vector3.zero, Quaternion.identity, buttonPanel.transform) as GameObject;
            yield return null;
            buttonsList[i].name = "PanelButton" + (i + 1);
            buttonsList[i].GetComponentInChildren<Text>().text = LocalizationManager.instance.GetLocalizedValue(buttons[i]);
            int i2 = i;
            buttonsList[i].GetComponent<Button>().onClick.AddListener(() => getChoiceAndClosePanel(buttons[i2]));
        }
    }

    /*
    This function is called when clicking a button in the modal panel.
    It set the public variable modalPanelAnswer to the string of the button pressed so that it can be accessed by another class.
     */
    private void getChoiceAndClosePanel(string button)
    {
        modalPanelAnswer = button;
        for (int i = 0; i < buttonsList.Length; i++)
        {
            buttonsList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            Object.Destroy(buttonsList[i]);
        }
        buttonsList = null;
        closeModalPanel();
    }

    private void openModalPanel()
    {
        modalPanelGameObject.SetActive(true);
    }
    private void closeModalPanel()
    {
        modalPanelGameObject.SetActive(false);
    }
}

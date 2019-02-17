using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private GameObject tooltip;
    [SerializeField]
    private Text modalPanelQuestion;
    [SerializeField]
    private Image modalPanelIconImage;
    [SerializeField]
    private Button modalPanelGenericButton;
    private GameObject[] buttonsList;
    [SerializeField]
    private GameObject modalPanelGameObject;
    public string modalPanelAnswer;

    void Awake()
    {
        instance = this;
    }


    public void showToolTip(Vector3 position, string text)
    {
        tooltip.SetActive(true);
        tooltip.transform.GetChild(0).GetComponent<Text>().text = text;
        tooltip.transform.position = position;
        RectTransform rectTransformCanvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        RectTransform rectTransform = tooltip.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(1,1);
        if (rectTransform.anchoredPosition.x < -rectTransformCanvas.rect.width/2 + 3/2*rectTransform.rect.width)
        {
            rectTransform.pivot = new Vector2(0, rectTransform.pivot.y);
        }
        else
        {
            rectTransform.pivot = new Vector2(1, rectTransform.pivot.y);
        }
        if (rectTransform.anchoredPosition.y > rectTransformCanvas.rect.height/2 - rectTransform.rect.height)
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 1);
        }
        else
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0);
        }
    }

    public void hideToolTip()
    {
        tooltip.SetActive(false);
    }

    public IEnumerator modalPanelChoice(string questionKey, string[] buttons)
    {
        modalPanelAnswer = null;
        buttonsList = new GameObject[buttons.Length];
        openModalPanel();
        GameObject buttonPanel = GameObject.Find("ButtonPanel");
        modalPanelQuestion.text = LocalizationManager.instance.GetLocalizedValue(questionKey);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonsList[i] = GameObject.Instantiate(Resources.Load("Prefabs/UI/Button"), Vector3.zero, Quaternion.identity, buttonPanel.transform) as GameObject;
            yield return null;
            buttonsList[i].GetComponentInChildren<Text>().text = LocalizationManager.instance.GetLocalizedValue(buttons[i]);
            int i2=i;
            buttonsList[i].GetComponent<Button>().onClick.AddListener(() => getChoiceAndClosePanel(buttons[i2]));
        }
    }

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

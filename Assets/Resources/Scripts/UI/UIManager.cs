using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private GameObject tooltip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    //public void showToolTip(Vector3 position)
    public void showToolTip(Vector3 position, string text)
    {
        tooltip.SetActive(true);
        tooltip.transform.SetAsLastSibling();
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
}

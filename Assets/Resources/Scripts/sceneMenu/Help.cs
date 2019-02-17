using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public void onHelpTap()
    {
        Debug.Log("Help Tap");
    }

    public void onHelpLongTapDown()
    {
        UIManager.instance.showToolTip(this.transform.position,LocalizationManager.instance.GetLocalizedValue("help_menu"));
    }

    public void onHelpLongTapUp()
    {
        UIManager.instance.hideToolTip();
    }
}

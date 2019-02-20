using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    /// <summary>
    /// Called when the help button is tapped from the menu
    /// </summary>
    public void onHelpTap()
    {
        Debug.Log("Help Tap");
    }

    /// <summary>
    /// Called when long tapping the help button from the menu.
    /// This function is used to display the tooltip on the help button.
    /// </summary>
    public void onHelpLongTapDown()
    {
        UIManager.instance.showToolTip(this.transform.position, LocalizationManager.instance.GetLocalizedValue("help_menu"));
    }

    /// <summary>
    /// Called after stopping long tapping the help button from the menu.
    /// It is used to hide the tooltip when stopping to long tap on the button.
    /// </summary>
    public void onHelpLongTapUp()
    {
        UIManager.instance.hideToolTip();
    }
}

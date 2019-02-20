using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    /// <summary>
    /// Called when the settings button is tapped from the menu
    /// </summary>
    public void onSettingsTap()
    {
        Debug.Log("Tap on Settings detected");
    }

    /// <summary>
    /// Called when long tapping the settings button from the menu.
    /// This function is used to display the tooltip on the settings button.
    /// </summary>
    public void onSettingsLongTapDown()
    {
        UIManager.instance.showToolTip(this.transform.position, LocalizationManager.instance.GetLocalizedValue("settings_game_menu"));
    }

    /// <summary>
    /// Called after stopping long tapping the settings button from the menu.
    /// It is used to hide the tooltip when stopping to long tap on the button.
    /// </summary>
    public void onSettingsLongTapUp()
    {
        UIManager.instance.hideToolTip();
    }
}

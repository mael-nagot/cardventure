using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public void onSettingsTap()
    {
        Debug.Log("Tap on Settings detected");
    }

    public void onSettingsLongTapDown()
    {
        UIManager.instance.showToolTip(this.transform.position, LocalizationManager.instance.GetLocalizedValue("settings_game_menu"));
    }

    public void onSettingsLongTapUp()
    {
        UIManager.instance.hideToolTip();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{

    /// <summary>
    /// This method is called when clicking the quit button
    /// </summary>
    public void onQuitGameTap()
    {
        StartCoroutine("onQuitGameTapCoroutine");
    }

    /// <summary>
    /// This method open a modal panel for the user to confirm if he really wants to quit the game
    /// </summary>
    private IEnumerator onQuitGameTapCoroutine()
    {
        playClickSound();
        StartCoroutine(UIManager.instance.modalPanelChoice("quit_confirm", new string[] { "yes", "no" }));
        yield return new WaitForSeconds(0.1f);
        while (UIManager.instance.modalPanelAnswer == null)
        {
            yield return null;
        }
        playClickSound();
        string answer = UIManager.instance.modalPanelAnswer;
        if (answer == "yes")
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Called when long tapping the quit button from the menu.
    /// This function is used to display the tooltip on the quit button.
    /// </summary>
    public void onQuitGameLongTapDown()
    {
        UIManager.instance.showToolTip(this.transform.position, LocalizationManager.instance.GetLocalizedValue("quit_game_menu"));
    }


    /// <summary>
    /// Called after stopping long tapping the quit button from the menu.
    /// It is used to hide the tooltip when stopping to long tap on the button.
    /// </summary>
    public void onQuitGameLongTapUp()
    {
        UIManager.instance.hideToolTip();
    }
    private void playClickSound()
    {
        StartCoroutine(SoundController.instance.playSE("click1", 1));
    }

}

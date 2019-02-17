using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void onQuitGameTap()
    {
        StartCoroutine("onQuitGameTapCoroutine");
    }
    private IEnumerator onQuitGameTapCoroutine()
    {
        playClickSound();
        StartCoroutine(UIManager.instance.modalPanelChoice("quit_confirm",new string[]{"yes","no"}));
        yield return new WaitForSeconds(0.1f);
        while(UIManager.instance.modalPanelAnswer == null)
        {
            yield return null;
        }
        playClickSound();
        string answer = UIManager.instance.modalPanelAnswer;
        if(answer == "yes")
        {
            Application.Quit();
        }
    }
    public void onQuitGameLongTapDown()
    {
        UIManager.instance.showToolTip(this.transform.position,LocalizationManager.instance.GetLocalizedValue("quit_game_menu"));
    }

    public void onQuitGameLongTapUp()
    {
        UIManager.instance.hideToolTip();
    }
    private void playClickSound()
    {
        StartCoroutine(SoundController.instance.playSE("click1",1));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Button buttonQuit;
    public Button buttonNewGame;
    public Button buttonContinueGame;
    public Button buttonHelp;
    public Button buttonSettings;
    [SerializeField]
    private ParticleSystem particleSys;

    void Awake()
    {
        // Make the flag buttons loading the localization when clicking on them
        buttonQuit.onClick.AddListener(() => quitGame());
        buttonNewGame.onClick.AddListener(() => StartCoroutine(startNewGame()));
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        // Tween to make the menu items slightly changing color over time
        Sequence menuItemAnimation = DOTween.Sequence();
        Color menuItemTargetColor = new Color32(79,42,8,220);
        menuItemAnimation
                .Append(GameObject.Find("NewGame").GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
                .Join(GameObject.Find("Continue").GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
				.SetLoops(-1, LoopType.Yoyo);
        
        // Tween to make the Particles system displaying the leaves moving right and left so the leaves spread everywhere
        Sequence leaveParticlesSystemAnimation = DOTween.Sequence();
        float xParticleSytem = particleSys.transform.position.x;
        leaveParticlesSystemAnimation
                .Append(particleSys.transform.DOMoveX(xParticleSytem - 8, 2))
				.SetLoops(-1, LoopType.Yoyo);
        yield return null;
    }
	
	void Update () {
		
	}

    public void quitGame()
    {
        playClickSound();
        Application.Quit();
    }

    private void playClickSound()
    {
        StartCoroutine(SoundController.instance.playSE("click1",1));
    }

    public IEnumerator startNewGame()
    {
        playClickSound();
        DataController.instance.gameData.isGameSessionStarted = true;
        DataController.instance.gameData.level = 1;
        particleSys.gameObject.SetActive(false);
        yield return StartCoroutine(LoadingScreenController.instance.loadScene("map"));
    }
}

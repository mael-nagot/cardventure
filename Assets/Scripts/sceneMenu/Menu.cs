using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Button buttonQuit;
    public Button buttonNewGame;
    public Button buttonContinueGame;
    public Button buttonHelp;
    public Button buttonSettings;
    [SerializeField]
    private ParticleSystem particleSys;

    void Awake()
    {
        buttonNewGame.onClick.AddListener(() => StartCoroutine(onStartNewGameTap()));
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        // Tween to make the menu items slightly changing color over time
        Sequence menuItemAnimation = DOTween.Sequence();
        Color menuItemTargetColor = new Color32(79, 42, 8, 220);
        menuItemAnimation
                .Append(GameObject.Find("NewGame").GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
                .Join(GameObject.Find("Continue").GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
                .SetLoops(-1, LoopType.Yoyo);

        // Tween to make the Particles system displaying the leaves moving right and left so the leaves spread everywhere
        Sequence leaveParticlesSystemAnimation = DOTween.Sequence();
        float xParticleSytem = particleSys.transform.position.x;
        leaveParticlesSystemAnimation
                .Append(particleSys.transform.DOMoveX(xParticleSytem - 19, 1))
                .Append(particleSys.transform.DOMoveX(xParticleSytem, 1))
                .SetLoops(-1);
        yield return null;
    }

    void Update()
    {

    }

    private void playClickSound()
    {
        StartCoroutine(SoundController.instance.playSE(SoundContainerMenu.instance.clickSound, 1));
    }

    /// <summary>
    /// Called after clicking on the "New Game" button.
    /// It loads the new game data and load the map scene.
    /// </summary>
    public IEnumerator onStartNewGameTap()
    {
        playClickSound();
        initializeGameData();
        particleSys.gameObject.SetActive(false);
        yield return StartCoroutine(LoadingScreenController.instance.loadScene("map"));
    }

    /// <summary>
    /// This method initializes the game data
    /// </summary>
    private void initializeGameData()
    {
        DataController.instance.gameData.isGameSessionStarted = true;
        DataController.instance.gameData.level = 1;
        DataController.instance.gameData.currentMap = "Forest";
    }
}

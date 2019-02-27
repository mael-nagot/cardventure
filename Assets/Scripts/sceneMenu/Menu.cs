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
        buttonContinueGame.onClick.AddListener(() => StartCoroutine(onContinueNewGameTap()));
    }
    IEnumerator Start()
    {
        // Tween to make the menu items slightly changing color over time
        Sequence menuItemAnimation = DOTween.Sequence();
        Color menuItemTargetColor = new Color32(79, 42, 8, 220);
        if (!DataController.instance.gameData.isGameSessionStarted)
        {
            // If no game ongoing, deactivate continue button
            buttonContinueGame.interactable = false;
            Color buttonTextColor = buttonContinueGame.GetComponentInChildren<Text>().color;
            buttonTextColor.a = 0.5f;
            buttonContinueGame.GetComponentInChildren<Text>().color = buttonTextColor;
            menuItemAnimation
                    .Append(buttonNewGame.gameObject.GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
                    .SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            menuItemAnimation
                    .Append(buttonNewGame.gameObject.GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
                    .Join(buttonContinueGame.gameObject.GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
                    .SetLoops(-1, LoopType.Yoyo);
        }

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
    /// If a game was ongoing it also opens a modal panel to confirm previous game data deletion.
    /// </summary>
    public IEnumerator onStartNewGameTap()
    {
        playClickSound();
        if (DataController.instance.gameData.isGameSessionStarted)
        {
            StartCoroutine(UIManager.instance.modalPanelChoice("new_game_confirm", new string[] { "yes", "no" }));
            yield return new WaitForSeconds(0.1f);
            while (UIManager.instance.modalPanelAnswer == null)
            {
                yield return null;
            }
            playClickSound();
            string answer = UIManager.instance.modalPanelAnswer;
            if (answer == "no")
            {
                yield break;
            }
        }

        initializeGameData();
        yield return StartCoroutine(LoadingScreenController.instance.loadScene("map"));
    }

    /// <summary>
    /// Called after clicking on the "Continue" button.
    /// It calls the map scene.
    /// </summary>
    public IEnumerator onContinueNewGameTap()
    {
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
        DataController.instance.gameData.mapItemLinks = null;
        DataController.instance.gameData.mapTiles = null;
    }
}

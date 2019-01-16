using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        Sequence flagMenuItemAnimation = DOTween.Sequence();
        Color menuItemTargetColor = new Color32(79,42,8,220);
        flagMenuItemAnimation
                .Append(GameObject.Find("NewGame").GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
                .Join(GameObject.Find("Continue").GetComponentInChildren<Text>().DOColor(menuItemTargetColor, 2))
				.SetLoops(-1, LoopType.Yoyo);
        Sequence leaveParticlesSystemAnimation = DOTween.Sequence();
        float xParticleSytem = GameObject.Find("ParticleSystem").transform.position.x;
        leaveParticlesSystemAnimation
                .Append(GameObject.Find("ParticleSystem").transform.DOMoveX(xParticleSytem - 8, 2))
				.SetLoops(-1, LoopType.Yoyo);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

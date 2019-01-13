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
        flagMenuItemAnimation
                .Append(GameObject.Find("NewGame").GetComponent<Text>().DOColor(new Color32(182,226,248,200), 2))
                .Join(GameObject.Find("Continue").GetComponent<Text>().DOColor(new Color32(182,226,248,200), 2))
                .Join(GameObject.Find("Settings").GetComponent<Text>().DOColor(new Color32(182,226,248,200), 2))
				.SetLoops(-1, LoopType.Yoyo);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

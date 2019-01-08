using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlagAnimation : MonoBehaviour {

	IEnumerator Start()
	{
		yield return new WaitForSeconds(0.2f);
		Sequence flagDisplaySequence = DOTween.Sequence();
		Sequence flagAnimation = DOTween.Sequence();
		flagDisplaySequence
		   		.Append(GameObject.Find("TextLanguage").GetComponent<Text>().DOFade(1,1))
			    .Join(GameObject.Find("ButtonFrench").GetComponent<Image>().DOFade(1,1))
				.Join(GameObject.Find("ButtonEnglish").GetComponent<Image>().DOFade(1,1));
		flagAnimation
				.Append(GameObject.Find("ButtonFrench").GetComponent<Image>().transform.DOScale(new Vector3(0.8f,0.8f,1),1))
				.Join(GameObject.Find("ButtonEnglish").GetComponent<Image>().transform.DOScale(new Vector3(0.8f,0.8f,1),1))
				.SetLoops(-1, LoopType.Yoyo);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DataLoading : MonoBehaviour {

	[SerializeField]
	private float _waitTime;

	void Awake()
	{
		DOTween.Init();
	}

	IEnumerator Start()
	{
		yield return new WaitForSeconds(0.2f);
		Sequence logoDisplaySequence = DOTween.Sequence();
		logoDisplaySequence
		   		.Append(GameObject.Find("Logo").transform.DOScale(new Vector3(1,1,1),_waitTime*1.5f/5))
 	   		    .Join(GameObject.Find("Logo").GetComponent<Image>().DOFade(1,_waitTime*2.5f/5))
				.AppendInterval(1)
				.Append(GameObject.Find("Logo").GetComponent<Image>().DOFade(0,_waitTime*1.5f/5))
				.OnComplete(() => LoadNextScene());
		logoDisplaySequence.Play();
	}

	void Update() {
		if (Input.touchCount > 0)
		{
			LoadNextScene();
		} 
	}

	void LoadNextScene() {
		SceneManager.LoadScene("languageSelection", LoadSceneMode.Single);
	}
}

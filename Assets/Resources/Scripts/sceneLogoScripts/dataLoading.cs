using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class dataLoading : MonoBehaviour {

	[SerializeField]
	private float _waitTime;
	private float _timer;


	void Awake()
	{
		DOTween.Init();
	}

	void Start () {
		_timer = 0;
		Sequence mySequence = DOTween.Sequence();
		mySequence.Insert(0,GameObject.Find("Logo").GetComponent<Image>().DOFade(255,_waitTime*1000*1.5f/5))
		   		.Insert(0.6f*_waitTime,GameObject.Find("Logo").GetComponent<Image>().DOFade(-255,_waitTime*1000/5));  
		mySequence.Play();
		
		//GameObject.Find("Logo").GetComponent<Image>().DOFade(0,_waitTime*2000);
	}
	
	// Update is called once per frame
	void Update () {
		_timer += Time.deltaTime;
		if (_timer >= _waitTime)
		{
			LoadNextScene();	
		}
	}

	void LoadNextScene() {
		SceneManager.LoadScene("languageSelection", LoadSceneMode.Single);
	}
}

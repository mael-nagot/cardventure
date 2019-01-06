using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagAnimation : MonoBehaviour {

	private int _direction;

	// Use this for initialization
	void Start () {
		_direction = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (_direction == 0)
		{
			if(this.transform.localScale.x <= 0.9)
			{
				_direction = 1;
			}
			else
			{
				this.transform.localScale -= new Vector3(0.01f,0.01f,0);
			}
		}
		else
		{
			if(this.transform.localScale.x >= 1)
			{
				_direction = 0;
			}
			else
			{
				this.transform.localScale += new Vector3(0.01f,0.01f,0);
			}			
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

	public bool fadeOut = true;

	public float fadeTime;
	public float fadeCounter;

	public bool fadeDone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(fadeOut)
		{
			if(fadeCounter >= fadeTime)
			{
				fadeOut = false;
			}
			else
			{
				fadeCounter += Time.deltaTime;
			}	
		}
		else
		{
			if(fadeCounter <= 0)
			{	
				fadeDone = true;
				gameObject.SetActive(false);
			}
			else
			{
				fadeCounter -= Time.deltaTime;
			}	
		}
		GetComponent<Image>().color = new Color(0,0,0,fadeCounter/fadeTime);
	}


	void OnEnable()
	{
		GetComponent<Image>().color = new Color(0,0,0,0);
		fadeOut = true;
		fadeDone = false;
	}

	void OnDisable()
	{
		fadeOut = false;
	}
}

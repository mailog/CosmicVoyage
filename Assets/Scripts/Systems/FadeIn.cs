using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public Color colorStart;
	public Color colorEnd;

	public float fadeInMult;

	private bool fadeIn;

	private float t;

	// Use this for initialization
	void Start () {
		t = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(fadeIn)
		{
			t += fadeInMult * Time.deltaTime;
			gameObject.GetComponent<Text>().color = Color.Lerp(colorStart, colorEnd, t);	
		}
	}

	void OnEnable()
	{
		fadeIn = true;
		t = 0;
	}

	void OnDisable()
	{
		gameObject.GetComponent<Text>().color = colorStart;
		fadeIn = false;
		t = 0;
	}

}

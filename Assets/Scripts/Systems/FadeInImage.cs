using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour {

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
			gameObject.GetComponent<Image>().color = Color.Lerp(colorStart, colorEnd, t);	
		}
	}

	void OnEnable()
	{
		fadeIn = true;
		t = 0;
	}

	void OnDisable()
	{
		gameObject.GetComponent<Image>().color = colorStart;
		fadeIn = false;
		t = 0;
	}
}

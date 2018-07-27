using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFlash : MonoBehaviour {

	public GameObject fill;

	public Color colorEnd;
	public Color colorStart;

	public float flashTimer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Slider>().value >= gameObject.GetComponent<Slider>().maxValue)  
		{
			float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
			fill.GetComponent<Image>().color = Color.Lerp(colorEnd, colorStart, lerp);
		}
	}
}

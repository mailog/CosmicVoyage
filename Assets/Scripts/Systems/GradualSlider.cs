using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradualSlider : MonoBehaviour {

	private float ogWidth;
	private float ogHeight;
	public float widthMult;

	private bool gradual;

	private float t;

	// Use this for initialization
	void Start () {
		t = 0;
		ogWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;
		ogHeight =  gameObject.GetComponent<RectTransform>().sizeDelta.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(gradual)
		{
			t += widthMult * Time.deltaTime;
			float tmp = Mathf.Lerp(0, ogWidth, t);
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(tmp, ogHeight);	
		}
	}

	void OnEnable()
	{
		gradual = true;
		t = 0;
	}

	void OnDisable()
	{
		gradual = false;
		t = 0;
	}

}

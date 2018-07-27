using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelUpOption : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	public Text displayedText;
	public string optionText;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Button>().onClick.AddListener(selectedOption);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void selectedOption()
	{
		transform.localScale /= 1.2f;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		displayedText.text = optionText;
		transform.localScale *= 1.2f;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		displayedText.text = "";
		transform.localScale /= 1.2f;
	}

	void OnEnable()
	{
		gameObject.GetComponent<Button>().onClick.AddListener(selectedOption);
	}
}

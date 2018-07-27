using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Location : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {

	public string locationName;
	private GameObject gameManager;

	public GameObject galaxyText;

	public bool currentLocation;
	public bool active;
	public bool connected;
	public bool visited;

	public bool salvage;

	public GameObject[] connectedLocations;

	public float spawnRate;
	public float totalTime;

	public GameObject[] enemies;

	public GameObject boss;
	public bool bossLevel;

	public float flashTimer;

	public Color colorStart;
	public Color colorEnd;

	public Color bossColorStart;
	public Color bossColorEnd;

	public Color visitedStart;
	public Color visitedEnd;

	public Color salvageStart;
	public Color salvageEnd;

	public Color currentLocationStart;
	public Color currentLocationEnd;

	public Texture2D cursorTexture;
	public Texture2D defaultTexture;

	// Use this for initialization
	void Start () {
		galaxyText = GameObject.FindWithTag("GalaxyName");
		gameManager = GameObject.FindWithTag("GameManager");
		gameObject.GetComponent<Button>().onClick.AddListener(selectedLocation);
	}
	
	// Update is called once per frame
	void Update () {
		if(currentLocation)
		{
			float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
			GetComponent<Image>().color = Color.Lerp(currentLocationStart, currentLocationEnd, lerp);
		}
		else if(visited)
		{
			float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
			GetComponent<Image>().color = Color.Lerp(visitedStart, visitedEnd, lerp);
		}
		else if(bossLevel)
		{
			float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
			GetComponent<Image>().color = Color.Lerp(bossColorStart, bossColorEnd, lerp);
		}
		else if(salvage)
		{
			float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
			GetComponent<Image>().color = Color.Lerp(salvageStart, salvageEnd, lerp);
		}
		else 
		{
			float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
			GetComponent<Image>().color = Color.Lerp(colorStart, colorEnd, lerp);
		}
	}

	void selectedLocation()
	{
		gameManager.GetComponent<GameManager>().changeLocation(gameObject);
		flashTimer = 0.2f;
		Cursor.SetCursor(defaultTexture, new Vector2(defaultTexture.width/2, defaultTexture.height/2), CursorMode.Auto);
		transform.localScale /= 1.2f;
		galaxyText.GetComponent<Text>().text = "";
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		flashTimer = 0.1f;
		Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2, cursorTexture.height/2), CursorMode.Auto);
		transform.localScale *= 1.2f;
		if(bossLevel)
		{
			galaxyText.GetComponent<Text>().text = locationName + "\n!!DANGER!!";
		}
		else if(salvage)
		{
			galaxyText.GetComponent<Text>().text = locationName + "\n[JUNK YARD]";
		}
		else
		{
			galaxyText.GetComponent<Text>().text = locationName;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		flashTimer = 0.2f;
		Cursor.SetCursor(defaultTexture, new Vector2(defaultTexture.width/2, defaultTexture.height/2), CursorMode.Auto);
		transform.localScale /= 1.2f;
		galaxyText.GetComponent<Text>().text = "";
	}
}

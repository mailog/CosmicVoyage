using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

	public GameObject[] bosses;
	public GameObject[] enemies;
	public List<LineRenderer> lines;

	public GameObject locationsCanvas;
	public GameObject location;
	public GameObject[,] locations;

	public int locationsX;
	public int locationsY;

	public Material lineMaterial;
	public Color lineColor;

	public Vector2 startLocation;
	public Vector2 currentLocation;

	private GameObject gameManager;

	private float fillTimer;
	private bool finished;

	public float spawnRateMin;
	public float spawnRateMax;

	public float totalTimeMin;
	public float totalTimeMax;
	// Use this for initialization
	public List<string> galaxyNames; 
	void Start () 
	{
		galaxyNames = new List <string> {"Lambda Acallaris", "Andromeda", "Comae Capella", "Centaurus Galaxy", "Boreas Nebula", "Seashell Nebula", "Rippled Cloud", "Spiral Star System", "OS-792", "CAY 68I", "Lyra Alatheia", "Lambda Centauri", "Orion Galaxy","Centaurus Cloud", "Serpent's Eye Galaxy", "Cat's Eye Nebula", "Eggshell Galaxy", "SF-028", "QVV 7H"};

		Debug.Log("Galaxy: " + galaxyNames.Count);
		gameManager = GameObject.FindWithTag("GameManager");
		gameObject.GetComponent<Image>().fillAmount = 0;
		fillTimer = 0;
		locations = new GameObject[5, 5];
		lines = new List<LineRenderer>();
		for(int i = 0; i < locations.GetLength(0); i++)
		{
			for(int j = 0; j < locations.GetLength(1); j++)
			{
				int enemyCount = Random.Range(2,enemies.Length+1);
				ArrayList tmpEnemies = new ArrayList(enemies);
				GameObject[] tmpSelection = new GameObject[enemyCount];
				do
				{
					int tmpNum = Random.Range(0, tmpEnemies.Count);
					tmpSelection[enemyCount-1] = (GameObject)tmpEnemies[tmpNum];
					tmpEnemies.RemoveAt(tmpNum);
					enemyCount--;
				}while(enemyCount>0);
				GameObject tmp = Instantiate(location, new Vector3(-4f + (2*i), -4f + (2*j), 0), Quaternion.identity);
				tmp.GetComponent<Location>().spawnRate = Random.Range(spawnRateMin,spawnRateMax);
				tmp.GetComponent<Location>().totalTime = Random.Range(totalTimeMin,totalTimeMax);
				tmp.name = "Location " + i + ", " + j;
				tmp.GetComponent<Location>().locationName = "Location " + i + ", " + j;
				tmp.GetComponent<Location>().enemies = tmpSelection;
				tmp.GetComponent<Location>().active = false;
				tmp.transform.SetParent(transform);
				locations[i,j] = tmp;
			}
		}
		locationsX = 5;
		locationsY = 5;
		startLocation = new Vector2(2,2);
		currentLocation = startLocation;
		gameManager.GetComponent<GameManager>().currLocation = locations[(int)startLocation.x,(int)startLocation.y];
		locations[(int)startLocation.x,(int)startLocation.y].GetComponent<Location>().currentLocation = true;
		locations[(int)startLocation.x,(int)startLocation.y].GetComponent<Location>().active = true;
		locations[(int)startLocation.x,(int)startLocation.y].GetComponent<Location>().visited = true;
		assignName(locations[(int)startLocation.x,(int)startLocation.y]);
		for(int i = 0; i < 2; i++)
		{
			createPath(chooseDestination(),0);
		}
		createPath(chooseDestination(),1);

		connectLocations();

		gameManager.GetComponent<GameManager>().locations = locations;
	}
	
	// Update is called once per frame
	void Update () {
		if(fillTimer < 1)
		{
			fillTimer += 0.8f * Time.deltaTime;
			GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, fillTimer);
		}
		else if(!finished)
		{
			GameObject currentLocation = gameManager.GetComponent<GameManager>().currLocation;

			for(int i = 0; i <locations.GetLength(0); i++)
			{
				for(int j = 0; j <locations.GetLength(1);j++)
				{
					if(locations[i,j].Equals(currentLocation) || locations[i,j].GetComponent<Location>().visited)
					{
						if(locations[i,j].Equals(currentLocation) )
						{	
							locations[i,j].GetComponent<Location>().currentLocation = true;
						}
						if(locations[i,j].GetComponent<Location>().active)
						{
							locations[i,j].SetActive(true);
							for(int k = 0; k < locations[i,j].GetComponent<Location>().connectedLocations.Length;k++)
							{
								locations[i,j].GetComponent<Location>().connectedLocations[k].SetActive(true);
								drawLine(locations[i,j].transform.position, locations[i,j].GetComponent<Location>().connectedLocations[k].transform.position, "From " + i + ", " + j + " #" + k);

							}
						}
					}
				}
			}
			finished = true;
		}
	}

	Vector2 chooseDestination()
	{
		int chooseDest = Random.Range(0,4);
		int chosenX = 0;
		int chosenY = 0;
		//Debug.Log(bossChoose);
		if(chooseDest == 0)
		{
			do
			{
				chosenX = 0;
				chosenY = Random.Range(0,locations.GetLength(1)-1);
			}while(locations[chosenX,chosenY].GetComponent<Location>().active);
		}
		else if(chooseDest == 1)
		{
			do
			{
				chosenX = Random.Range(0,locations.GetLength(0)-1);
				chosenY = 0;
			}while(locations[chosenX,chosenY].GetComponent<Location>().active);
		}
		else if(chooseDest == 2)
		{
			do
			{
				chosenX = locations.GetLength(0)-1;
				chosenY = Random.Range(0,locations.GetLength(1));
			}while(locations[chosenX,chosenY].GetComponent<Location>().active);
		}
		else if(chooseDest == 3)
		{
			do
			{
				chosenX = Random.Range(0,locations.GetLength(0));
				chosenY = locations.GetLength(0)-1;
			}while(locations[chosenX,chosenY].GetComponent<Location>().active);
		}
		return new Vector2(chosenX,chosenY);
	}

	void createPath(Vector2 target, int roomType)
	{
		Vector2 currentLocation = startLocation;
		if(target.x != currentLocation.x && target.y != currentLocation.y)
		{
			int firstStepDirection = Random.Range(0,3);
			int tmpStepx = 0;
			int tmpStepy = 0;

			if(target.x < currentLocation.x)
			{
				tmpStepx = -1;
			}
			else
			{
				tmpStepx = 1;
			}

			if(target.y < currentLocation.y)
			{
				tmpStepy = -1;
			}
			else
			{
				tmpStepy = 1;
			}

			if(firstStepDirection == 0)
			{
				//x
				currentLocation.x += tmpStepx;
			}
			else if(firstStepDirection == 1)
			{
				//diagonal
				currentLocation.x += tmpStepx;
				currentLocation.y += tmpStepy;
			}
			else if(firstStepDirection == 2)
			{
				//y
				currentLocation.y += tmpStepy;
			}
			locations[(int)currentLocation.x,(int)currentLocation.y].GetComponent<Location>().active = true;
			assignName(locations[(int)currentLocation.x,(int)currentLocation.y]);
		}
		//Debug.Log("Before x: " + currentLocation.x + "y: " + currentLocation.y);

		while(currentLocation.x != target.x || currentLocation.y != target.y)
		{
			int tmpStepx = 0;
			int tmpStepy = 0;
			if(target.x < currentLocation.x)
			{
				tmpStepx = -1;
			}
			else if(target.x > currentLocation.x)
			{
				tmpStepx = 1;
			}

			if(target.y < currentLocation.y)
			{
				tmpStepy = -1;
			}
			else if(target.y > currentLocation.y)
			{
				tmpStepy = 1;
			}
			currentLocation.x += tmpStepx;
			currentLocation.y += tmpStepy;
			locations[(int)currentLocation.x,(int)currentLocation.y].GetComponent<Location>().active = true;
			assignName(locations[(int)currentLocation.x,(int)currentLocation.y]);
			//Debug.Log("During x: " + currentLocation.x + "y: " + currentLocation.y);
		}

		//	Debug.Log("After x: " + currentLocation.x + "y: " + currentLocation.y);
		switch(roomType)
		{
			case 0:
				locations[(int)target.x,(int)target.y].GetComponent<Location>().salvage = true;
				break;
			case 1:
				int chosenBoss = Random.Range(0,bosses.Length);
				locations[(int)target.x,(int)target.y].GetComponent<Location>().bossLevel = true;
				locations[(int)target.x,(int)target.y].GetComponent<Location>().boss = (GameObject)bosses[chosenBoss];
				break;
			default:
				break;
		}
		locations[(int)target.x,(int)target.y].GetComponent<Location>().active = true;
		assignName(locations[(int)target.x,(int)target.y]);
	}

	void connectLocations()
	{
		for(int i = 0; i < locations.GetLength(0); i++)
		{
			for(int j = 0; j < locations.GetLength(1); j++)
			{
				if(locations[i,j].GetComponent<Location>().active)
				{
					List<GameObject> tmpAdj = new List<GameObject>();
					for(int x = -1; x < 2; x++)
					{
						for (int y = -1; y < 2; y++)
						{
							if((i+x >= 0 && j+y >= 0) && (i+x < locations.GetLength(0) && j+y < locations.GetLength(1)))
							{
								//Debug.Log("X: " + (i+x) + "Y: " + (j+y));
								if(locations[i+x,j+y].GetComponent<Location>().active && !locations[i+x,j+y].Equals(locations[i,j]))
								{
										tmpAdj.Add(locations[i+x,j+y]);
								}  
							}
						}
					}
					locations[i,j].GetComponent<Location>().connectedLocations = tmpAdj.ToArray();
				}
			}
		}
	}

	void drawLine(Vector3 start, Vector3 end, string name)
	{
		GameObject line = new GameObject();
		line.transform.position = start;
		line.AddComponent<LineRenderer>();
		LineRenderer lr = line.GetComponent<LineRenderer>();
		lr.material = lineMaterial;
		lr.startColor = lineColor;
		lr.endColor = lineColor;
		lr.startWidth = 0.1f;
		lr.endWidth = 0.1f;
		lr.SetPosition(0, start);
		lr.SetPosition(1,end);
		lr.transform.SetParent(transform);
		lr.name = name;
		lr.sortingLayerName = "UI";
		lr.sortingOrder = 13;
		lines.Add((LineRenderer)lr);
	}

	void assignName(GameObject tmpLoc)
	{
		int randName = Random.Range(0,galaxyNames.Count);
		tmpLoc.GetComponent<Location>().locationName = galaxyNames[randName];
		galaxyNames.RemoveAt(randName);
	}

	void OnEnable()
	{
		fillTimer = 0;
		finished = false;
		lines = new List<LineRenderer>();
		for(int i = 0; i <locationsX; i++)
		{
			for(int j = 0; j <locationsY;j++)
			{
				locations[i,j].GetComponent<Location>().currentLocation = false;
				locations[i,j].SetActive(false);
				Debug.Log(locations[i,j].activeSelf);
			}
		}
	}

	void OnDisable()
	{
		for(int i = 0; i < lines.Count; i++)
		{
			Destroy(lines[i].gameObject);
		}
		for(int i = 0; i <locationsX; i++)
		{
			for(int j = 0; j <locationsY;j++)
			{
				locations[i,j].GetComponent<Location>().currentLocation = false;
				locations[i,j].SetActive(false);
				Debug.Log(locations[i,j].activeSelf);
			}
		}
		GetComponent<Image>().fillAmount = 0;
		fillTimer = 0;
	}
}

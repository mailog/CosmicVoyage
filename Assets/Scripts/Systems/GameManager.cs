using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject levelCompleteUI;
	public GameObject upgradeCanvas;
	public GameObject fader;

	public AudioSource level1Music;
	public AudioSource bossMusic;

	public Slider progressBar;
	public Slider progressBar2;

	public bool upgrading;

	public GameObject topBar;
	public GameObject bottomBar;

	public GameObject player;

	public GameObject[,] locations;
	public GameObject[] enemies;

	public GameObject currLocation;
	public string currLocationName;

	public bool bossLevel;

	public GameObject boss;
	public GameObject BossUI;

	public Texture2D cursorTexture;

	public float spawnRate;
	public float totalTime;
	public float timer;

	private float spawnTimer;
	private bool bossSpawned = false;

	public int enemyCount;
	public bool bossDead;

	public bool levelComplete;

	public float delayTimer;
	public float delay;
	public bool delayDone;

	public bool selecting;
	public bool uiActive;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2, cursorTexture.height/2), CursorMode.Auto);
		enemyCount = 0;
		bossDead = false;
		delayTimer = 0;
		delay = 2f;
		selecting = true;
		uiActive = false;
		delayDone = false;
		level1Music.GetComponent<AudioSource>().Play(0);
		bossMusic.GetComponent<AudioSource>().Stop();
		levelCompleteUI.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(fader.GetComponent<FadeInOut>().fadeDone)
		{
			if(currLocation.GetComponent<Location>().visited && !player.GetComponent<Player>().pause)
			{
				levelComplete = true;
				BossUI.SetActive(false);
				levelCompleteUI.SetActive(true);
			}
			else if(currLocation.GetComponent<Location>().salvage && !upgrading)
			{
				Debug.Log("WHY?");
				player.GetComponent<Player>().levelUp();
				upgrading = true;
			}
			else if(currLocation.GetComponent<Location>().salvage  && !player.GetComponent<Player>().pause)
			{
				currLocation.GetComponent<Location>().visited = true;
				levelComplete = true;
				BossUI.SetActive(false);
				levelCompleteUI.SetActive(true);
			}
			else if(selecting && !uiActive && !player.GetComponent<Player>().pause)
			{
				levelCompleteUI.SetActive(true);
				uiActive = true;
			}
			else if(fader.activeSelf)
			{

			}
			else if(!player.GetComponent<Player>().pause)
			{
				bottomBar.SetActive(true);
				if(progressBar.value >= progressBar.maxValue)
				{
		 			topBar.SetActive(true);
				}
				else
				{
		 			topBar.SetActive(false);
		 		}
				timer += Time.deltaTime;
				progressBar.value = timer;
				progressBar2.value = timer;
				if(bossLevel)
				{
					if(bossDead && !delayDone)
					{
						if(delayTimer >= delay)
						{
							delayDone = true;	
							GameObject[] tmpPewpews = GameObject.FindGameObjectsWithTag("EnemyPewpew");
							for(int i = 0; i <= tmpPewpews.Length-1; i++)
							{
								Destroy(tmpPewpews[i]);
							}
						}
						else
						{
							delayTimer += Time.deltaTime;
						}
					}
					else if(delayDone && !upgrading)
					{
						Debug.Log("BOSS LEVELUP");
						player.GetComponent<Player>().levelUp();
						upgrading = true;
					}
					else if(delayDone && !levelComplete && !player.GetComponent<Player>().pause && !upgradeCanvas.activeSelf)
					{
						levelComplete = true;
						BossUI.SetActive(false);
						levelCompleteUI.SetActive(true);
						currLocation.GetComponent<Location>().visited = true;
						level1Music.GetComponent<AudioSource>().PlayDelayed(1);
						bossMusic.GetComponent<AudioSource>().Stop();	
					}
					else if(totalTime <= 0 && !bossSpawned && enemyCount <= 0)
					{
						spawnBoss(boss);
						bossSpawned = true;
					}
					else if(!bossSpawned && totalTime > 0)
					{
						spawn();
						totalTime -= Time.deltaTime;
					}
				}
				else
				{
					if(enemyCount <= 0 && totalTime<=0 && !selecting && !delayDone)
					{
						if(delayTimer >= delay)
						{
							delayDone = true;	
							GameObject[] tmpPewpews = GameObject.FindGameObjectsWithTag("EnemyPewpew");
							for(int i = 0; i <= tmpPewpews.Length-1; i++)
							{
								Destroy(tmpPewpews[i]);
							}
						}
						else
						{
							delayTimer += Time.deltaTime;
						}
					}
					/*
					else if(delayDone && !upgrading)
					{
						GameObject[] tmpPewpews = GameObject.FindGameObjectsWithTag("EnemyPewpew");
						for(int i = 0; i <= tmpPewpews.Length-1; i++)
						{
							Destroy(tmpPewpews[i]);
						}
						player.GetComponent<Player>().levelUp();
						upgrading = true;
					}*/
					else if(enemyCount <= 0 && totalTime<=0 && !selecting && !player.GetComponent<Player>().pause && !upgradeCanvas.activeSelf)
					{
						levelComplete = true;
						levelCompleteUI.SetActive(true);
						currLocation.GetComponent<Location>().visited = true;
					}
					else if(!selecting && totalTime > 0)
					{
						spawn();
						totalTime -= Time.deltaTime;
					}
				}
			}
		}
	}

	void spawn()
	{
		if(spawnTimer>=spawnRate)
		{
			spawnEnemy(enemies[(int)Random.Range(0,enemies.Length)], Random.Range(-3f,3f));
			incEnemy();
			spawnTimer = 0;
		}
		else
		{
			spawnTimer += Time.deltaTime;
		}
	}
		
	void spawnEnemy(GameObject enemy, float xCoord)
	{
		Instantiate(enemy, new Vector2(xCoord, 21f), Quaternion.identity);
	}

	void spawnBoss(GameObject boss)
	{
		Instantiate(boss, new Vector3(0, 24f, 0f), Quaternion.identity);
		BossUI.SetActive(true);
		level1Music.GetComponent<AudioSource>().Stop();
		bossMusic.GetComponent<AudioSource>().PlayDelayed(1);
	}

	public void incEnemy()
	{
		enemyCount++;
	}

	public void decEnemy()
	{
		enemyCount--;
	}

	public void changeLocation(GameObject location)
	{
		currLocation = location;
		currLocationName = currLocation.GetComponent<Location>().locationName;
		spawnRate = location.GetComponent<Location>().spawnRate;
		spawnTimer = 0;
		totalTime = location.GetComponent<Location>().totalTime;
		enemies = location.GetComponent<Location>().enemies;
		bossLevel = location.GetComponent<Location>().bossLevel;
		if(bossLevel)
		{
			boss = location.GetComponent<Location>().boss;
			Debug.Log("BOSS");
		}
		selecting = false;
		levelCompleteUI.SetActive(false);
		uiActive = false;
		delayTimer = 0;
		delayDone = false;
		levelComplete = false;
		timer = 0;
		progressBar.maxValue = totalTime;
		progressBar2.maxValue = totalTime;
		progressBar.value = 0;
		progressBar2.value = 0;
		upgrading = false;
		fader.SetActive(true);
		topBar.SetActive(false);
		bottomBar.SetActive(false);
	}
}

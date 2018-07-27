using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Peacemaker : MonoBehaviour {

	private GameObject gameManager;

	public float health;
	public GameObject player;
	public float speed;

	public Slider healthBar;
	public Text name;

	public float fireTimer;
	public float fireRate;
	public float damage;
	public float bulletSpeed;

	public GameObject pewpew;
	public GameObject deathAnimation;

	public int currPattern;
	public float patternTimer;
	public float patternCycle;
	public bool patternDone;

	public float x;
	public float t;
	public bool inc;

	public bool bossEntrance;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindWithTag("GameManager");
		healthBar = GameObject.FindWithTag("Boss Health").GetComponent<Slider>();
		healthBar.maxValue = health;
		name = GameObject.FindWithTag("Boss Text").GetComponent<Text>();
		name.fontSize = 1;
		name.text = "Peacemaker";
		player = GameObject.FindWithTag("Player");
		t = 0.5f;
		bossEntrance = true;
	}
	
	// Update is called once per frame
	void Update () {
	if(bossEntrance)
	{
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 13, transform.position.z), 5f * Time.deltaTime);
		if(transform.position == new Vector3(0,13,transform.position.z))
		{
			bossEntrance = false;
		}
	}
	else
	{
		x = Mathf.Lerp(-5f, 5f, t); 
		transform.position = new Vector3(x, transform.position.y, 0);
		if(t >= 1)
		{
			inc = false;
		}
		if(t <= 0)
		{
			inc = true;
		}
		if(inc)
		{
			t += (0.25f * Time.deltaTime);	
		}
		else
		{
			t -= (0.25f * Time.deltaTime);
		}
	}
		healthBar.value = health;

		Vector3 vectorToTarget = player.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

		if(patternDone)
		{
			chooseAttackPattern();
		}
		switch(currPattern)
		{
			case 0: attackPattern2();
			break;
			case 1: attackPattern1();
			break;
			case 2: attackPattern3();
			break;
			default: attackPattern2();
			break;
		}
			
		if(health<=0)
		{
			gameManager.GetComponent<GameManager>().bossDead = true;
			Destroy(gameObject);
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
		}
	}

	void chooseAttackPattern()
	{
		currPattern = (int)Mathf.Round(Random.Range(0f, 3f));
		patternTimer = 0;
		switch(currPattern)
		{
			case 0: fireRate = 1f;
					patternCycle = 4;
			break;
			case 1: fireRate = 2f;
					patternCycle = 4;
			break;
			case 2: fireRate = 1f;
					patternCycle = 4;
			break;
			default: fireRate = 1f;
					patternCycle = 4;
			break;
		}
		patternDone = false;
	}

	void attackPattern1()
	{
		if(patternTimer >= patternCycle)	
		{
			patternDone = true;
		}
		else
		{
			patternTimer += Time.deltaTime;
			if(fireTimer >= fireRate)
			{	
				bulletSpeed = 20f;
				/*GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet1.GetComponent<PeacemakerPewpew>().stop1 = new Vector3(player.transform.position.x + 2, player.transform.position.y + 2, player.transform.position.z); 
				bullet1.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet1.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet1.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet2 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet2.GetComponent<PeacemakerPewpew>().stop1 = new Vector3(player.transform.position.x - 2, player.transform.position.y + 2, player.transform.position.z); 
				bullet2.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet2.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet2.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet3 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet3.GetComponent<PeacemakerPewpew>().stop1 = new Vector3(player.transform.position.x - 2, player.transform.position.y - 2, player.transform.position.z); 
				bullet3.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet3.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet3.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;*/

				GameObject bullet4 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet4.GetComponent<PeacemakerPewpew>().stop1 = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z); 
				bullet4.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet4.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet4.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet5 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet5.GetComponent<PeacemakerPewpew>().stop1 = new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z); 
				bullet5.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet5.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet5.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet6 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet6.GetComponent<PeacemakerPewpew>().stop1 = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z); 
				bullet6.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet6.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet6.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet7 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet7.GetComponent<PeacemakerPewpew>().stop1 = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z); 
				bullet7.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet7.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet7.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet8 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet8.GetComponent<PeacemakerPewpew>().stop1 = new Vector3(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z); 
				bullet8.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet8.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet8.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				fireTimer = 0;

				}
			else
			{
				fireTimer += Time.deltaTime;
			}
		}
	}

	void attackPattern2()
	{
		if(patternTimer >= patternCycle)	
		{
			patternDone = true;
		}
		else
		{
			patternTimer += Time.deltaTime;
			if(fireTimer >= fireRate)
			{	
				//Debug.Log(transform.position +transform.up+ "||" +(transform.position-transform.up));
				bulletSpeed = 20f;
				GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet1.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (3 * transform.up);
				bullet1.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet1.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet1.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet2 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet2.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (6* transform.up);
				bullet2.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet2.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet2.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet3 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet3.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (9* transform.up);
				bullet3.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet3.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet3.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet4 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet4.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (12* transform.up);
				bullet4.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet4.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet4.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet5 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet5.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (15* transform.up);
				bullet5.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet5.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet5.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet6 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet6.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (18* transform.up);
				bullet6.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet6.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet6.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet7 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet7.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (21* transform.up);
				bullet7.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet7.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet7.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				fireTimer = 0;
			}
			else
			{
				fireTimer += Time.deltaTime;
			}
		}
	}

	void attackPattern3()
	{
		if(patternTimer >= patternCycle)	
		{
			patternDone = true;
		}
		else
		{
			patternTimer += Time.deltaTime;
			if(fireTimer >= fireRate)
			{	
				bulletSpeed = 20f;		
				GameObject bullet5 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet5.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (1 * transform.right);
				bullet5.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet5.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet5.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet6 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet6.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (3 * transform.right);
				bullet6.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet6.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet6.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet1.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (5 * transform.right);
				bullet1.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet1.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet1.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet2 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet2.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (7 * transform.right);
				bullet2.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet2.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet2.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				/*GameObject bullet11 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet11.GetComponent<PeacemakerPewpew>().stop1 = transform.position - (9 * transform.right);
				bullet11.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet11.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet11.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;*/

				GameObject bullet7 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet7.GetComponent<PeacemakerPewpew>().stop1 = transform.position + (1 * transform.right);
				bullet7.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet7.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet7.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet8 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet8.GetComponent<PeacemakerPewpew>().stop1 = transform.position + (3 * transform.right);
				bullet8.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet8.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet8.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet9 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet9.GetComponent<PeacemakerPewpew>().stop1 = transform.position + (5 * transform.right);
				bullet9.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet9.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet9.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				GameObject bullet10 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet10.GetComponent<PeacemakerPewpew>().stop1 = transform.position + (7 * transform.right);
				bullet10.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet10.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet10.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;

				/*GameObject bullet12 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet12.GetComponent<PeacemakerPewpew>().stop1 = transform.position + (9 * transform.right);
				bullet12.GetComponent<PeacemakerPewpew>().damage = damage;
				bullet12.GetComponent<PeacemakerPewpew>().bulletSpeed = bulletSpeed;
				bullet12.GetComponent<PeacemakerPewpew>().pauseTimer = 3f;*/


				fireTimer = 0;
				}
			else
			{
				fireTimer += Time.deltaTime;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("EnemyPewpew") || other.gameObject.tag.Equals("Companion"))
		{
			//	Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),gameObject.GetComponent<Collider2D>());
		}
		if(other.gameObject.tag.Equals("PlayerPewpew"))
		{
			health -= other.gameObject.GetComponent<Pewpew>().damage;
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		else
		{
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("EnemyPewpew") || other.gameObject.tag.Equals("Companion"))
		{
			//	Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),gameObject.GetComponent<Collider2D>());
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("GameManager"))
		{
			gameManager.GetComponent<GameManager>().bossDead = true;
			Destroy(gameObject);
		}
	}
}

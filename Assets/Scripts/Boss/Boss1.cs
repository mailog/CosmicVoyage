using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour {

	private GameObject gameManager;
	public Slider healthBar;
	public float health;

	public Text name;

	public GameObject pewpew;
	public float xVel;
	public float yVel;

	public float fireRate = 0.2f;
	private float fireTimer;
	public float turnRate;
	private float turnTimer;
	private int currPattern;
	private bool patternDone;
	private float patternCycle;
	private float patternTimer;
	private bool top = true;
	private float x;
	private float y;
	private float t = 0;

	private bool bossEntrance = true;
	private Vector3 startPosition;

	private Vector3 topPosition = new Vector3(0,19,0);

	private float bulletSpeed;

	public GameObject player;

	private bool inc;
	// Use this for initialization
	void Start () {
		//healthBar.maxValue = health;
		gameManager = GameObject.FindWithTag("GameManager");
		healthBar = GameObject.FindWithTag("Boss Health").GetComponent<Slider>();
		healthBar.maxValue = health;
		name = GameObject.FindWithTag("Boss Text").GetComponent<Text>();
		name.fontSize = 1;
		name.text = "Gatekeeper";
		startPosition = transform.position;
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = health;

		Vector3 vectorToTarget = player.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

		gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;

		if(health<=0)
		{
			gameManager.GetComponent<GameManager>().bossDead = true;
			Destroy(gameObject);
		}
		if(bossEntrance)
		{
			transform.position = Vector3.Lerp(startPosition, topPosition, t);
			t += 0.4f * Time.deltaTime;
			if(fireTimer >= fireRate)
			{
				GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet1.transform.Rotate(0,0,180f);
				bullet1.GetComponent<Pewpew>().bulletSpeed = Random.Range(25f,15f);
				GameObject bullet2 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet2.transform.Rotate(0,0,185f);
				bullet2.GetComponent<Pewpew>().bulletSpeed = Random.Range(25f,15f);
				GameObject bullet4 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet4.transform.Rotate(0,0,175f);
				bullet4.GetComponent<Pewpew>().bulletSpeed = Random.Range(25f,15f);
				fireTimer = 0;	
			}
			else
			{
				fireTimer += Time.deltaTime;
			}
			if(t>=1)
			{
				transform.position = topPosition;
				xVel = 0;
				yVel = 0;
				t = 0;
				bossEntrance = false;
			}
		}
		else
		{
			if(patternDone)
			{
				chooseAttackPattern();
			}
			switch(currPattern)
			{
				case 0: attackPattern1();
				break;
				case 1: attackPattern2();
				break;
				case 2: attackPattern3();
				break;
				default: attackPattern1();
				break;
			}
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("EnemyPewpew"))
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

	void chooseAttackPattern()
	{
		currPattern = (int)Mathf.Round(Random.Range(0f, 2f));
		patternTimer = 0;
		switch(currPattern)
		{
			case 0: fireRate = 0.5f;
					patternCycle = 5;
			break;
			case 1: fireRate = 0.2f;
					patternCycle = 4;
			break;
			case 2: fireRate = 0.5f;
					patternCycle = 8;
					t = 0.5f;
			break;
			default: fireRate = 0.25f;
					patternCycle = 5;
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
			x = Mathf.Lerp(-10f, 10f, t);
			if(fireTimer >= fireRate)
			{
				GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet1.transform.Rotate(0,0,200f - x);
				bullet1.GetComponent<Pewpew>().bulletSpeed = 15f;
				GameObject bullet2 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet2.transform.Rotate(0,0,160f + x);
				bullet2.GetComponent<Pewpew>().bulletSpeed = 15f;
				GameObject bullet3 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet3.transform.Rotate(0,0,180f + x);
				bullet3.GetComponent<Pewpew>().bulletSpeed = 15f;
				GameObject bullet4 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet4.transform.Rotate(0,0,180f - x);
				bullet4.GetComponent<Pewpew>().bulletSpeed = 15f;
				fireTimer = 0;	
			}
			else
			{
				fireTimer += Time.deltaTime;
			}
			if(t <=0)
			{
				inc = true;
			}
			if(t >= 1)
			{
				inc = false;
			}
			if(inc)
			{
				t += 0.5f * Time.deltaTime;
			}
			else
			{
				t -= 0.5f * Time.deltaTime;
			}
			patternTimer += Time.deltaTime;
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
			if(fireTimer >= fireRate)
			{
				GameObject temp = Instantiate(pewpew, transform.position, transform.rotation);
				temp.transform.Rotate(0,0,Random.Range(250f,110f));
				temp.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				GameObject temp2 = Instantiate(pewpew, transform.position, transform.rotation);
				temp2.transform.Rotate(0,0,Random.Range(250f,110f));
				temp2.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				GameObject temp3 = Instantiate(pewpew, transform.position, transform.rotation);
				temp3.transform.Rotate(0,0,Random.Range(250f,110f));
				temp3.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				GameObject temp4 = Instantiate(pewpew, transform.position, transform.rotation);
				temp4.transform.Rotate(0,0,Random.Range(250f,110f));
				temp4.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				GameObject temp5 = Instantiate(pewpew, transform.position, transform.rotation);
				temp5.transform.Rotate(0,0,Random.Range(250f,110f));
				temp5.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				GameObject temp6 = Instantiate(pewpew, transform.position, transform.rotation);
				temp6.transform.Rotate(0,0,Random.Range(250f,110f));
				temp6.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				GameObject temp7 = Instantiate(pewpew, transform.position, transform.rotation);
				temp7.transform.Rotate(0,0,Random.Range(250f,110f));
				temp7.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				GameObject temp8 = Instantiate(pewpew, transform.position, transform.rotation);
				temp8.transform.Rotate(0,0,Random.Range(250f,110f));
				temp8.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				GameObject temp9 = Instantiate(pewpew, transform.position, transform.rotation);
				temp9.transform.Rotate(0,0,Random.Range(250f,110f));
				temp9.GetComponent<Pewpew>().bulletSpeed = Random.Range(5f,10f);
				fireTimer = 0;	
			}
			else
			{
				fireTimer += Time.deltaTime;
			}
			patternTimer += Time.deltaTime;
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
			if(fireTimer >= fireRate)
			{
				GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet1.transform.Rotate(0,0,180f);
				bullet1.GetComponent<Pewpew>().bulletSpeed = Random.Range(25f,15f);
				GameObject bullet2 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet2.transform.Rotate(0,0,185f);
				bullet2.GetComponent<Pewpew>().bulletSpeed = Random.Range(25f,15f);
				GameObject bullet4 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet4.transform.Rotate(0,0,175f);
				bullet4.GetComponent<Pewpew>().bulletSpeed = Random.Range(25f,15f);
				/*GameObject bullet5 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet5.transform.Rotate(0,0,165f);
				bullet5.GetComponent<Pewpew>().bulletSpeed = Random.Range(15f,10f);*/
				fireTimer = 0;	
			}
			else
			{
				fireTimer += Time.deltaTime;
			}

			patternTimer += Time.deltaTime;
		}
	}
}

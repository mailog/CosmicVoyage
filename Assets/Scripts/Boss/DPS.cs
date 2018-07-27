using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPS : MonoBehaviour {

	public GameObject player;
	public GameObject deathAnimation;
	public GameObject pewpew;

	public float health;
	public Slider healthBar;

	public float fireTimer;
	public float fireRate;

	public int currPattern;
	public bool patternDone;
	public float patternCycle;
	public float patternTimer;

	public float damage;
	public float bulletSpeed;
	public float ogHealth;

	public float buff;

	public float healTimer;
	public float healCounter;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		healTimer = 2f;
		healCounter = 0;
		ogHealth = health;
		buff = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vectorToTarget = player.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
		fireRate = 3f * buff;
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
			
		if(health<=0)
		{
			Destroy(gameObject);
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
		}
	}

	void chooseAttackPattern()
	{
		currPattern = (int)Mathf.Round(Random.Range(0f, 3f));
		patternTimer = 0;
		currPattern = 0;
		switch(currPattern)
		{
			case 0: fireRate = 0.25f * buff;
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

	public void healing(float heal)
	{
		if(healCounter >= healTimer)
		{
			if((health+heal) >= ogHealth)
			{
				health = ogHealth;
			}
			else
			{
				health += heal;
			}
			healCounter = 0;
		}
		else
		{
			healCounter += Time.deltaTime;
		}	
	}

	public void buffed(float buff)
	{
		this.buff = buff;
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
				bulletSpeed = 60f;
				GameObject temp = Instantiate(pewpew, transform.position, transform.rotation);
				temp.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;

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
				bulletSpeed = 20f;
				GameObject bullet1 = Instantiate(pewpew, (transform.position + (2 * transform.up) + (0* transform.right)), transform.rotation); 
				bullet1.GetComponent<Pewpew>().damage = damage;
				bullet1.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;

				GameObject bullet2 = Instantiate(pewpew, (transform.position + (1 * transform.up) + (1* transform.right)), transform.rotation); 
				bullet2.GetComponent<Pewpew>().damage = damage;
				bullet2.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;

				GameObject bullet3 = Instantiate(pewpew, (transform.position + (1 * transform.up) + (-1* transform.right)), transform.rotation); 
				bullet3.GetComponent<Pewpew>().damage = damage;
				bullet3.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;

				GameObject bullet4 = Instantiate(pewpew, (transform.position + (0 * transform.up) + (2* transform.right)), transform.rotation); 
				bullet4.GetComponent<Pewpew>().damage = damage;
				bullet4.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;

				GameObject bullet5 = Instantiate(pewpew, (transform.position + (0 * transform.up) + (-2* transform.right)), transform.rotation); 
				bullet5.GetComponent<Pewpew>().damage = damage;
				bullet5.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;

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
				GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation); 
				bullet1.GetComponent<Pewpew>().damage = damage;
				bullet1.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;

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

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("GameManager"))
		{
			Destroy(gameObject);
		}
	}
}

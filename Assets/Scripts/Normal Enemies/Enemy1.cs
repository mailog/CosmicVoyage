using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour {

	private GameObject gameManager;

	public Slider healthBar;
	public float health;

	public GameObject player;
	public GameObject pewpew;

	public float xVel;
	public float yVel;

	public float bulletSpeed;
	public float damage;

	public float fireRate;
	private float fireTimer;
	public float turnRate;
	private float turnTimer;

	public GameObject deathAnimation;
	// Use this for initialization
	void Start () {
		healthBar.maxValue = health;
		player = GameObject.FindWithTag("Player");
		if(Random.Range(-0.5f,0.5f) < 0)
		{
			xVel = -xVel;		
		}
	}
	
	// Update is called once per frame
	void Update () {
		gameManager = GameObject.FindWithTag("GameManager");

		healthBar.value = health;

		Vector3 vectorToTarget = player.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

		if(transform.position.y <= Random.Range(10f,13f))
        {
        	yVel = 0;
        }

		if(health<=0)
		{
			player.GetComponent<Player>().exp += 100;
			gameManager.GetComponent<GameManager>().decEnemy();
			Destroy(gameObject);
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
		}
		if(fireTimer >= fireRate)
		{
			GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
			bullet1.transform.Rotate(0,0,200f);
			GameObject bullet2 = Instantiate(pewpew, transform.position, transform.rotation);
			bullet2.transform.Rotate(0,0,190f);
			GameObject bullet3 = Instantiate(pewpew, transform.position, transform.rotation);
			bullet3.transform.Rotate(0,0,180f);
			GameObject bullet4 = Instantiate(pewpew, transform.position, transform.rotation);
			bullet4.transform.Rotate(0,0,170f);
			GameObject bullet5 = Instantiate(pewpew, transform.position, transform.rotation);
			bullet5.transform.Rotate(0,0,160f);

			bullet1.GetComponent<Pewpew>().damage = damage;
			bullet1.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			bullet2.GetComponent<Pewpew>().damage = damage;
			bullet2.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			bullet3.GetComponent<Pewpew>().damage = damage;
			bullet3.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			bullet4.GetComponent<Pewpew>().damage = damage;
			bullet4.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			bullet5.GetComponent<Pewpew>().damage = damage;
			bullet5.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			fireTimer = 0;	
		}
		else
		{
			fireTimer += Time.deltaTime;
		}
		if(turnTimer >= turnRate)
		{
			xVel = -xVel;
			turnTimer = 0;
		}
		else
		{
			turnTimer += Time.deltaTime;
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

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("GameManager"))
		{
			gameManager.GetComponent<GameManager>().decEnemy();
			Destroy(gameObject);
		}
	}
}

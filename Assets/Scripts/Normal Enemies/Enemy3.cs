using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy3 : MonoBehaviour {

	private GameObject gameManager;

	public float health;
	public Slider healthBar;

	public GameObject pewpew;
	public GameObject player;

	public float xVel;
	public float yVel;

	public float fireRate;
	private float fireTimer;
	public float turnRate;
	private float turnTimer;

	public GameObject deathAnimation;
	// Use this for initialization
	void Start () {
		healthBar.maxValue = health;
		gameManager = GameObject.FindWithTag("GameManager");
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = health;
		if(health<=0)
		{
			gameManager.GetComponent<GameManager>().decEnemy();
			player.GetComponent<Player>().exp += 100;
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		if(fireTimer >= fireRate)
		{
			float bulletSpeed = 10f;
			GameObject bullet1 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet1.transform.Rotate(0,0,20f);
			bullet1.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			GameObject bullet2 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet2.transform.Rotate(0,0,60f);
			bullet2.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			GameObject bullet3 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet3.transform.Rotate(0,0,100f);
			bullet3.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			GameObject bullet4 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet4.transform.Rotate(0,0,140f);
			bullet4.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			GameObject bullet5 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet5.transform.Rotate(0,0,180f);
			bullet5.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			GameObject bullet6 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet6.transform.Rotate(0,0,220f);
			bullet6.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			GameObject bullet7 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet7.transform.Rotate(0,0,260f);
			bullet7.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			GameObject bullet8 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet8.transform.Rotate(0,0,300f);
			bullet8.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
			GameObject bullet9 = Instantiate(pewpew, transform.position, Quaternion.identity);
			bullet9.transform.Rotate(0,0,340f);
			bullet9.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;
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

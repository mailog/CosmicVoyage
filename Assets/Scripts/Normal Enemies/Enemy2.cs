using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour {

	private GameObject gameManager;

	public GameObject player;
	public float health;
	public Slider healthBar;

	public GameObject pewpew;
	public float xVel;
	public float yVel;

	public float damage;
	public float bulletSpeed;

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
		if(Random.Range(-0.5f,0.5f) < 0)
		{
			xVel = -xVel;		
		}
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = health; 
		Vector3 vectorToTarget = player.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

        if(transform.position.y <= Random.Range(12f,14f))
        {
        	yVel = 0;
        }

		if(health<=0)
		{
			player.GetComponent<Player>().exp += 100;
			gameManager.GetComponent<GameManager>().decEnemy();
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		if(fireTimer >= fireRate)
		{	
			GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
			bullet1.transform.Rotate(0,0,180f);

			bullet1.GetComponent<Pewpew>().damage = damage;
			bullet1.GetComponent<Pewpew>().bulletSpeed = bulletSpeed;

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

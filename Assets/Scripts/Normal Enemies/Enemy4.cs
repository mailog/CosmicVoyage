using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour {

	private GameObject gameManager;

	public GameObject player;
	public float speed;

	public GameObject deathAnimation;
	public float health;

	public float damage;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindWithTag("GameManager");
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);


		Vector3 vectorToTarget = player.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

		if(health<=0)
		{
			player.GetComponent<Player>().exp += 100;
			gameManager.GetComponent<GameManager>().decEnemy();
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.tag.Equals("Shield"))
		{
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
			gameManager.GetComponent<GameManager>().decEnemy();
			Destroy(gameObject);
		}
		if(other.gameObject.tag.Equals("Player"))
		{
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
			gameManager.GetComponent<GameManager>().decEnemy();
			other.gameObject.GetComponent<Player>().takeDamage(damage);
			Destroy(gameObject);
		}
		if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("EnemyPewpew"))
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

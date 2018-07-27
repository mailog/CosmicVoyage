using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pewpew : MonoBehaviour {

	public float lifeTime;
	public float lifeTimer;
	public float damage;
	public Vector2 velocity;

	public AudioSource explosion;

	public ParticleSystem collide;
	public float bulletSpeed;

	// Use this for initialization
	void Start () {
		lifeTime = 10;
		gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		collide.transform.localScale = transform.localScale;
		if(lifeTimer>=lifeTime)
		{
			Destroy(gameObject);
		}
		lifeTimer += Time.deltaTime;
		if(gameObject.tag.Equals("EnemyPewpew"))
		{
			gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
			bulletSpeed = Mathf.Lerp(bulletSpeed, 10, lifeTimer/(lifeTime*5f));
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(gameObject.tag.Equals("EnemyPewpew"))
		{
			if(other.gameObject.tag.Equals("GameManager"))
			{
				Destroy(gameObject);
			}
			else if(other.gameObject.tag.Equals("Shield"))
			{
				Instantiate(explosion, transform.position, Quaternion.identity);
				Instantiate(collide, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
			else if(other.gameObject.tag.Equals("GameManager") || other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("PlayerPewpew") || other.gameObject.tag.Equals("EnemyPewpew") || other.gameObject.tag.Equals("Companion"))
				Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),gameObject.GetComponent<Collider2D>());
			else
			{
				Instantiate(explosion, transform.position, Quaternion.identity);
				Instantiate(collide, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
		else{
			if(other.gameObject.tag.Equals("GameManager"))
			{
				Destroy(gameObject);
			}
			else if(other.gameObject.tag.Equals("Shield") || other.gameObject.tag.Equals("GameManager") || other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("PlayerPewpew") || other.gameObject.tag.Equals("EnemyPewpew") || other.gameObject.tag.Equals("Companion"))
				Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),gameObject.GetComponent<Collider2D>());
			else
			{
				Instantiate(explosion, transform.position, Quaternion.identity);
				Instantiate(collide, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),gameObject.GetComponent<Collider2D>());
	}

	void OnCollisionExit2D(Collision2D other)
	{
		Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),gameObject.GetComponent<Collider2D>());
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("GameManager"))
		{
			Destroy(gameObject);
		}
	}
}

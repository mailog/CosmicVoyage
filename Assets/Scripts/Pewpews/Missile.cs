using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public float lifeTime;
	public float lifeTimer;
	public float damage;
	public Vector2 velocity;

	public AudioSource explosion;

	public GameObject collide;
	public float bulletSpeed;

	public float elapsedTime;

	public Vector3 startPos;
	public Vector3 target;

	// Use this for initialization
	void Start () {
		lifeTime = 10;
		elapsedTime = 0;
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(lifeTimer>=lifeTime)
		{
			Destroy(gameObject);
		}
		lifeTimer += Time.deltaTime;

		elapsedTime += 25f * Time.deltaTime;

		transform.position = Vector2.MoveTowards(startPos, target, elapsedTime);

		//Debug.Log(target);
		if(transform.position.x == target.x && transform.position.y == target.y)
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			Instantiate(collide, transform.position, Quaternion.identity);
			Destroy(gameObject);
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

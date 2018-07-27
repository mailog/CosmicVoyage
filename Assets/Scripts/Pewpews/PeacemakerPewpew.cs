using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeacemakerPewpew : MonoBehaviour {

	public float lifeTime;
	public float lifeTimer;
	public float damage;

	public AudioSource explosion;

	public Vector3 stop1;

	public Vector3 player_pos;

	public ParticleSystem collide;
	public float bulletSpeed;

	public bool stage1;

	public float pauseTotal;
	public float pauseTimer;

	public Color startColor;
	public Color endColor;

	// Use this for initialization
	void Start () {
		lifeTime = 10;
		stage1 = true;
		pauseTotal = pauseTimer;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().color = Color.Lerp(endColor, startColor, pauseTimer/pauseTotal);
		if(lifeTimer>=lifeTime)
		{
			Destroy(gameObject);
		}
		lifeTimer += Time.deltaTime;
		if(stage1)
		{
			if(transform.position == stop1 && pauseTimer <= 0)
			{
				player_pos = GameObject.FindWithTag("Player").transform.position;
				Vector3 vectorToTarget = player_pos - transform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90+Random.Range(-10,10)));
				GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
				gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
				stage1 = false;
			}
			else if(transform.position == stop1)
			{
				pauseTimer -= Time.deltaTime;
			}
			else{
				transform.position = Vector3.MoveTowards(transform.position, stop1, bulletSpeed * Time.deltaTime);

			}
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
			else if(other.gameObject.tag.Equals("GameManager") || other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("PlayerPewpew") || other.gameObject.tag.Equals("EnemyPewpew"))
				Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),gameObject.GetComponent<Collider2D>());
			else
			{	
				other.gameObject.GetComponent<Player>().takeDamage(damage);
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
			else if(other.gameObject.tag.Equals("Shield") || other.gameObject.tag.Equals("GameManager") || other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("PlayerPewpew") || other.gameObject.tag.Equals("EnemyPewpew"))
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

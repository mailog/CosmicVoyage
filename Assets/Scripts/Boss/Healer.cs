using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healer : MonoBehaviour {

	public GameObject target;
	public GameObject target2;
	public GameObject deathAnimation;

	public GameObject line2;

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
	public LineRenderer lineDraw;
	public LineRenderer lineDraw2;

	public bool pattern;

	public bool inc;
	public float t;

	public float heal;
	public float buff = 1f;

	// Use this for initialization
	void Start () {
		//switchTarget();
		lineDraw = gameObject.GetComponent<LineRenderer>();
		lineDraw2 = line2.GetComponent<LineRenderer>();
		target = GameObject.Find("DPS");
		target2 = GameObject.Find("Tank");
		patternCycle = 5;
		t = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector3 vectorToTarget = target.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));*/

		if(patternDone)
		{
			//switchTarget();
			pattern = !pattern;
			patternDone = false;
		}
		else
		{
			if(patternTimer >= patternCycle)	
			{
				patternDone = true;
				if(!target.Equals(null))
					target.GetComponent<DPS>().buffed(1);
				if(!target2.Equals(null))
					target2.GetComponent<Tank>().buffed(1);
				patternTimer = 0;
			}
			else
			{
				patternTimer += Time.deltaTime;
				beams();
			}
		}

		if(health<=0)
		{
			if(!target.Equals(null))
				target.GetComponent<DPS>().buffed(1f);
			if(!target2.Equals(null))
				target2.GetComponent<Tank>().buffed(1f);
			Destroy(gameObject);
			Instantiate(deathAnimation, transform.position, Quaternion.identity);
		}
	}

	void beams()
	{
		if(pattern)
		{
			if(!target.Equals(null))
			{
				target.GetComponent<DPS>().healing(heal);
				lineDraw.startColor = Color.white;
				lineDraw.endColor = Color.green;
				lineDraw.SetPosition(0, transform.position);
				lineDraw.SetPosition(1, target.transform.position);
			}
			else
			{
				lineDraw.enabled = false;
			}
			if(!target2.Equals(null))
			{
				target2.GetComponent<Tank>().buffed(buff);
				lineDraw2.startColor = Color.red;
				lineDraw2.endColor = Color.blue;
				lineDraw2.SetPosition(0, transform.position);
				lineDraw2.SetPosition(1, target2.transform.position);
			}
			else
			{
				lineDraw2.enabled = false;
			}
		}
		else
		{
			if(!target.Equals(null))
			{
				target.GetComponent<DPS>().buffed(buff);
				lineDraw.startColor = Color.red;
				lineDraw.endColor = Color.blue;
				lineDraw.SetPosition(0, transform.position);
				lineDraw.SetPosition(1, target.transform.position);
			}
			else
			{
				lineDraw.enabled = false;
			}
			if(!target2.Equals(null))
			{
				target2.GetComponent<Tank>().healing(heal);
				lineDraw2.startColor = Color.white;
				lineDraw2.endColor = Color.green;
				lineDraw2.SetPosition(0, transform.position);
				lineDraw2.SetPosition(1, target2.transform.position);
			}
			else
			{
				lineDraw2.enabled = false;
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

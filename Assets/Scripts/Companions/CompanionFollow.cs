using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollow : MonoBehaviour {

	public GameObject follow;
	public GameObject target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, follow.transform.position) >= 3)
		{
			transform.position = Vector3.MoveTowards(transform.position, follow.transform.position, 10* Time.deltaTime);
			if(target == null)
			{
				Vector3 vectorToTarget = follow.transform.position - transform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
		    }
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("Enemy") && target == null)
		{
			target = other.gameObject;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("Enemy") && target == null)
		{
			target = other.gameObject;
		}
	}
}

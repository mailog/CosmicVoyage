using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCompanion : MonoBehaviour {

	public GameObject pewpew;
	private float fireRate;
	private float fireTimer;
	private float damage;
	private float bulletSpeed;
	void Start () {
		fireRate = 0.2f;
		fireTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<CompanionFollow>().target != null)
		{
			Vector3 vectorToTarget = gameObject.GetComponent<CompanionFollow>().target.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
	        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
			if(fireTimer >= fireRate)
			{	
				GameObject bullet1 = Instantiate(pewpew, transform.position, transform.rotation);
				bullet1.transform.Rotate(0,0,180f);

				bullet1.GetComponent<Pewpew>().damage = 2f;
				bullet1.GetComponent<Pewpew>().bulletSpeed = 30f;

				fireTimer = 0;	
			}
			else
			{
				fireTimer += Time.deltaTime;
			}
	    }
	}

}

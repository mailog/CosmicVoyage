using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunCompanion : MonoBehaviour {

	public GameObject pewpew;
	private float fireRate;
	private float fireTimer;
	private float damage;
	private float bulletSpeed;
	void Start () {
		fireRate = 0.5f;
		fireTimer = 0f;
		damage = 2f;
		bulletSpeed = 20f;
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
	    }
	}
}

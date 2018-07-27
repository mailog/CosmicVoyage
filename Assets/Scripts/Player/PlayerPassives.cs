using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassives : MonoBehaviour {

	public bool homingShots;
	public bool splitShots;
	public bool pierceShots;
	public bool slowShots;
	public bool missilesShots;
	public bool lasersShots;

	public bool additionalShots;

	public bool damageUp;

	public bool fireRateUp;

	public bool healthUp;

	public bool movementSpeedUp;

	public bool bulletSpeedUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void homing()
	{
		homingShots = true;
	}

	public void split()
	{
		splitShots = true;
	}

	public void pierce()
	{
		pierceShots = true;
	}

	public void slow()
	{
		slowShots = true;
	}

	public void missiles()
	{
		missilesShots = true;
	}

	public void lasers()
	{
		lasersShots = true;
	}

	public void additionalShotsOne()
	{
		additionalShots = true;
	}

	public void damageUpOne()
	{
		damageUp = true;
	}

	public void fireRateOne()
	{
		fireRateUp = true;
	}

	public void healthUpOne()
	{
		healthUp = true;
	}

	public void movementSpeedOne()
	{
		movementSpeedUp = true;
	}

	public void bulletSpeedOne()
	{
		bulletSpeedUp = true;
	}
}

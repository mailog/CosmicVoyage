using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public GameObject pewpews;
	public GameObject levelUpCanvas;
	public GameObject actualHitbox;

	public Button[] optionButts;
	public Text levelUpText;

	public Sprite[] upgradeSprites;

	public ArrayList options;

	public bool pause;

	public float buttonTapTime;
	public float buttonTapCounter;

	public Text currGun;
	private int gunType = 0;
	public Slider fireRateSlider;

	private Vector3 mouse_pos;
	private Transform target;
	private Vector3 object_pos;
	private float angle;	

	private bool invulnerable;
	private float invulnerableDuration;
	private float invulnerableCounter;

	public float flashTimer = 0.25f;
	public Color colorStart;
	public Color colorEnd;
	public Color colorFade;

	public int additionalShot;

	private Renderer rend;

	public GameObject heart;

	public ArrayList hearts;
	public float lastHearts;

	public Text damageStat;
	public Text fireRateStat;
	public Text movementSpeedStat;
	public Text bulletSpeedStat;

	public List<int> upgrades;
	public int numUpgrades;
	public int chosenUpgrades;

	public float fireRateModifier;
	public float damageModifier;
	public float bulletSpeedModifier;
	public float movementSpeedModifier;
	public float accuracyModifier;

	public float health;
	public float damage;
	public float bulletSpeed;
	public float movementSpeed;

	public float fireRate;
	private float fireTimer;

	public bool basicGunActive;
	public bool shotGunActive;
	public bool cannonActive;
	public bool machineGunActive;

	public Slider activeBarSlider;
	public Image activeFill;
	public float activeCD;
	private float activeTimer;
	private int[] actives;
	private int currentActive1;
	private int currentActive2;
	public Text textActive1;
	public Text textActive2;

	public bool missileActive;
	public GameObject missile;

	public bool shieldActive;
	public float shieldTimer;
	public float shieldDuration;
	public GameObject shield;

	public int damageUpgradeTier;
	public int fireRateUpgradeTier;
	public int movementSpeedUpgradeTier;
	public int bulletSpeedUpgradeTier;

	public Slider expSlider;
	public int level;
	public float exp;
	public float nextLevel;

	public ArrayList companions;
	public GameObject basicCompanion;
	public GameObject shotgunCompanion;

	// Use this for initialization
	void Start () {
		pause = false;
		pewpews.GetComponent<Pewpew>().velocity = new Vector2(0, 10f);
		invulnerable = false;
		invulnerableCounter = 0;
		invulnerableDuration = 2f;
		rend = GetComponent<Renderer>();
		hearts = new ArrayList();
		upgrades = new List<int>();
		numUpgrades = 9;

		basicGunActive = true;

		companions = new ArrayList();

		additionalShot = 0;

		fireTimer = 1f;

		damageUpgradeTier = 0;
		fireRateUpgradeTier = 0;
		movementSpeedModifier = 0;
		bulletSpeedUpgradeTier = 0;

		fireRateModifier = 1;
		damageModifier = 1;
		movementSpeedModifier = 1;
		bulletSpeedModifier = 1;
		accuracyModifier = 0;

		damageStat.text = "" + damageModifier.ToString("F1") + "x";
		fireRateStat.text = "" + fireRateModifier.ToString("F1") + "x";
		movementSpeedStat.text = "" + damageModifier.ToString("F1") + "x";
		bulletSpeedStat.text = "" + bulletSpeedModifier.ToString("F1") + "x";

		fireRateSlider.maxValue = fireRate;

		for(int i = 0; i < numUpgrades; i++)
		{
			upgrades.Add(i);
		}

		exp = 0;
		expSlider.maxValue = nextLevel;
		for(int i = 0; i < health/10; i++)
		{
			GameObject tmp2 = Instantiate(heart, new Vector3(-8+(i*1.4f), -18.5f, 0), Quaternion.identity);
			tmp2.GetComponent<Renderer>().material.color = colorFade;
			GameObject tmp = Instantiate(heart, new Vector3(-8+(i*1.4f), -18.5f, 0), Quaternion.identity);
			hearts.Add(tmp);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
		{
			actualHitbox.SetActive(true);
		}
		if(Input.GetMouseButtonUp(0))
		{
			actualHitbox.SetActive(false);
		}
		activeBarSlider.maxValue = activeCD;
		activeBarSlider.value = activeTimer;

		damageStat.text = "" + damageModifier.ToString("F1") + "x";
		fireRateStat.text = "" + fireRateModifier.ToString("F1") + "x";
		movementSpeedStat.text = "" + movementSpeedModifier.ToString("F1") + "x";
		bulletSpeedStat.text = "" + bulletSpeedModifier.ToString("F1") + "x";
		expSlider.value = exp;
		fireRateSlider.value = fireTimer;

		if(exp >= nextLevel)
		{
			//levelUp();
		}
		float x = 0;
		float y = 0;

		float xt = 0;
		float yt = 0;
		/*if(Input.GetAxis("Horizontal") < 0)
		{
			x = -1 * Time.deltaTime * (7.5f * movementSpeedModifier);
		}
		else if(Input.GetAxis("Horizontal") > 0)
		{
	        x = 1 * Time.deltaTime * (7.5f * movementSpeedModifier);
		}

		if(Input.GetAxis("Vertical") < 0)
		{
			y = -1 * Time.deltaTime * (7.5f * movementSpeedModifier);
		}
		else if(Input.GetAxis("Vertical") > 0)
		{
	        y = 1 * Time.deltaTime * (7.5f * movementSpeedModifier);
		}*/


		//x = Input.GetAxis("Horizontal") * Time.deltaTime * (20.0f * movementSpeedModifier);
		//y = Input.GetAxis("Vertical") * Time.deltaTime * (20.0f * movementSpeedModifier);

		if(Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Horizontal") > -0.05f)
		{
			xt = -0.05f;
		}
		else if(Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Horizontal") < 0.05f)
		{
	        xt = 0.05f;
		}
		else
		{
			xt = Input.GetAxis("Horizontal");
		}

		if(Input.GetAxis("Vertical") < 0 && Input.GetAxis("Vertical") > -0.05f)
		{
			yt = -0.05f;
		}
		else if(Input.GetAxis("Vertical") > 0 && Input.GetAxis("Vertical") < 0.05f)
		{
			yt = 0.05f;
		}
		else
		{
			yt = Input.GetAxis("Vertical");
		}

		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
		{
			xt = 0;
		}

		if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
		{
			yt = 0;
		}

		x = xt * Time.deltaTime * (23.0f * movementSpeedModifier);
		y = yt * Time.deltaTime * (23.0f * movementSpeedModifier);


		/*if(Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
		{
			transform.position += new Vector3(0,0.18f*movementSpeedModifier,0);
		}

		if(Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A))
		{
			transform.position += new Vector3(-0.18f*movementSpeedModifier,0,0);
		}

		if(Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
		{
			transform.position += new Vector3(0,-0.18f*movementSpeedModifier,0);
		}

		if(Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
		{
			transform.position += new Vector3(0.18f*movementSpeedModifier,0,0);
		}*/

        transform.Translate(x, y, 0, Space.World);
        if(invulnerable)
        {
			float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
			rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);	
    		if(invulnerableCounter >= invulnerableDuration)
    		{
    			invulnerable = false;
				invulnerableCounter = 0;
    			rend.material.color = colorEnd;
    		}
    		else
    		{
    			invulnerableCounter += Time.deltaTime;
    		}
        }
        if(transform.position.x >= 10f)
        {
			transform.position = new Vector3(10f, transform.position.y, transform.position.z);
       	}
		if(transform.position.x <= -10f)
		{
			transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
       	}
		if(transform.position.y >= 19.5f)
		{
			transform.position = new Vector3(transform.position.x, 19.5f, transform.position.z);
       	}
		if(transform.position.y <= -19.5f)
		{
			transform.position = new Vector3(transform.position.x, -19.5f, transform.position.z);
       	}
		Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;
 
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
 
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        if(!shieldActive)
        {
			if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
			{
				shoot();
			}
	        if(activeTimer >= activeCD)
	        {
				if(Input.GetMouseButtonDown(1))
				{
					useActive2();
				}
				if(activeTimer > activeCD)
				{
					activeTimer = activeCD;
				}
				float lerp = Mathf.PingPong(Time.time, flashTimer) / flashTimer;
				activeFill.color = Color.Lerp(colorEnd, colorStart, lerp);
			}
			else
			{
				activeTimer += Time.deltaTime;
				activeFill.color = colorEnd;
			}
		}
		else
		{
			if(shieldTimer >= shieldDuration)
			{
				shield.SetActive(false);
				shieldActive = false;
				activeCD = 7f;
				activeTimer = 0f;
			}
			else
			{
				shieldTimer += Time.deltaTime;
				shield.GetComponent<Renderer>().material.color = Color.Lerp(colorEnd, colorStart, (shieldTimer/shieldDuration));	
			}
		}
		/*if(Input.GetMouseButtonUp(0))
			{
	       		fireRateSlider.gameObject.SetActive(true);
			}*/
			//shoot();
		fireTimer += (Time.deltaTime * fireRateModifier);
        if(fireTimer >= fireRate)
       	{
       		fireTimer = fireRate + 1;
       		//fireRateSlider.gameObject.SetActive(false);
       	}

		if(health <= 0)
		{
			Destroy(gameObject);
			Time.timeScale = 0;
		}
	}

	void useActive1()
	{
		switch(currentActive1)
		{
			case 1: useMissile();
			break;
			case 2: useShield();
			break;
			default:
			break;
		}
	}

	void useActive2()
	{
		switch(currentActive2)
		{
			case 1: useMissile();
			break;
			case 2: useShield();
			break;
			default:
			break;
		}
	}

	void useMissile()
	{
		GameObject temp = Instantiate(missile, transform.position, transform.rotation);
		temp.GetComponent<Missile>().damage = 10;
		temp.GetComponent<Missile>().target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		activeCD = 3f;
		activeTimer = 0f;
	}

	void useShield()
	{
		shield.SetActive(true);
		shieldActive = true;
		shieldDuration = 3f;
		shieldTimer = 0f;
	}

	public void levelUp()
	{
		levelUpText.text = "SALVAGE!";
		//Time.timeScale = 0;
		level++;
		exp = exp - nextLevel;
		nextLevel *= 1.3f;
		expSlider.maxValue = nextLevel;
		levelUpCanvas.SetActive(true);
		pause = true;
		List<int> upgradeOptions = new List<int>(upgrades);
		for(int i = 0; i<3; i++)
		{
			float modifier;
			/*switch(i)
			{
				case 0: modifier = Random.Range(50f,50f);
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(delegate{damageUpOne(modifier/100);});
						optionButts[i].GetComponentInChildren<Text>().text = "Damage Up " + modifier + "%";
				break;
				case 1: modifier = Random.Range(50,50f);
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(delegate{fireRateOne(modifier/100);});
						optionButts[i].GetComponentInChildren<Text>().text = "Fire Rate Up "  + modifier + "%";
				break;
				case 2: modifier = Random.Range(50,50f);
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(delegate{movementSpeedOne(modifier/100);});
						optionButts[i].GetComponentInChildren<Text>().text = "Movement Speed Up "  + modifier + "%";
				break;
				case 3: modifier = Random.Range(50,50f);
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(delegate{bulletSpeedOne(modifier/100);});
						optionButts[i].GetComponentInChildren<Text>().text = "Bullet Speed Up "  + modifier + "%";
				break;
				default:
				break;
			}*/
			int choice = Random.Range(0,upgradeOptions.Count);
			switch((int)upgradeOptions[choice])
			{
				case 0: modifier = Random.Range(50f,50f);
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(delegate{damageUpOne(modifier/100);});
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Damage Up " + modifier + "%";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
				break;
				case 1: modifier = Random.Range(50,50f);
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(delegate{fireRateOne(modifier/100);});
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Fire Rate Up "  + modifier + "%";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
				break;
				case 2: modifier = Random.Range(50,50f);
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(delegate{movementSpeedOne(modifier/100);});
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Movement Speed Up "  + modifier + "%";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
				break;
				case 3: modifier = Random.Range(50,50f);
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(delegate{bulletSpeedOne(modifier/100);});
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Bullet Speed Up "  + modifier + "%";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
				break;
				case 4: 
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(shotPlusOne);
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Additional Shot";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
						break;
				case 5: 
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(activateCannon);
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Cannon";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
				break;
				case 6: 
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(activateMachineGun);
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Minigun";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
				break;
				case 7: 
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(createBasicCompanion);
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Basic Drone";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
				break;
				case 8: 
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(createShotgunCompanion);
						optionButts[i].GetComponent<LevelUpOption>().optionText = "Shotgun Drone";
						optionButts[i].GetComponent<Image>().sprite = upgradeSprites[(int)upgradeOptions[choice]];
				break;
				/*case 9: 
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(installMissile);
						optionButts[i].GetComponentInChildren<Text>().text = "Install Missile";
				break;
				case 10: 
						optionButts[i].onClick.RemoveAllListeners();
						optionButts[i].onClick.AddListener(installShield);
						optionButts[i].GetComponentInChildren<Text>().text = "Install Shield";
				break;*/
				/*case 7: optionButts[i].onClick.AddListener(damageUpOne);
						optionButts[i].GetComponentInChildren<Text>().text = "Damage Up";
				break;
				case 8: optionButts[i].onClick.AddListener(fireRateOne);
						optionButts[i].GetComponentInChildren<Text>().text = "Fire Rate Up";
				break;
				case 9: optionButts[i].onClick.AddListener(healthUpOne);
						optionButts[i].GetComponentInChildren<Text>().text = "Health Up";
				break;
				case 10: optionButts[i].onClick.AddListener(movementSpeedOne);
						optionButts[i].GetComponentInChildren<Text>().text = "Movement Speed Up";
				break;
				case 11: optionButts[i].onClick.AddListener(bulletSpeedOne);
						optionButts[i].GetComponentInChildren<Text>().text = "Bullet Speed Up";
				break;*/
				default:
				break;
			}
			upgradeOptions.RemoveAt(choice);
		}
	}

	void finishLevelUp()
	{
		pause = false;
		//Time.timeScale = 1;
		levelUpCanvas.SetActive(false);
	}

	void removeUpgrade(int remove)
	{
		for(int i = 0; i < upgrades.Count; i++)
		{
			if(upgrades[i] == remove)
			{
				upgrades.RemoveAt(i);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("PlayerPewpew"))
		{
			Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}
		if(other.gameObject.tag.Equals("EnemyPewpew"))
		{
			takeDamage(other.gameObject.GetComponent<Pewpew>().damage);
			Destroy(other.gameObject);
		}
	}

	public void takeDamage(float damage)
	{
		if(!invulnerable && !shieldActive)
		{
	 		health -= damage;
	 		GameObject tmp = (GameObject)hearts[hearts.Count - 1];
	 		Destroy(tmp);
	 		hearts.RemoveAt(hearts.Count-1);
	 		invulnerable = true;
	 	}
	}

	void shoot()
	{
		if(fireTimer >= fireRate)
		{
			if(additionalShot == 0)
			{
				GameObject temp = Instantiate(pewpews, transform.position, transform.rotation);
				temp.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp.transform.localScale = new Vector2(temp.transform.localScale.x * damageModifier, temp.transform.localScale.y * damageModifier);
				temp.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
				fireTimer = 0;
			}
			if(additionalShot == 1)
			{
				GameObject temp = Instantiate(pewpews, transform.position + (0.5f*transform.right), transform.rotation);
				temp.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp.transform.localScale = new Vector2(temp.transform.localScale.x * damageModifier, temp.transform.localScale.y * damageModifier);
				temp.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
			
				GameObject temp2 = Instantiate(pewpews, transform.position - (0.5f*transform.right), transform.rotation);
				temp2.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp2.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp2.transform.localScale = new Vector2(temp2.transform.localScale.x * damageModifier, temp2.transform.localScale.y * damageModifier);
				temp2.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
				fireTimer = 0;
			}
			if(additionalShot == 2)
			{
				GameObject temp = Instantiate(pewpews, transform.position, transform.rotation);
				temp.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp.transform.localScale = new Vector2(temp.transform.localScale.x * damageModifier, temp.transform.localScale.y * damageModifier);
				temp.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
			
				GameObject temp2 = Instantiate(pewpews, transform.position + (0.5f*transform.right), transform.rotation);
				temp2.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp2.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp2.transform.localScale = new Vector2(temp2.transform.localScale.x * damageModifier, temp2.transform.localScale.y * damageModifier);
				temp2.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));

				GameObject temp3 = Instantiate(pewpews, transform.position - (0.5f*transform.right), transform.rotation);
				temp3.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp3.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp3.transform.localScale = new Vector2(temp3.transform.localScale.x * damageModifier, temp3.transform.localScale.y * damageModifier);
				temp3.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
				fireTimer = 0;
			}
			if(additionalShot == 3)
			{
				GameObject temp = Instantiate(pewpews, transform.position + (0.5f * transform.right), transform.rotation);
				temp.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp.transform.localScale = new Vector2(temp.transform.localScale.x * damageModifier, temp.transform.localScale.y * damageModifier);
				temp.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
			
				GameObject temp2 = Instantiate(pewpews, transform.position - (0.5f * transform.right), transform.rotation);
				temp2.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp2.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp2.transform.localScale = new Vector2(temp2.transform.localScale.x * damageModifier, temp2.transform.localScale.y * damageModifier);
				temp2.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));

				GameObject temp3 = Instantiate(pewpews, transform.position + (1.5f*transform.right), transform.rotation);
				temp3.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp3.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp3.transform.localScale = new Vector2(temp3.transform.localScale.x * damageModifier, temp3.transform.localScale.y * damageModifier);
				temp3.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));

				GameObject temp4 = Instantiate(pewpews, transform.position - (1.5f*transform.right), transform.rotation);
				temp4.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp4.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp4.transform.localScale = new Vector2(temp4.transform.localScale.x * damageModifier, temp4.transform.localScale.y * damageModifier);
				temp4.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
				fireTimer = 0;
			}
			if(additionalShot == 4)
			{
				GameObject temp = Instantiate(pewpews, transform.position, transform.rotation);
				temp.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp.transform.localScale = new Vector2(temp.transform.localScale.x * damageModifier, temp.transform.localScale.y * damageModifier);
				temp.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
			
				GameObject temp2 = Instantiate(pewpews, transform.position + (0.5f*transform.right), transform.rotation);
				temp2.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp2.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp2.transform.localScale = new Vector2(temp2.transform.localScale.x * damageModifier, temp2.transform.localScale.y * damageModifier);
				temp2.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));

				GameObject temp3 = Instantiate(pewpews, transform.position - (0.5f*transform.right), transform.rotation);
				temp3.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp3.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp3.transform.localScale = new Vector2(temp3.transform.localScale.x * damageModifier, temp3.transform.localScale.y * damageModifier);
				temp3.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));

				GameObject temp4 = Instantiate(pewpews, transform.position + (1.5f*transform.right), transform.rotation);
				temp4.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp4.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp4.transform.localScale = new Vector2(temp4.transform.localScale.x * damageModifier, temp4.transform.localScale.y * damageModifier);
				temp4.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));

				GameObject temp5 = Instantiate(pewpews, transform.position - (1.5f*transform.right), transform.rotation);
				temp5.GetComponent<Pewpew>().damage = damage * damageModifier;
				temp5.GetComponent<Pewpew>().bulletSpeed = bulletSpeed * bulletSpeedModifier;
				temp5.transform.localScale = new Vector2(temp5.transform.localScale.x * damageModifier, temp5.transform.localScale.y * damageModifier);
				temp5.transform.Rotate(0,0,Random.Range(accuracyModifier,-accuracyModifier));
				fireTimer = 0;
			}
		}
	}

	public void damageUpOne(float upgrade)
	{
		finishLevelUp();
		damageModifier *= 1 + upgrade;
		damageUpgradeTier++;
		if(damageUpgradeTier>=3)
		{
			removeUpgrade(0);
		}
	}

	public void fireRateOne(float upgrade)
	{
		finishLevelUp();
		fireRateModifier *= 1 + upgrade;
		fireRateUpgradeTier++;
		if(fireRateUpgradeTier>=3)
		{ 
			removeUpgrade(1);
		}
	}

	public void movementSpeedOne(float upgrade)
	{
		finishLevelUp();
		movementSpeedModifier *= 1 + upgrade;
		movementSpeedUpgradeTier++;
		if(movementSpeedUpgradeTier>=3)
		{
			removeUpgrade(2);
		}
	}

	public void bulletSpeedOne(float upgrade)
	{
		finishLevelUp();
		bulletSpeedModifier *= 1 + upgrade;
		bulletSpeedUpgradeTier++;
		if(bulletSpeedUpgradeTier>=3)
		{
			removeUpgrade(3);
		}
	}

	public void shotPlusOne()
	{
		finishLevelUp();
		additionalShot++;
		fireRateModifier *= 0.8f;
		if(additionalShot >=4)
		{
			removeUpgrade(4);
		}
	}

	public void activateCannon()
	{
		finishLevelUp();
		damageModifier *= 2f;
		fireRateModifier *= 0.75f;
		removeUpgrade(5);
	}

	public void activateMachineGun()
	{
		finishLevelUp();
		damageModifier *= 0.5f;
		fireRateModifier *= 3f;
		accuracyModifier += 5f;
		removeUpgrade(6);
	}

	public void createBasicCompanion()
	{
		finishLevelUp();
		GameObject temp = Instantiate(basicCompanion, new Vector2(transform.position.x, transform.position.y - 10f), transform.rotation);
		if(companions.Count == 0)
		{
			temp.GetComponent<CompanionFollow>().follow = gameObject;
		}
		else
		{
			temp.GetComponent<CompanionFollow>().follow = (GameObject)companions[companions.Count-1];
		}
		companions.Add(temp);
		removeUpgrade(7);
	}

	public void createShotgunCompanion()
	{
		finishLevelUp();
		GameObject temp = Instantiate(shotgunCompanion, new Vector2(transform.position.x, transform.position.y - 10f), transform.rotation);
		if(companions.Count == 0)
		{
			temp.GetComponent<CompanionFollow>().follow = gameObject;
		}
		else
		{
			temp.GetComponent<CompanionFollow>().follow = (GameObject)companions[companions.Count-1];
		}
		companions.Add(temp);
		removeUpgrade(8);
	}

	public void installMissile()
	{
		if(currentActive1 == 0)
		{
			currentActive1 = 1;
			textActive1.text = "1) Missile";
		}
		else if(currentActive2 == 0)
		{
			currentActive2 = 1;
			textActive2.text = "2) Missile";
		}
		else
		{
			//replace
		}
		activeCD = 5f;
		activeTimer = 0f;
		finishLevelUp();
		removeUpgrade(9);
	}	

	public void installShield()
	{
		if(currentActive1 == 0)
		{
			currentActive1 = 2;
			textActive1.text = "1) Shield";
		}
		else if(currentActive2 == 0)
		{
			currentActive2 = 2;
			textActive2.text = "2) Shield";
		}
		else
		{
			//replace
		}
		activeCD = 3f;
		activeTimer = 0f;
		finishLevelUp();
		removeUpgrade(10);
	}
}

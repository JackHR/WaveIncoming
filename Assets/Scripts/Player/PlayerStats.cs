using UnityEngine;
using System.Collections;

public enum ClassType { Assault, Tank, Sniper };

public class PlayerStats : MonoBehaviour {

	[SerializeField]
	private ClassType classType;
	
	public PlayerNumber PlayerNumber { get { return playerNumber; } }
	[SerializeField]
	private PlayerNumber playerNumber;

	public ClassType Class { 
		get { return classType; } 
	}

	public Vector3 Position {
		get { return position; }
		set { position = value; }
	}

	public int Money {
		get { return money; }
		set { money = value; }
	}

	public int Health {
		get { return health; }
		set { health = Mathf.Clamp(value, 0, classStats.vitality); }
	}

	public int Vitality {
		get { return classStats.vitality; }
		set { classStats.vitality = value; }
	}

	public float Speed {
		get { return (2f + classStats.agility / 30); }
	}

	public float AimOffset {
		get { return ( (1f/(float)classStats.accuracy) * 8f ); }
	}

	public int Agility {
		get { return classStats.agility; }
		set { classStats.agility = Mathf.Clamp (value, 0, 220); }
	}

	public int Accuracy {
		get { return classStats.accuracy; }
		set { classStats.accuracy = value; }
	}

	public int Damage {
		get { return ( classStats.weaponStat.damage ); }
		set { classStats.weaponStat.damage = value; }
	}
	
	public int FireRate {
		get { return classStats.weaponStat.firerate; }
		set { classStats.weaponStat.firerate = value; }
	}
	
	public int CritChance {
		get { return ( classStats.weaponStat.critChance ); }
		set { classStats.weaponStat.critChance = Mathf.Clamp (value, 0, 100); }
	}
	
	public int ClipSize {
		get { return classStats.weaponStat.clipSize; }
		set { classStats.weaponStat.clipSize = value; }
	}
	
	public float ReloadTime {
		get { return classStats.weaponStat.reloadTime; }
		set { classStats.weaponStat.reloadTime = value; }
	}

	public Transform deathParticle;

	private int health;
	private ClassStats classStats;
	private int money = 0;
	private Vector3 position;

	[SerializeField]
	private float healthRegenRate = 1f;
	private float healthRegenDelay;
	private float lastRegen = 0f;

	private void Awake() {
		ResetPlayer();
		healthRegenDelay = 1f / healthRegenRate;
	}

	private void Update () {
		if (Time.time > lastRegen + healthRegenDelay && Health < Vitality) {
			Health += 5;
			CombatText.SpawnCombatText (Position, 5, CombatText.CombatTextType.heal);
			lastRegen = Time.time;
		}
	}

	public void AdjustMoney (int amount) {
		money += amount;
		CombatText.SpawnCombatText(position, amount, CombatText.CombatTextType.money);
	}

	public void ResetPlayer() {
		money = 100;
		classStats = new ClassStats(classType);

		Health = classStats.vitality;
		Agility = classStats.agility;
		Vitality = classStats.vitality;
		Accuracy = classStats.accuracy;
		
		Damage = classStats.weaponStat.damage;
		FireRate = classStats.weaponStat.firerate;
		CritChance = classStats.weaponStat.critChance;
		ClipSize = classStats.weaponStat.clipSize;
		ReloadTime = classStats.weaponStat.reloadTime;
	}

	public void AdjustPlayerHealth (int adj) {
		Health += adj;
		
		CombatText.SpawnCombatText (Position, adj, CombatText.CombatTextType.damage);
		
		if (Health <= 0) {
			StartCoroutine ("KillPlayer");
		}
	}

	public IEnumerator KillPlayer () {
		Instantiate (deathParticle, Position, Quaternion.identity);
		Destroy (gameObject);
		
		yield return new WaitForSeconds (1f);
		
		float waitTime = Fade.FadeInstance.BeginFade(1);
		yield return new WaitForSeconds (waitTime);
		
		Application.LoadLevel (Strings.MainMenu);
	}
}
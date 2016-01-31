using UnityEngine;
using System.Collections;

public class ClassStats {

	public string name;
	
	public int vitality;
	public int agility;
	public int accuracy;
	
	public WeaponStats weaponStat;
	
	public ClassStats (ClassType classType) {
	
		if (classType == ClassType.Assault) {
			name = "Assault";
			
			vitality = 40;
			agility = 80;
			accuracy = 50;
			
			weaponStat = new WeaponStats(5, 10, 7, 20, 1f);
		}
		
		if (classType == ClassType.Tank) {
			name = "Tank";
			
			vitality = 110;
			agility = 8;
			accuracy = 90;
			
			weaponStat = new WeaponStats(14, 4, 3, 12, 2f);
		}
		
		if (classType == ClassType.Sniper) {
			name = "Sniper";
			
			vitality = 20;
			agility = 70;
			accuracy = 250;
			
			weaponStat = new WeaponStats(40, 0, 30, 6, 2f);
		}
	}
}

public class WeaponStats {

	public int damage;
	public int firerate;
	public int critChance;
	public int clipSize;
	public float reloadTime;
	
	public WeaponStats (int dmg, int rate, int cc, int clip, float reload) {
		damage = dmg;
		firerate = rate;
		critChance = cc;
		clipSize = clip;
		reloadTime = reload;
	}
	
	public WeaponStats () {
		damage = 1;
		firerate = 1;
		critChance = 1;
		clipSize = 1;
		reloadTime = 1f;
	}
}
using UnityEngine;
using System;

public class EnemyMaster : MonoBehaviour {

	public int health = 30;
	public Transform deathParticles;
	public int moneyReward = 30;
	private EnemyAI ai;

	private void Awake() {
		ai = GetComponent<EnemyAI>();
	}

	public void Damage (int damage, Action<int> rewardCallback) {
		health += damage;
		
		CombatText.SpawnCombatText (transform.position, damage, CombatText.CombatTextType.damage);
		
		if (health <= 0) {
			Die (rewardCallback);
		}
	}

	public void Die (Action<int> rewardCallback) {
		Transform clone = Instantiate (deathParticles, transform.position, Quaternion.identity) as Transform;
		rewardCallback(moneyReward);
		Destroy (clone.gameObject, 20f);
		Destroy (gameObject);
	}
}

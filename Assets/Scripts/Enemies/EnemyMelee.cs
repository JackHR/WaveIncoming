using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyAI))]
public class EnemyMelee : MonoBehaviour {

	public int damage = 10;
	public float hitRate = 1f;
	
	private float lastHit = 0f;

	private Transform target;
	private EnemyAI ai;
	
	void Start () {
		ai = GetComponent<EnemyAI>();
	}
	
	void Update () {
		if (target == null) {
			target = ai.target;
			return;
		}
		PlayerStats playerStats;
		if (ai.InSight(out playerStats)) {
			if (Time.time > lastHit + 1f/hitRate) {
				Hit (playerStats);
				lastHit = Time.time;
			}
		}
			
	}
	
	void Hit (PlayerStats playerStats) {
		playerStats.AdjustPlayerHealth (-damage);
		//Debug.Log (PlayerStats.playerHealth);
	}
	
}

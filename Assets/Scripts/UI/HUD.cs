using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class HUD : MonoBehaviour {

	public PlayerStats playerStats;
	public Slider slider;
	public Text bullets;

	private Animator hudAnimator;
	private int lastHealth = 0;
	private Weapon weapon;
	
	void Start () {
		hudAnimator = GetComponent<Animator>();
		weapon = transform.parent.GetComponentInChildren<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
		slider.maxValue = playerStats.Vitality;
	
		if (lastHealth != playerStats.Health) {
			slider.value = playerStats.Health;
			lastHealth = playerStats.Health;
			hudAnimator.SetBool ("Update", true);
		} else
			hudAnimator.SetBool ("Update", false);
		
		bullets.text = (playerStats.ClipSize - weapon.shotsFired).ToString();
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Customization : CursorMenu {

	public static bool MenuActive = false;
	
	// UPGRADE COSTS
	
	const int agilityCost = 65;
	const int vitalityCost = 55;
	const int accuracyCost = 55;
	
	const int damageCost = 45;
	const int fireRateCost = 70;
	const int critChanceCost = 60;
	const int clipSizeCost = 30;
	const int reloadTimeCost = 35;
	
	// TEXT REFERENCES
	
	public Text moneyText;
	
	public Text agilityText;
	public Text vitalityText;
	public Text accuracyText;
	
	public Text damageText;
	public Text fireRateText;
	public Text critChanceText;
	public Text clipSizeText;
	public Text reloadTimeText;
	
	// TEXT COST REFERENCES
	
	public Text agilityCostText;
	public Text vitalityCostText;
	public Text accuracyCostText;
	
	public Text damageCostText;
	public Text fireRateCostText;
	public Text critChanceCostText;
	public Text clipSizeCostText;
	public Text reloadTimeCostText;
	
	// BUTTON REFERENCES
	
	public Button agilityButton;
	public Button vitalityButton;
	public Button accuracyButton;
	
	public Button damageButton;
	public Button fireRateButton;
	public Button critChanceButton;
	public Button clipSizeButton;
	public Button reloadTimeButton;

	public ChangePlayerSprite changePlayerSprite;

	private PlayerStats PlayerStats_P1;
	private PlayerStats PlayerStats_P2;
	private PlayerStats currentStats;
	private GameObject cv;

	void Start () {

		PlayerStats[] playerStats = GameObject.FindObjectsOfType<PlayerStats>();
		foreach(var stats in playerStats) {
			if(stats.PlayerNumber == PlayerNumber.P1) {
				Debug.Log("Setting " + stats.gameObject + " as P1");
				PlayerStats_P1 = stats;
			}
			else
				PlayerStats_P2 = stats;
		}

		cv = transform.FindChild ("Canvas").gameObject;
		if (cv == null) {
			Debug.LogError ("No canvas under Customization?");
		}
	}

	void UpdateMenu(PlayerStats stats) {
		GetStats(stats);
		SetupButtons(stats);
	}

	void GetStats(PlayerStats stats) {
		moneyText.text = "Money: $" + stats.Money.ToString();
		
		agilityCostText.text = agilityCost.ToString();
		vitalityCostText.text = vitalityCost.ToString();
		accuracyCostText.text = accuracyCost.ToString();
		
		damageCostText.text = damageCost.ToString();
		fireRateCostText.text = fireRateCost.ToString();
		critChanceCostText.text = critChanceCost.ToString();
		clipSizeCostText.text = clipSizeCost.ToString();
		reloadTimeCostText.text = reloadTimeCost.ToString();
		
		agilityText.text = stats.Agility.ToString();
		vitalityText.text = stats.Vitality.ToString();
		accuracyText.text = stats.Accuracy.ToString();
		
		damageText.text = stats.Damage.ToString();
		fireRateText.text = stats.FireRate.ToString();
		critChanceText.text = stats.CritChance.ToString() + "%";
		clipSizeText.text = stats.ClipSize.ToString();
		reloadTimeText.text = stats.ReloadTime.ToString() + " sec";
	}

	void SetupButtons(PlayerStats stats) {
		agilityButton.GetComponentInChildren<Text>().text = "$" + agilityCost.ToString();
		if (agilityCost >= stats.Money || stats.Agility >= 220) {
			agilityButton.interactable = false;
		} else
			agilityButton.interactable = true;
		
		vitalityButton.GetComponentInChildren<Text>().text = "$" + vitalityCost.ToString();
		if (vitalityCost >= stats.Money) {
			vitalityButton.interactable = false;
		} else
			vitalityButton.interactable = true;
		
		accuracyButton.GetComponentInChildren<Text>().text = "$" + accuracyCost.ToString();
		if (accuracyCost >= stats.Money) {
			accuracyButton.interactable = false;
		} else
			accuracyButton.interactable = true;
		
		damageButton.GetComponentInChildren<Text>().text = "$" + damageCost.ToString();
		if (damageCost >= stats.Money) {
			damageButton.interactable = false;
		} else
			damageButton.interactable = true;
		
		fireRateButton.GetComponentInChildren<Text>().text = "$" + fireRateCost.ToString();
		if (fireRateCost >= stats.Money) {
			fireRateButton.interactable = false;
		} else if (stats.FireRate != 0)
			fireRateButton.interactable = true;
		
		critChanceButton.GetComponentInChildren<Text>().text = "$" + critChanceCost.ToString();
		if (critChanceCost >= stats.Money) {
			critChanceButton.interactable = false;
		} else
			critChanceButton.interactable = true;
		
		clipSizeButton.GetComponentInChildren<Text>().text = "$" + clipSizeCost.ToString();
		if (clipSizeCost >= stats.Money) {
			clipSizeButton.interactable = false;
		} else
			clipSizeButton.interactable = true;
		
		reloadTimeButton.GetComponentInChildren<Text>().text = "$" + reloadTimeCost.ToString();
		if (reloadTimeCost >= stats.Money) {
			reloadTimeButton.interactable = false;
		} else
			reloadTimeButton.interactable = true;
	}

	void Update () {

		if(PauseMenu.MenuActive)
			return;

		if (Input.GetButtonDown(Strings.Upgrade_P1)) {
			ToggleMenu(PlayerStats_P1);
		}
		else if(Input.GetButtonDown(Strings.Upgrade_P2)) {
			ToggleMenu(PlayerStats_P2);
		}
	}
	
	public void ToggleMenu (PlayerStats stats) {
		currentStats = stats;
		ToggleMenu(!MenuActive);

		if(MenuActive) {
			changePlayerSprite.ChangeSprite(stats.Class);
			UpdateMenu(stats);
		}
	}

	public void Close() {
		ToggleMenu(false);
	}

	protected override void ToggleMenu (bool menuActive)
	{
		base.ToggleMenu (menuActive);
		MenuActive = menuActive;
		cv.SetActive(menuActive);
		Time.timeScale = menuActive ? 0f : 1f;
	}

	
	public void UpgradeAgility (int amount) {
		currentStats.Agility += amount;
		currentStats.Money -= agilityCost;
		UpdateMenu(currentStats);
	}
	public void UpgradeVitality (int amount) {
		currentStats.Vitality += amount;
		currentStats.Money -= vitalityCost;
		UpdateMenu(currentStats);
	}
	public void UpgradeAccuracy (int amount) {
		currentStats.Accuracy += amount;
		currentStats.Money -= accuracyCost;
		UpdateMenu(currentStats);
	}
	
	public void UpgradeDamage (int amount) {
		currentStats.Damage += amount;
		currentStats.Money -= damageCost;
		UpdateMenu(currentStats);
	}
	public void UpgradeFireRate (int amount) {
		currentStats.FireRate += amount;
		currentStats.Money -= fireRateCost;
		UpdateMenu(currentStats);
	}
	public void UpgradeCritChance (int amount) {
		if (currentStats.CritChance == 100)
			return;
	
		currentStats.CritChance += amount;
		currentStats.Money -= critChanceCost;
		UpdateMenu(currentStats);
	}
	public void UpgradeClipSize (int amount) {
		currentStats.ClipSize += amount;
		currentStats.Money -= clipSizeCost;
		UpdateMenu(currentStats);
	}
	public void UpgradeReloadTime (int amount) {
		currentStats.ReloadTime = Mathf.Round( (currentStats.ReloadTime - (float)amount * currentStats.ReloadTime/100f) * 1000f) / 1000f;
		currentStats.Money -= reloadTimeCost;
		UpdateMenu(currentStats);
	}
	
}

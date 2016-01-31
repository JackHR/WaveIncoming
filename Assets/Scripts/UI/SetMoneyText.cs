using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetMoneyText : MonoBehaviour {

	public Text P1_Money, P2_Money;

	private bool P1_UpdateRunning, P2_UpdateRunning;

	// Use this for initialization
	void Start () {

		PlayerStats[] playerStats = GameObject.FindObjectsOfType<PlayerStats>();

		foreach(var stats in playerStats) {
			if(stats.PlayerNumber == PlayerNumber.P1 && !P1_UpdateRunning) {
				StartCoroutine(UpdateMoney(stats, P1_Money));
				P1_UpdateRunning = true;
			}
			else if(!P2_UpdateRunning) {
				StartCoroutine(UpdateMoney(stats, P2_Money));
				P2_UpdateRunning = true;
			}
		}

		if(MainMenu.SelectedGameMode == GameMode.Singleplayer)
			P2_Money.gameObject.SetActive(false);

	}

	IEnumerator UpdateMoney(PlayerStats stats, Text moneyText) {

		while(true) {
			moneyText.text = "Money: $" + stats.Money;
			yield return null;
		}
	}
}

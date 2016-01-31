using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GameMode { Singleplayer, Multiplayer };

public class MainMenu : MonoBehaviour {
	
	public static GameMode SelectedGameMode = GameMode.Singleplayer;
	public static ClassType P1_Class = ClassType.Assault;
	public static ClassType P2_Class = ClassType.Assault;
	
	public string assaultDesc;
	public string tankDesc;
	public string sniperDesc;
	public Text descText;
	public Text multiplayerText;
	public Text singleplayerText;

	public Color P1_color;
	public Color P2_color;
	
	void Start () {
		GameObject.Find (Strings.Assault).GetComponent<Image>().color = P1_color;
		JoystickDetector.Instance.ForceCursorOn = true;
	}

	public void PlayGame () {
		StartCoroutine (_PlayGame());
	}
	private IEnumerator _PlayGame () {
		float waitTime = Fade.FadeInstance.BeginFade(1);
		yield return new WaitForSeconds (waitTime);
		JoystickDetector.Instance.ForceCursorOn = false;
		Application.LoadLevel (Strings.MainLevel);
	}

	public void QuitGame () {
		Application.Quit();
	}

	public void LoadQualitySettingsMenu () {
		Application.LoadLevel (Strings.QualitySettingsMenu);
	}

	public void SelectGameMode (string gameMode) {

		switch (gameMode) {

		case(Strings.SinglePlayer):
			if(SelectedGameMode == GameMode.Singleplayer) {
				PlayGame ();
			}
			else {
				GameObject.Find (Strings.Assault).GetComponent<Image>().color = P2_color;
				multiplayerText.text = Strings.Multiplayer;
				descText.text = Strings.SelectP1Class;
				SelectedGameMode = GameMode.Singleplayer;
				HighlightSelectedButton(P1_Class);
			}
			break;

		case(Strings.Multiplayer):

			if(SelectedGameMode == GameMode.Multiplayer) {
				PlayGame();
			}
			else {
				GameObject.Find (Strings.Assault).GetComponent<Image>().color = P2_color;
				multiplayerText.text = Strings.Start;
				descText.text = Strings.SelectP2Class;
				SelectedGameMode = GameMode.Multiplayer;
				HighlightSelectedButton(P2_Class);
			}
			break;

		default:
			Debug.LogError ("Invalid game mode: " + gameMode);
			break;
		}
	}

	public void SelectClass (string cName) {

		switch(cName) 
		{
		case Strings.Tank:
			if(SelectedGameMode == GameMode.Singleplayer)
				P1_Class = ClassType.Tank;
			else
				P2_Class = ClassType.Tank;
			descText.text = tankDesc;
			break;
		case Strings.Sniper:
			if(SelectedGameMode == GameMode.Singleplayer)
				P1_Class = ClassType.Sniper;
			else
				P2_Class = ClassType.Sniper;
			descText.text = sniperDesc;
			break;
		default:
			if(SelectedGameMode == GameMode.Singleplayer)
				P1_Class = ClassType.Assault;
			else
				P2_Class = ClassType.Assault;
			descText.text = assaultDesc;
			break;
		}
		
		HighlightSelectedButton(cName);
	}

	private void HighlightSelectedButton(ClassType classType) {

		string cName;
		switch(classType)
		{
		case ClassType.Sniper:
			cName = Strings.Sniper;
			break;
		case ClassType.Tank:
			cName = Strings.Tank;
			break;
		default:
			cName = Strings.Assault;
			break;
		}

		HighlightSelectedButton(cName);
	}

	private void HighlightSelectedButton(string cName) {
		GameObject[] bns = GameObject.FindGameObjectsWithTag (Strings.ClassButtons);
		
		for (int i = 0; i < bns.Length; i++) {
			if (bns[i].name == cName)
				bns[i].GetComponent<Image>().color = SelectedGameMode == GameMode.Singleplayer ? P1_color : P2_color;
			else
				bns[i].GetComponent<Image>().color = Color.white;
		}
	}
}

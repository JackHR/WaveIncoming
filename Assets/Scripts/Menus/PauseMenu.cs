using UnityEngine;
using System.Collections;

public class PauseMenu : CursorMenu {
		
	public static bool MenuActive = false;
	public GameObject pauseMenu;
	public GameObject warningDialogue;

	private bool leaveGame = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(Strings.Pause_P1) && !Customization.MenuActive) {

			if (MenuActive == true)
				ToggleMenu(menuActive: false);

			else
				ToggleMenu(menuActive: true);
		}
	}
	
	public void Continue () {
		ToggleMenu(menuActive: false);
	}

	protected override void ToggleMenu (bool menuActive)
	{
		base.ToggleMenu (menuActive);
	
		MenuActive = menuActive;
		pauseMenu.SetActive(menuActive);
		Time.timeScale = menuActive ? 0f : 1f;
	}

	public void ToggleWarningDialogue (bool isOn) {
		warningDialogue.SetActive (isOn);
	}

	public void SetLeaveGame (bool isTrue) {
		leaveGame = isTrue;
	}

	public void LeaveGame () {
		if (leaveGame)
			_Quit();
		else
			StartCoroutine ( _OpenMainMenu() );
	}
	
	private void _Quit () {
		Time.timeScale = 1f;
		Application.Quit();
	}

	private IEnumerator _OpenMainMenu () {
		Time.timeScale = 1f;

		float waitTime = Fade.FadeInstance.BeginFade(1);
		yield return new WaitForSeconds (waitTime);

		ToggleMenu(false);
		Application.LoadLevel (Strings.MainMenu);
	}
	
	public void AdjustVolume (float vol) {
		Debug.Log ("TODO: CHANGE GLOBAL VOLUME");
	}
}

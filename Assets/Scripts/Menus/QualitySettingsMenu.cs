//-----------------------------------------------------------------
// This script is not at all optimized and is very dependent on all components
// being on the right objects... USE WITH CAUTION!
//-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QualitySettingsMenu : MonoBehaviour {

	public Transform resElement;
	public Transform resList;

	private Resolution[] resolutions;
	public List<Toggle> resToggles;
	private int qualityLevel = 2;
	private bool fullscreen = true;

	void Awake () {
		/*
		if (PlayerPrefs.GetInt (Strings.FirstLoad, 1) == 0) {

			int _w = PlayerPrefs.GetInt (Strings.ResWidth, 600);
			int _h = PlayerPrefs.GetInt (Strings.ResHeight, 400);
			bool _f = PlayerPrefs.GetInt (Strings.Fullscreen, 0) == 1;
			Screen.SetResolution ( _w, _h, _f );

			Application.LoadLevel (Application.loadedLevel + 1);
			return;
		}*/
	}

	// Use this for initialization
	void Start () {
		resolutions = Screen.resolutions;
		resToggles = new List<Toggle>();

		bool firstIteration = true;
		foreach (Resolution res in resolutions) {
			if (res.width > 800 && res.height > 600) {
				Transform re = (Transform)Instantiate (resElement);
				re.SetParent (resList);
				re.GetComponentInChildren <Text>().text = res.width.ToString() + " x " + res.height.ToString();
				Toggle toggle = re.GetComponentInChildren<Toggle>();
				toggle.group = resList.GetComponent<ToggleGroup>();
			
				if (firstIteration) {
					toggle.isOn = true;
					firstIteration = false;
				} else {
					toggle.isOn = false;
				}

				re.GetComponent<ResolutionElement>().res = res;
			}
		}

	}

	public void SetQualityLevel (int level) {
		qualityLevel = level;
	}

	public void SetFullScreen (bool isOn) {
		fullscreen = isOn;
	}
	
	public void ApplySettings () {

		foreach (Transform child in resList) {
			if (child.GetComponentInChildren<Toggle>().isOn) {
				Resolution res = child.GetComponent<ResolutionElement>().res;

				Screen.SetResolution ( res.width, res.height, fullscreen );
				PlayerPrefs.SetInt (Strings.ResWidth, res.width);
				PlayerPrefs.SetInt (Strings.ResHeight, res.height);
				int _f = (fullscreen) ? 1 : 0;
				PlayerPrefs.SetInt (Strings.Fullscreen, _f);
			}
		}

		QualitySettings.SetQualityLevel (qualityLevel);

		Application.LoadLevel(Strings.MainMenu);
	}
}
using UnityEngine;

public class CursorMenu : MonoBehaviour {

	protected virtual void ToggleMenu(bool menuActive) {

		if(menuActive)
			JoystickDetector.Instance.ForceCursorOn = true;

		else if(!menuActive)
			JoystickDetector.Instance.ForceCursorOn = false;
	}
}
